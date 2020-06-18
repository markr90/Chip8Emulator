using System;
using Chip8Emulator.Architecture;

namespace Chip8Emulator.Core
{
    class Clock
    {
        private System.Diagnostics.Stopwatch aStopwatch = new System.Diagnostics.Stopwatch();
        readonly TimeSpan targetElapsedTime60Hz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / InternalTimer.Frequency);
        readonly TimeSpan targetElapsedTimeCPU;
        TimeSpan lastTime;
        TimeSpan lastTimeCPU;

        int cpuFreq;

        public Clock(int cpuClockFrequency)
        {
            cpuFreq = cpuClockFrequency;
            targetElapsedTimeCPU = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / cpuFreq);
        }

        public void Start()
        {
            aStopwatch.Start();
        }

        public void Reset()
        {
            aStopwatch.Reset();
            lastTime = TimeSpan.Zero;
            lastTimeCPU = TimeSpan.Zero;
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

        public bool HasCycleElapsed()
        {
            TimeSpan currentTime = aStopwatch.Elapsed;
            var elapsed = currentTime - lastTimeCPU;
            if (elapsed >= targetElapsedTimeCPU)
            {
                lastTimeCPU += targetElapsedTimeCPU;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
