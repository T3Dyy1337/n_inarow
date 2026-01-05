
# N-In-A-Row (Tic-Tac-Toe Variant)

A console implementation of a generalized **N-in-a-row** game written in **C#**.

The player selects the board size (from **3×3 up to 30×30**) and the program automatically chooses how many marks in a row are required to win:

| Board size | Win condition |
|-----------:|--------------|
| 3×3        | 3 in a row |
| 4–9        | 4 in a row |
| 10+        | 5 in a row |

The game supports both **interactive play** and **loading predefined positions from a file**.

---

## ✨ Features

- Variable board size (3–30)
- Automatic “marks-to-win” logic
- Two-player local play (X vs O)
- Win detection in:
  - rows  
  - columns  
  - both diagonals
- “No possible win” detection (mathematically unwinnable states)
- Optional loading of positions from a text file
- Basic strategy hints for 3×3
- Clear console visualization of the board

---

## 🎮 How to Play (Interactive Mode)

Run the application and choose:
Enter 1 if you want to play and 0 if you want to load a position from a file

Select **1**.

You’ll then be asked to choose the board size, and the game will begin.

Players alternate as:

- **X** — Player 1  
- **O** — Player 2  

Enter coordinates when prompted:

Please enter where to place the X
Please enter the row (1-N)
Please enter the column (1-N)


The game ends when:

- a player gets N in a row  
- the board becomes full  
- no winning combination is mathematically possible

---


Boards are read from:

fields.txt


Each board uses:

- `X` — Player 1  
- `O` — Player 2  
- `.` — empty cell  

Example:

.    .    .

X    O    .

.    .    X


Multiple boards can be listed in the file, separated by blank lines.

After evaluating a board, the program lets you:

- **N** — load the next position  
- **Q** — quit

Invalid boards are detected automatically.

---

## 🧠 Implementation Notes (for Developers)

The engine includes:

- board validation & safe parsing
- move counter & player-turn tracking
- efficient win search in all directions
- “win still possible?” evaluation
- robust console input validation
- dynamic grid printing with padding

Key logic components:

- row / column / diagonal scanning  
- win detection functions  
- draw & impossibility detection  
- text-to-board parser  
- console renderer

The focus is correctness and gameplay logic rather than UI.

---

## 🛠️ Running the Project

Ensure **.NET SDK** is installed.

Clone and run:

```bash
git clone https://github.com/T3Dyy1337/n_inarow
cd n_inarow
dotnet run




