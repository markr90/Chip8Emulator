﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Emulator.Architecture
{
    public class InternalTimer
    {
        public const int Frequency = 60;
        private byte Delay;
        private byte Sound;

        public void SetDelay(byte value)
        {
            Delay = value;
        }

        public byte GetDelay()
        {
            return Delay;
        }

        public void SetSound(byte value)
        {
            Sound = value;
        }

        public byte GetSound(byte value)
        {
            return Sound;
        }

        public void UpdateTimers()
        {
            if (Delay > 0)
                Delay--;
            if (Sound > 0)
                Sound--;
        }
    }
}
