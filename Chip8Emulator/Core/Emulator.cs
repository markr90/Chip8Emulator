using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Chip8Emulator.Architecture;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;

namespace Chip8Emulator.Core
{
    public partial class Emulator : Form
    {
        private string ROM = @"../../../Roms/IBM_Logo.ch8";
        Bitmap bootImage;

        private const int Clockspeed = 540; // 540 Hz gives exactly 9 clock cycles per frame
        CPU cpu = new CPU(Clockspeed);
        private Thread cpuThread;
        Clock clock = new Clock();
        private bool _isRunning = false;

        public Emulator()
        {
            InitializeComponent();
            InitBootImage();
            pbScreen.Image = bootImage;

            cpu = new CPU(Clockspeed);
            cpu.Initialize();

            KeyDown += SetKeyDown;
            KeyUp += SetKeyUp;
        }

        private void InitBootImage()
        {
            Stream bootimage = Assembly.GetExecutingAssembly().GetManifestResourceStream("Chip8Emulator.Chip8Logo.bmp");
            bootImage = new Bitmap(bootimage);
        }

        private void SetKeyDown(object sender, KeyEventArgs e)
        {
            if (keyMapping.ContainsKey(e.KeyCode))
                cpu.KeyDown(keyMapping[e.KeyCode]);
        }

        private void SetKeyUp(object sender, KeyEventArgs e)
        {
            if (keyMapping.ContainsKey(e.KeyCode))
                cpu.KeyUp(keyMapping[e.KeyCode]);
        }
        
        private void Start()
        {
            _isRunning = true;
            clock.Start();
            cpuThread = new Thread(RunLoop);
            cpuThread.Start();
        }

        private void Stop()
        {
            clock.Stop();
            _isRunning = false;
        }

        private void Restart()
        {
            Stop();
            cpu.Initialize();
            LoadRom();
            Start();
        }

        private void LoadRom()
        {
            try
            {
                cpu.LoadRom(File.ReadAllBytes(ROM));
            }
            catch
            {
                MessageBox.Show("ERROR: Couldn't load rom.",
                    "Rom load error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void RunLoop()
        {
            if (!cpu.HasRomLoaded)
            {
                return;
            }
            while (_isRunning)
            {
                RunCpuCycle();
            }
        }

        private void RunCpuCycle()
        {
            if (clock.HasFrameElapsed())
            {
                cpu.RunCycle();
                cpu.Graphics.Draw(pbScreen);
            }
        }

        private Dictionary<Keys, byte> keyMapping = new Dictionary<Keys, byte> 
        {
            { Keys.D1, 0x1 },
            { Keys.D2, 0x2 },
            { Keys.D3, 0x3 },
            { Keys.D4, 0xC },
            { Keys.Q, 0x4 },
            { Keys.W, 0x5 },
            { Keys.E, 0x6 },
            { Keys.R, 0xD },
            { Keys.A, 0x7 },
            { Keys.S, 0x8 },
            { Keys.D, 0x9 },
            { Keys.F, 0xE },
            { Keys.Z, 0xA },
            { Keys.X, 0x0 },
            { Keys.C, 0xB },
            { Keys.V, 0xF },
        };

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to exit?",
                                     "Confirm exit",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to restart?",
                                     "Restart exit",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Restart();
            }
        }

        private void loadRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Title = "Browse ROM files";
            openFileDialog.DefaultExt = "ch8";
            openFileDialog.Filter = "Rom Files|*.ch8";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ROM = openFileDialog.FileName;
                Restart();
            }
        }
    }
}
