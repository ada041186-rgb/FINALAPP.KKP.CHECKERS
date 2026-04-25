using СHECKERS.Models;
using СHECKERS.ViewModels.Base;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace СHECKERS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private Board board = new();
        private ICommand? cellCommand;
        private ICommand? newGameCommand;
        private CellValueEnum currentPlayer = CellValueEnum.WhiteChecker;
        private string currentPlayerText = "Хід: Білі";

        public Board Board
        {
            get => board;
            set
            {
                board = value;
                OnPropertyChanged();
            }
        }

        public string CurrentPlayerText
        {
            get => currentPlayerText;
            set
            {
                currentPlayerText = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewGameCommand => newGameCommand ??= new Command(_ =>
        {
            currentPlayer = CellValueEnum.WhiteChecker;
            CurrentPlayerText = "Хід: Білі";
            SetupBoard();
        });

        public ICommand CellCommand => cellCommand ??= new Command(parameter =>
        {
            Cell cell = (Cell)parameter!;
            Cell? activeCell = Board.FirstOrDefault(x => x.Act);

            if (cell.Cellvalueenum != CellValueEnum.Empty)
            {
                if (cell.Cellvalueenum == currentPlayer)
                {
                    if (activeCell != null) activeCell.Act = false;
                    if (!cell.Act)
                        cell.Act = true;
                    else
                        cell.Act = false;
                }
                else if (activeCell != null)
                {
                    cell.Act = false;
                }
            }
            else if (activeCell != null && activeCell.Cellvalueenum == currentPlayer)
            {
                int rowDiff = Math.Abs(activeCell.Row - cell.Row);
                int colDiff = Math.Abs(activeCell.Column - cell.Column);

                bool validDirection = currentPlayer == CellValueEnum.WhiteChecker
                    ? cell.Row < activeCell.Row   
                    : cell.Row > activeCell.Row;  

                bool simpleMove = rowDiff == 1 && colDiff == 1 && validDirection;

                bool captureMove = false;
                Cell? capturedCell = null;
                if (rowDiff == 2 && colDiff == 2)
                {
                    int captureRow = (activeCell.Row + cell.Row) / 2;
                    int captureCol = (activeCell.Column + cell.Column) / 2;
                    capturedCell = Board.FirstOrDefault(x => x.Row == captureRow && x.Column == captureCol);
                    captureMove = capturedCell != null
                        && capturedCell.Cellvalueenum != CellValueEnum.Empty
                        && capturedCell.Cellvalueenum != activeCell.Cellvalueenum;
                }

                if (simpleMove || captureMove)
                {
                    activeCell.Act = false;

                    if (captureMove && capturedCell != null)
                    {
                        capturedCell.Cellvalueenum = CellValueEnum.Empty;
                    }

                    cell.Cellvalueenum = activeCell.Cellvalueenum;
                    activeCell.Cellvalueenum = CellValueEnum.Empty;

                    if (Board.VictoryCondition(currentPlayer))
                    {
                        ShowEndGameMessage(currentPlayer == CellValueEnum.WhiteChecker);
                        currentPlayer = CellValueEnum.WhiteChecker;
                        CurrentPlayerText = "Хід: Білі";
                        SetupBoard();
                    }
                    else
                    {
                        currentPlayer = currentPlayer == CellValueEnum.WhiteChecker
                            ? CellValueEnum.BlackChecker
                            : CellValueEnum.WhiteChecker;

                        CurrentPlayerText = currentPlayer == CellValueEnum.WhiteChecker
                            ? "Хід: Білі"
                            : "Хід: Чорні";
                    }
                }
            }

        }, parameter => parameter is Cell cell &&
            (Board.Any(x => x.Act) || (cell.Cellvalueenum != CellValueEnum.Empty && cell.Cellvalueenum == currentPlayer)));

        private void SetupBoard()
        {
            Board board = new();

            board[0, 1] = CellValueEnum.BlackChecker;
            board[0, 3] = CellValueEnum.BlackChecker;
            board[0, 5] = CellValueEnum.BlackChecker;
            board[0, 7] = CellValueEnum.BlackChecker;
            board[1, 0] = CellValueEnum.BlackChecker;
            board[1, 2] = CellValueEnum.BlackChecker;
            board[1, 4] = CellValueEnum.BlackChecker;
            board[1, 6] = CellValueEnum.BlackChecker;
            board[2, 1] = CellValueEnum.BlackChecker;
            board[2, 3] = CellValueEnum.BlackChecker;
            board[2, 5] = CellValueEnum.BlackChecker;
            board[2, 7] = CellValueEnum.BlackChecker;

            board[5, 0] = CellValueEnum.WhiteChecker;
            board[5, 2] = CellValueEnum.WhiteChecker;
            board[5, 4] = CellValueEnum.WhiteChecker;
            board[5, 6] = CellValueEnum.WhiteChecker;
            board[6, 1] = CellValueEnum.WhiteChecker;
            board[6, 3] = CellValueEnum.WhiteChecker;
            board[6, 5] = CellValueEnum.WhiteChecker;
            board[6, 7] = CellValueEnum.WhiteChecker;
            board[7, 0] = CellValueEnum.WhiteChecker;
            board[7, 2] = CellValueEnum.WhiteChecker;
            board[7, 4] = CellValueEnum.WhiteChecker;
            board[7, 6] = CellValueEnum.WhiteChecker;

            Board = board;
        }

        public static void ShowEndGameMessage(bool isWhiteWinner)
        {
            string winner = isWhiteWinner ? "Білі шашки" : "Чорні шашки";
            MessageBox.Show($"Кінець гри. Переможець - {winner}.", "Кінець гри",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MainWindowViewModel()
        {
            SetupBoard();
        }
    }
}