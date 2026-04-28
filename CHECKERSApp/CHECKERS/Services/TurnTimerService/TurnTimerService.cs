using System;
using System.Timers;

namespace CHECKERS.Services
{
    public class TurnTimerService : ITurnTimerService, IDisposable
    {
        private readonly System.Timers.Timer _timer;

        public int LimitSeconds { get; }
        public int SecondsLeft { get; private set; }

        public event Action? Tick;
        public event Action? TimeExpired;

        public TurnTimerService(int limitSeconds = 60)
        {
            LimitSeconds = limitSeconds;
            SecondsLeft = limitSeconds;

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnElapsed;
        }

        public void Start()
        {
            SecondsLeft = LimitSeconds;
            _timer.Start();
        }

        public void Stop() => _timer.Stop();

        public void Reset()
        {
            _timer.Stop();
            SecondsLeft = LimitSeconds;
            Tick?.Invoke();
        }

        private void OnElapsed(object? sender, ElapsedEventArgs e)
        {
            SecondsLeft--;
            Tick?.Invoke();
            if (SecondsLeft <= 0)
            {
                _timer.Stop();
                TimeExpired?.Invoke();
            }
        }

        public void Dispose() => _timer.Dispose();
    }
}