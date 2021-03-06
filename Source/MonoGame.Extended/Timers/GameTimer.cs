using System;
using Microsoft.Xna.Framework;

namespace MonoGame.Extended.Timers
{
    public abstract class GameTimer
    {
        protected GameTimer(double intervalSeconds)
            : this(TimeSpan.FromSeconds(intervalSeconds))
        {
        }

        protected GameTimer(TimeSpan interval)
        {
            Interval = interval;
            Restart();
        }

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Paused;

        public TimeSpan Interval { get; set; }
        public TimeSpan CurrentTime { get; protected set; }
        public TimerState State { get; protected set; }

        public void Start()
        {
            State = TimerState.Started;
            Started.Raise(this, EventArgs.Empty);
        }

        public void Stop()
        {
            State = TimerState.Stopped;
            CurrentTime = TimeSpan.Zero;
            OnStopped();
            Stopped.Raise(this, EventArgs.Empty);
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Pause()
        {
            State = TimerState.Paused;
            Paused.Raise(this, EventArgs.Empty);
        }

        protected abstract void OnStopped();
        protected abstract void OnUpdate(GameTime gameTime);

        public void Update(GameTime gameTime)
        {
            if (State != TimerState.Started)
                return;

            CurrentTime += gameTime.ElapsedGameTime;
            OnUpdate(gameTime);
        }
    }
}