# CHECKERS
A classic game of **Checkers (Draughts)** implemented in C# using WPF and MVVM architecture.
 
---
 
## Functionality
 
- Classic checkers rules on an 8×8 board
- Mandatory capture rule (if a capture is available, it must be made)
- Chain captures (multiple jumps in a single turn)
- Promotion of regular checkers to **Kings** upon reaching the opposite end
- Kings can move and capture in all four diagonal directions across multiple squares
- Visual highlighting of selected pieces and available moves
- Win detection (no pieces left or no valid moves)
- **Single-player mode against an AI opponent with selectable difficulty (Easy, Medium, Hard)**
- **Heuristic-based move evaluation for the Hard AI difficulty (prioritizing kings, edge safety, and back-row defense)**
- **Real-time AI toggle during gameplay to easily switch between PvP and PvE modes**
- Settings window with resolution options, fullscreen toggle, and AI difficulty selection
- Settings are saved to a local `settings.json` file and restored on next launch
- Main menu with Play, Settings, and Exit options
- In-game menu for New Game, Main Menu, Settings, and Exit
---
 
## How to Run Locally
 
### Requirements
 
- .NET 8 SDK
- Windows OS (WPF is Windows-only)
### Steps
 
```bash
git clone https://github.com/your-username/CHECKERSApp.git
cd CHECKERSApp
dotnet run --project CHECKERSApp
```
 
Or open the `.sln` file in **Visual Studio 2022+** and press `F5`.
 
---
 
## Project Structure
 
```
CHECKERS/
├── Models/           # Domain models: Board, Cell, Move, enums, settings, AIDifficulty
├── ViewModels/       # MVVM ViewModels: MainWindowViewModel, CellViewModel, SettingsViewModel
├── Views/            # WPF Windows and XAML
├── Services/         # Game logic, rules, state machine, strategies
│   ├── AI/           # AI opponent logic, heuristics, and mode tracking
│   ├── Settings/     # Settings load/save
│   └── States/       # State pattern interfaces
├── Convertor/        # WPF value converters
└── App.xaml.cs       # DI container setup and application entry point
```
 
---
 
## Programming Principles

### 1. Single Responsibility Principle (SRP)
Each class has one clearly defined responsibility.
`BoardSetupService` only places pieces, `PromotionService` only promotes,
`TurnSwitcher` only switches turns, `GameSnapshotService` only
serializes/restores board state. `MainWindowViewModel` only binds UI —
it never builds snapshots or applies window settings directly.

### 2. Open/Closed Principle (OCP)
New piece types are added by implementing `IMoveStrategy` and registering
a condition in `MoveStrategyRegistry` — no existing code is touched.
New services are added to the DI container in `App.xaml.cs` with a single
line — `OnStartup` itself is never modified.

### 3. Liskov Substitution Principle (LSP)
`IdleState` and `PieceSelectedState` both implement `IGameState` and are
substituted transparently via `IStateContext.TransitionTo(...)`.

### 4. Interface Segregation Principle (ISP)
`IStateContext` is composed of three focused sub-interfaces:
`IBoardContext` (board + player + rules),
`IStateManager` (highlights + transitions),
`IMoveContext` (move execution).
States depend only on the subset they actually use.

### 5. Dependency Inversion Principle (DIP)
All components depend on abstractions. `IGameContext` exposes
`SwitchTurn()` and `CurrentPlayer` setter so callers never cast to
`GameContext`. `IGameSnapshotService` owns snapshot logic so the ViewModel
never touches `Board` internals directly.
---
 
## Design Patterns
 
### 1. State Pattern
**Files:** [`IGameState`](CHECKERSApp/CHECKERS/Services/States/IGameState.cs), [`IdleState`](CHECKERSApp/CHECKERS/Services/States/IdleState.cs), [`PieceSelectedState`](CHECKERSApp/CHECKERS/Services/States/PieceSelectedState.cs), [`IStateContext`](CHECKERSApp/CHECKERS/Services/States/IStateContext.cs)
 
The game board input is handled through a State machine. When no piece is selected, the board is in `IdleState`. After selecting a valid piece, it transitions to `PieceSelectedState`. This eliminates conditional branching (`if (pieceSelected) ... else ...`) and makes adding new states straightforward.
 
### 2. Strategy Pattern
**Files:** [`IMoveStrategy`](CHECKERSApp/СHECKERS/Services/Strategies/IMoveStrategy.cs), [`CheckerMoveStrategy`](CHECKERSApp/СHECKERS/Services/Strategies/CheckerMoveStrategy.cs), [`KingMoveStrategy`](CHECKERSApp/СHECKERS/Services/Strategies/KingMoveStrategy.cs), [`MoveStrategyRegistry`](CHECKERSApp/СHECKERS/Services/Strategies/MoveStrategyRegistry.cs)
 
Move generation is abstracted behind `IMoveStrategy`. Regular checkers use `CheckerMoveStrategy` (one square forward, capture in all directions), while kings use `KingMoveStrategy` (unlimited diagonal movement). The correct strategy is resolved at runtime by `MoveStrategyRegistry` based on the piece type.
 
### 3. Factory Pattern
**Files:** [`IMoveStrategyFactory`](CHECKERSApp/СHECKERS/Services/Strategies/MoveStrategyFactory/IMoveStrategyFactory.cs), [`MoveStrategyFactory`](CHECKERSApp/СHECKERS/Services/Strategies/MoveStrategyFactory/MoveStrategyFactory.cs)
 
`MoveStrategyFactory` encapsulates the logic for selecting the appropriate `IMoveStrategy` for a given cell. Consumers such as `GameRules` request a strategy through the factory without knowing how it is resolved.
 
### 4. Command Pattern
**Files:** [`Command`](CHECKERSApp/СHECKERS/ViewModels/Base/Command.cs), [`MainWindowViewModel`](CHECKERSApp/СHECKERS/ViewModels/MainWindowViewModel.cs)
 
All UI actions (Play, New Game, Exit, Cell Click, Open Settings) are encapsulated as `ICommand` instances. This decouples the View from the ViewModel and allows binding directly in XAML.
 
### 5. Observer Pattern (via INotifyPropertyChanged)
**Files:** [`ViewModel`](CHECKERSApp/CHECKERS/ViewModels/Base/ViewModel.cs), [`CellViewModel`](CHECKERSApp/СHECKERS/ViewModels/CellViewModel.cs)
 
The base `ViewModel` class implements `INotifyPropertyChanged`. All UI state (piece type, highlighting, active cell, current player) is automatically propagated to the View when properties change, without manual UI updates.
 
---
 
## Refactoring Techniques
 
### 1. Extract Method
Large blocks of logic were broken into focused private methods. For example, chain-capture detection was extracted into `TryChainCapture(...)` in [`AfterMoveHandler`](CHECKERSApp/СHECKERS/Services/AfterMoveHandler/AfterMoveHandler.cs), and board initialization was extracted into [`BoardSetupService`](CHECKERSApp/СHECKERS/Services/BoardSetupService/BoardSetupService.cs).
 
### 2. Extract Interface
Concrete classes were refactored to depend on interfaces (`IGameRules`, `IMoveExecutor`, `IPromotionService`, etc.), enabling loose coupling and testability.
 
### 3. Replace Conditional with Polymorphism
The original `if/else` checks for piece type (checker vs. king) and game state (piece selected or not) were replaced with polymorphic dispatch via the Strategy and State patterns.
 
### 4. Introduce Parameter Object
Related parameters passed together (board, player, selected cell, available moves) were grouped into the `IStateContext` interface, reducing method signatures and improving cohesion.
 
### 5. Move Method
Responsibilities were relocated to the most appropriate class. For example, move application logic lives in `MoveExecutor.ApplyMove(...)`, promotion logic in `PromotionService`, and turn switching in `TurnSwitcher` — each in isolation.
 
### 6. Replace Magic Numbers with Constants/Enums
Board size (8×8), piece colors, and cell states are represented by named types (`CellValueEnum`, board bounds checked via `Board.InBounds(...)`) rather than raw literals.
 
### 7. Separate Query from Modifier (CQS)
Methods either return data (`GetAvailableMoves`, `GetWinner`) or cause side effects (`Execute`, `Switch`, `ClearHighlights`) — not both. This makes the codebase easier to reason about and test.

### 8. Asynchronous Execution (Task-based UI)

The AI move execution was refactored to use async/await and Task.Delay(). This prevents the UI thread from freezing during AI computations and provides a natural delay, improving the user experience during PvE matches.
