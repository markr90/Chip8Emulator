using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Chip8Emulator.Architecture;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using Chip8Emulator.Games;

namespace Chip8Emulator.Core
{
    public partial class Emulator : Form
    {
        byte[] ROM;
        Bitmap bootImage;
        Dictionary<Keys, byte> keyMapping;

        CancellationTokenSource cancellationSource;

        private const int Clockspeed = 540; // 540 Hz gives exactly 9 clock cycles per frame
        CPU cpu = new CPU(Clockspeed);
        private Thread cpuThread;
        Clock clock = new Clock(Clockspeed);

        public Emulator()
        {
            keyMapping = defaultKeyMap;
            InitializeComponent();
            InitBootImage();
            pbScreen.Image = bootImage;

            string[] filenames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Console.WriteLine("{0}", string.Join(",", filenames));

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
            cpu.Initialize();
            LoadRom();
            cancellationSource = new CancellationTokenSource();
            clock.Start();
            cpuThread = new Thread(() => RunLoop(cancellationSource.Token));
            cpuThread.Start();
        }

        private void Stop()
        {
            cancellationSource?.Cancel();
            cpu.Stop();
            clock.Reset();
        }

        private void Restart()
        {
            Stop();
            Start();
        }

        private void LoadRom()
        {
            try
            {
                cpu.LoadRom(ROM);
            }
            catch
            {
                MessageBox.Show("ERROR: Couldn't load rom.",
                    "Rom load error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void RunLoop(CancellationToken token)
        {
            if (!cpu.HasRomLoaded)
            {
                return;
            }
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                RunCpuCycle();
            }
        }

        private void RunCpuCycle()
        {
            if (clock.HasCycleElapsed())
            {
                cpu.RunCycle();
            }
            if (clock.HasFrameElapsed())
            {
                // timers and graphics need to be rendered at 60 Hz, should be outside the cpu loop
                this.Invoke(new Action(() => cpu.Timer.UpdateTimers()));
                this.Invoke(new Action(() => Render()));
            }
        }

        private void Render() => this.pbScreen.Image = cpu.Graphics.Draw();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to exit?",
                                     "Confirm exit",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Stop();
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
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ROM = File.ReadAllBytes(openFileDialog.FileName);
                    keyMapping = new Dictionary<Keys, byte>(defaultKeyMap);
                    Restart();
                }
                catch
                {

                }
            }
        }

        private void OverrideKeyMap(Dictionary<Keys, byte> newMap)
        {
            keyMapping = new Dictionary<Keys, byte>(defaultKeyMap);
            foreach (KeyValuePair<Keys, byte> kv in newMap)
            {
                if (keyMapping.ContainsKey(kv.Key))
                    keyMapping[kv.Key] = kv.Value;
                else
                    keyMapping.Add(kv.Key, kv.Value);
            }
        }

        private void Restart(IGame game)
        {
            ROM = game.RomData();
            OverrideKeyMap(game.GetKeyOverride());
            Restart();
        }

        private void tankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart(new Tank());
        }

        private readonly Dictionary<Keys, byte> defaultKeyMap = new Dictionary<Keys, byte>
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

        private void tetrisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart(new Tetris());
        }

        private void spaceInvadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart(new Invaders());
        }

        private void brixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart(new Brix());
        }
    }
}
