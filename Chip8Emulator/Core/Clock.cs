using System;
using Chip8Emulator.Architecture;

namespace Chip8Emulator.Core
{
    class Clock
    {
        private System.Diagnostics.Stopwatch aStopwatch = new System.Diagnostics.Stopwatch();

        public void Start()
        {
            aStopwatch.Start();
        }

        public void Stop()
        {
            aStopwatch.Reset();
        }

        public void Reset()
        {
            aStopwatch.Reset();
        }

        public bool HasFrameElapsed()
        {
            TimeSpan ts = aStopwatch.Elapsed;
            if (ts.Milliseconds >= 1000.0 / InternalTimer.Frequency)
            {
                aStopwatch.Reset();
                aStopwatch.Start();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
