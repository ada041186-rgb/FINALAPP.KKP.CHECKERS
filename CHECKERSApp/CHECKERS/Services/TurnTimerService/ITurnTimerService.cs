using System;

namespace CHECKERS.Services
{
    public interface ITurnTimerService
    {
        int SecondsLeft { get; }
        int LimitSeconds { get; }
        event Action? Tick;
        event Action? TimeExpired;
        void Start();
        void Stop();
        void Reset();
    }
}