using System;
using Chip8Emulator.Architecture;

namespace Chip8Emulator.Core
{
    class Clock
    {
        private System.Diagnostics.Stopwatch aStopwatch = new System.Diagnostics.Stopwatch();
        readonly TimeSpan targetElapsedTime60Hz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / InternalTimer.Frequency);
        TimeSpan lastTime;

        public void Start()
        {
            aStopwatch.Start();
        }

        public void Reset()
        {
            aStopwatch.Reset();
            lastTime = TimeSpan.Zero;
        }

        public bool HasFrameElapsed()
        {
            TimeSpan currentTime = aStopwatch.Elapsed;
            var elapsed = currentTime - lastTime;
            if (elapsed >= targetElapsedTime60Hz)
            {
                lastTime += targetElapsedTime60Hz;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
