# Chip 8 Emulator in C#

A chip-8 emulator built in C# (sound not implemented). Included are four games with additional keybindings mapped for a more intuitive experience. 

- Tetris
- Space invaders
- Brix
- Tank

Spacebar is related to shooting, arrow keys are for moving. The usual keybinding map is used for any externally loaded ROMs.

```
1 2 3 4
Q W E R
A S D F
Z X C V
```

## How to use

Solution can be built in Visual Studio. Games can be run from either the inbuilt games or external roms using the menu bar.

## CPU clock design

Cpu runs at 540 Hz. A stopwatch is used to render the images at 60 Hz. Every framerate cycle the CPU will calculate 540 / 60 = 9 cpu cycles. 