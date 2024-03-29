# Chip 8 Emulator in C#

![Alt text](screenshot.jpg?raw=true "Space Invaders")

A chip-8 emulator built in C#, project is .NET Framework 4.7.2. Included are four games with additional keybindings mapped for a more intuitive experience. 

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

Solution can be built in Visual Studio. You will need .NET Framework 4.7.2 to build the project. Games can be run from either the inbuilt games or external roms using the menu bar. 

## CPU clock design

Cpu runs at 540 Hz. A stopwatch keeps track of when a cpu cycle has passed and when a frame needs to be rendered (60Hz). 
