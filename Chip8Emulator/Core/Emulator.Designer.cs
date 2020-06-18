namespace Chip8Emulator.Core
{
    partial class Emulator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private PictureBoxWithInterpolationMode pbScreen;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tetrisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spaceInvadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbScreen = new Chip8Emulator.Core.PictureBoxWithInterpolationMode();
            this.brixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(640, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRomToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadRomToolStripMenuItem
            // 
            this.loadRomToolStripMenuItem.Name = "loadRomToolStripMenuItem";
            this.loadRomToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.loadRomToolStripMenuItem.Text = "Browse rom";
            this.loadRomToolStripMenuItem.Click += new System.EventHandler(this.loadRomToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tankToolStripMenuItem,
            this.tetrisToolStripMenuItem,
            this.spaceInvadersToolStripMenuItem,
            this.brixToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // tankToolStripMenuItem
            // 
            this.tankToolStripMenuItem.Name = "tankToolStripMenuItem";
            this.tankToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tankToolStripMenuItem.Text = "Tank";
            this.tankToolStripMenuItem.Click += new System.EventHandler(this.tankToolStripMenuItem_Click);
            // 
            // tetrisToolStripMenuItem
            // 
            this.tetrisToolStripMenuItem.Name = "tetrisToolStripMenuItem";
            this.tetrisToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tetrisToolStripMenuItem.Text = "Tetris";
            this.tetrisToolStripMenuItem.Click += new System.EventHandler(this.tetrisToolStripMenuItem_Click);
            // 
            // spaceInvadersToolStripMenuItem
            // 
            this.spaceInvadersToolStripMenuItem.Name = "spaceInvadersToolStripMenuItem";
            this.spaceInvadersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.spaceInvadersToolStripMenuItem.Text = "Space Invaders";
            this.spaceInvadersToolStripMenuItem.Click += new System.EventHandler(this.spaceInvadersToolStripMenuItem_Click);
            // 
            // pbScreen
            // 
            this.pbScreen.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pbScreen.Location = new System.Drawing.Point(0, 24);
            this.pbScreen.Name = "pbScreen";
            this.pbScreen.Size = new System.Drawing.Size(640, 320);
            this.pbScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbScreen.TabIndex = 0;
            this.pbScreen.TabStop = false;
            // 
            // brixToolStripMenuItem
            // 
            this.brixToolStripMenuItem.Name = "brixToolStripMenuItem";
            this.brixToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.brixToolStripMenuItem.Text = "Brix";
            this.brixToolStripMenuItem.Click += new System.EventHandler(this.brixToolStripMenuItem_Click);
            // 
            // Emulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 344);
            this.Controls.Add(this.pbScreen);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Emulator";
            this.Text = "Chip 8 Emulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tetrisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spaceInvadersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brixToolStripMenuItem;
    }
}