# Battleship

This is the Battleship game seed .NET Core project. You should use it as a starting point for implementing Flare's take-home exercise

This project is cross-platform and supports Mac OS / Windows / Linux.

## Prerequisites

1. Install [.NET Core SDK](https://dotnet.microsoft.com/download)
2. Install [Visual Studio Code](https://code.visualstudio.com/)
3. Open this project in VS Code and accept the prompt to install recommended extensions

## Creating .NET project

You can use various dotnet CLI commands to set up and run a new project, e.g.

```
dotnet new console
dotnet run
```

Follow the information in the [official guide](https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code)

For more details about CLI, refer to the [official documentation for CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x).

## Install

```
dotnet new console
```

### Instructions

| Operation                      | Instruction                                                                     |
| ------------------------------ | ------------------------------------------------------------------------------- |
| Add a player                   | `Player <Name>`                                                                 |
| Add a battle ship for a player | `Ship <Name> <x1> <y1> <x2> <y2> (see example 2 below)`                         |
| Start the game                 | `Start (Should be called when all players has put their ships on their boards)` |
| Attack a player's battle ship  | `Attack <Name> <x> <y>`                                                         |
| End the game                   | `End`                                                                           |

### How to play

1. Read sample games from `.txt` files

```
$ dotnet run < sample-game.txt
```

**sample-game.txt**

```
Player A                    # Add player with name A
Ship A 1 1 1 1              # Add a ship at (1,1) for A
Ship A 0 0 0 0              # Add a ship at (0,0) for A
Ship A 2 3 7 8              # Add a ship at [(2,3), (7,8)] for A
Player B                    # Add player with name B
Ship B 5 5 5 5              # Add a ship at (5,5) for B
Player C                    # Add player with name B
Ship C 6 6 6 6              # Add a ship at (6,6) for C
Start                       # All players put their ships on boards and game started. Not able to add more players and ships from now on.
Attack B 5 5                # A's turn and Attack B's ship at (5,5)
Attack A 0 0                # C's turn as B has lost, attack A's ship at (0,0)
Attack C 6 6                # A's turn and Attack B's ship at (6,6). A wins.
```

2. Type the instructions to `stdin`

Use `game.showBoardSwitcher()` to show the game board in terminal.

```
$ dotnet run
> Game stated! Please give instructions!    // Game is ready to go
> Player A                                  // type correct instructions
> Ship A 1 1 2 2
> Player B
> Ship B 2 2 2 2
> Start
Current Player: A
> Attack B 1 1
Current Board of Player: A
(The board would looks like [0->empty, 1->ship, 2->damaged]:)
(1,1) is the top left point and the (2,2) is the bottom down point of the ship we added before for a
0 0 0 0 0 0 0 0 0 0
0 1 1 0 0 0 0 0 0 0
0 1 1 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
Attack to B at (1,1) missed
Current Player: B
>...
```
