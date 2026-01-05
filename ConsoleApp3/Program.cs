using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    internal class Program
    {
        public static string fileName = @"C:\\Users\\juric\\Documents\\ConsoleApp3older\\ConsoleApp3\\fields.txt";
        //returns the amount of moves played
        static int MoveCounter(char[,] ticTac)
        {
            int allSpots = ticTac.Length;
            int moveCounter = allSpots - AllEmptySpots(ticTac); ;
            return moveCounter;

        }

        //checks if a win is possible from the current position
        static bool WinPossible(char[,] ticTac, ref byte inRowToWin)
        {
            int player;
            List<char> win;

            if (MoveCounter(ticTac) % 2 == 0) { player = 1; } else { player = 0; }
            int requiredSpots;

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    win = CheckLRDiagonals(ticTac, ref inRowToWin);
                }
                else if (i == 1)
                {
                    win = CheckRLDiagonals(ticTac, ref inRowToWin);
                }
                else if (i == 2)
                {
                    win = CheckCols(ticTac, ref inRowToWin);
                }
                else
                {
                    win = CheckRows(ticTac, ref inRowToWin);
                }
                int count = win.Count(x => x == '.');

                requiredSpots = 0;
                if (count == inRowToWin)
                {
                    requiredSpots = (count * 2) - player;
                    if (AllEmptySpots(ticTac) >= requiredSpots)
                    {
                        return true;
                    }
                }
                else
                {
                    count = win.Count(x => x == 'X');
                    if (count > 0)
                    {
                        requiredSpots = ((inRowToWin - count) * 2) - player;
                        if (AllEmptySpots(ticTac) >= requiredSpots)
                        {
                            return true;
                        }
                    }
                    count = win.Count(x => x == 'O');
                    if (count > 0)
                    {
                        requiredSpots = ((inRowToWin - count) * 2) - player;
                        if (AllEmptySpots(ticTac) >= requiredSpots)
                        {
                            return true;
                        }
                    }
                }
                count = 0;
            }

            return false;
        }

        //returns the amount of empty spots on the board
        static int AllEmptySpots(char[,] ticTac)
        {
            byte spotCounter = 0;
            for (int i = 0; i < ticTac.GetLength(0); i++)
            {
                for (int j = 0; j < ticTac.GetLength(1); j++)
                {
                    if (ticTac[i, j] == '.') { spotCounter++; }
                }
            }
            return spotCounter;
        }
        //recommends a few starting moves for 3x3 tic tac toe
        static void RecommendedMove(char[,] ticTac, ref int num)
        {
            if (ticTac.Length == 9)
            {
                if (num == 1)
                {
                    if (ticTac[1, 1] == 'X')
                    {
                        Console.WriteLine("Place the O into one of the corners.");
                        Console.WriteLine();
                    }
                    if (ticTac[0, 0] == 'X' || ticTac[0, 2] == 'X' || ticTac[2, 0] == 'X' || ticTac[2, 2] == 'X')
                    {
                        Console.WriteLine("Place the O into the center.");
                        Console.WriteLine();
                    }
                }
                if (num == 0)
                {
                    Console.WriteLine("The best starting move is either the center or any of the corners.");
                    Console.WriteLine();
                }
                if (num == 2)
                {
                    if (ticTac[1, 1] == 'X' && (ticTac[0, 0] == 'O' || ticTac[0, 2] == 'O' || ticTac[2, 0] == 'O' || ticTac[2, 2] == 'O'))
                    {
                        Console.WriteLine("Any move draws.");
                        Console.WriteLine();
                    }
                    else if (ticTac[1, 1] == '0' && (ticTac[0, 0] == 'X' || ticTac[0, 2] == 'X' || ticTac[2, 0] == 'X' || ticTac[2, 2] == 'X'))
                    {
                        Console.WriteLine("Any move draws.");
                        Console.WriteLine();
                    }
                    else { Console.WriteLine("Any move wins."); Console.WriteLine(); }
                }
            }
        }

        //searches for a winning sequence on a row
        static int SearchRow(char[,] ticTac, ref int row, ref int col, ref byte inRowToWin)
        {
            char temp = ticTac[row, col];
            char pointer;
            int counter = 1;

            //check row

            int i;
            //checks left from starting point
            if (col != 0)
            {
                i = col - 1;
                while (i >= 0)
                {
                    pointer = ticTac[row, i];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i--;
                    }
                    else { i = -1; }
                    temp = pointer;
                }
            }

            //checks right from starting point
            if (col != ticTac.GetLength(1) - 1)
            {
                i = col + 1;
                temp = ticTac[row, col];
                while (i < ticTac.GetLength(1))
                {
                    pointer = ticTac[row, i];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i++;
                    }
                    else { i = ticTac.GetLength(1); }
                }
            }
            return counter;
        }

        //searches for a winning sequence in a column
        static int SearchCol(char[,] ticTac, ref int row, ref int col, ref byte inRowToWin)
        {
            char temp = ticTac[row, col];
            char pointer;
            int counter = 1;

            int i;
            if (row != 0)
            {
                i = row - 1;
                while (i >= 0)
                {
                    pointer = ticTac[i, col];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i--;
                    }
                    else { i = -1; }
                    temp = pointer;
                }
            }

            if (row != ticTac.GetLength(0))
            {
                i = row + 1;
                temp = ticTac[row, col];
                while (i < ticTac.GetLength(0))
                {
                    pointer = ticTac[i, col];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i++;
                    }
                    else { i = ticTac.GetLength(0); }
                }
            }
            return counter;
        }

        //searches for a winning sequence on a top left to bottom right diagonal
        static int SearchMajorDiagonal(char[,] ticTac, ref int row, ref int col, ref byte inRowToWin)
        {
            char temp = ticTac[row, col];
            char pointer;
            int counter = 1;

            if (row == 0 && col == ticTac.GetLength(1) - 1) { return counter; }
            if (row == ticTac.GetLength(0) - 1 && col == 0) { return counter; }

            int i;
            int j;
            if (row != 0 || col != 0)
            {
                i = row - 1;
                j = col - 1;
                while (i >= 0 && j >= 0)
                {
                    pointer = ticTac[i, j];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i--;
                        j--;
                    }
                    else { i = -1; }
                    temp = pointer;
                }
            }

            if (row != ticTac.GetLength(0) - 1 || col != ticTac.GetLength(1) - 1)
            {
                i = row + 1;
                j = col + 1;
                temp = ticTac[row, col];
                while (i < ticTac.GetLength(0) && j < ticTac.GetLength(1))
                {
                    pointer = ticTac[i, j];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i++;
                        j++;
                    }
                    else { i = ticTac.GetLength(0); }
                }
            }
            return counter;
        }

        //searches for a winning sequence on a top right to bottome left diagonal
        static int SearchMinorDiagonal(char[,] ticTac, ref int row, ref int col, ref byte inRowToWin)
        {
            char temp = ticTac[row, col];
            char pointer;
            int counter = 1;

            if (row == 0 && col == 0) { return counter; }
            if (row == ticTac.GetLength(0) - 1 && col == ticTac.GetLength(1) - 1) { return counter; }

            int i;
            int j;
            if (row != 0 || col != ticTac.GetLength(1) - 1)
            {
                i = row - 1;
                j = col + 1;
                while (i >= 0 && j < ticTac.GetLength(1))
                {
                    pointer = ticTac[i, j];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i--;
                        j++;
                    }
                    else { i = -1; }
                    temp = pointer;
                }
            }

            if (row != ticTac.GetLength(0) - 1 || col != ticTac.GetLength(1) - 1)
            {
                i = row + 1;
                j = col - 1;
                temp = ticTac[row, col];
                while (i < ticTac.GetLength(0) && j >= 0)
                {
                    pointer = ticTac[i, j];
                    if (pointer == temp)
                    {
                        counter++;
                        if (counter == inRowToWin) { return counter; }
                        i++;
                        j--;
                    }
                    else { i = ticTac.GetLength(0); }
                }
            }
            return counter;
        }

        //searches the whole array by top left to bottom right diagonals and returns a list of a place where a winning sequence is possible
        //returns an empty list if not
        static List<char> CheckLRDiagonals(char[,] ticTac, ref byte inRowToWin)
        {
            //check diagonals left to right

            //declare and initialize variables
            List<char> chars = new List<char>();
            char temp = '0';
            char pointer;
            int counter;
            int runCounter = 0;
            int row = 0;
            int col = ticTac.GetLength(1);
            int tempRow = 0;
            int tempCol = ticTac.GetLength(1);
            int emptyCounter = 0;
            bool cont;
            byte fullRunCounter = 0;

            while (fullRunCounter < (ticTac.GetLength(0) * ticTac.GetLength(1)))
            {
                cont = true;
                counter = 0;
                if (tempCol == 0)
                {
                    tempCol = 1;
                    if (tempRow < ticTac.GetLength(0) - 1)
                    {
                        tempRow++;
                    }
                }
                else
                {
                    col--;
                    tempCol = col;
                }

                while (((col < ticTac.GetLength(1) && col > -1) || row < ticTac.GetLength(0)) && cont == true)
                {
                    pointer = ticTac[row, col];
                    if (pointer == 'X' || pointer == 'O')
                    {
                        if (runCounter == 0)
                        {
                            counter = 0;
                        }
                        if ((runCounter > 0 && pointer == temp) || runCounter == 0)
                        {
                            temp = pointer;
                            chars.Add(pointer);
                            counter++;
                        }
                        else
                        {
                            temp = pointer;
                            if (chars.Count(x => x == '.') != chars.Count)
                            {
                                chars.Clear();
                            }
                            chars.Add(pointer);
                            counter = 1;
                        }
                        runCounter++;
                    }
                    else
                    {
                        emptyCounter++;
                        temp = pointer;
                        chars.Add(pointer);
                    }
                    if (emptyCounter == inRowToWin)
                    {
                        return chars;
                    }
                    if ((counter + emptyCounter) == inRowToWin)
                    {
                        return chars;
                    }
                    if (col == ticTac.GetLength(1) - 1 || row == ticTac.GetLength(0) - 1)
                    {
                        if (row == ticTac.GetLength(0) - 1 && col == ticTac.GetLength(1) - 1)
                        {
                            tempRow = 1;
                            tempCol = 0;
                        }
                        cont = false;
                    }
                    else
                    {
                        col++;
                        row++;
                    }
                }
                fullRunCounter++;
                chars.Clear();
                runCounter = 0;
                emptyCounter = 0;
                if (row == ticTac.GetLength(0) - 1 && col == 0)
                {
                    runCounter = (ticTac.GetLength(0) * 2);
                }
                row = tempRow;
                col = tempCol;
            }
            chars.Clear();
            return chars;
        }

        //searches the whole array by top right to bottom left diagonals a list of a place where a winning sequence is possible
        //returns an empty list if not
        static List<char> CheckRLDiagonals(char[,] ticTac, ref byte inRowToWin)
        {
            List<char> chars = new List<char>();
            char temp = '0';
            char pointer;
            int runCounter = 0;
            int row = 0;
            int tempRow = 0;
            int counter;
            int tempCol = 0;
            int col = -1;
            int emptyCounter = 0;
            bool cont;
            byte fullRunCounter = 0;

            //check diagonals right to left
            while (fullRunCounter < (ticTac.GetLength(0) * ticTac.GetLength(1)))
            {
                cont = true;
                counter = 0;
                if (tempCol == ticTac.GetLength(1) - 1)
                {
                    tempCol = ticTac.GetLength(1) - 1;
                    if (tempRow < ticTac.GetLength(0) - 1)
                    {
                        tempRow++;
                    }
                }
                else
                {
                    col++;
                    tempCol = col;
                }

                while ((col >= 0 || row < ticTac.GetLength(0)) && cont == true)
                {
                    pointer = ticTac[row, col];
                    if (pointer == 'X' || pointer == 'O')
                    {
                        if (runCounter == 0)
                        {
                            counter = 0;
                        }
                        if ((runCounter > 0 && pointer == temp) || runCounter == 0)
                        {
                            temp = pointer;
                            chars.Add(pointer);
                            counter++;
                        }
                        else
                        {
                            temp = pointer;
                            if (chars.Count(x => x == '.') != chars.Count)
                            {
                                chars.Clear();
                            }
                            chars.Add(pointer);
                            counter = 1;
                        }
                        runCounter++;
                    }
                    else
                    {
                        emptyCounter++;
                        temp = pointer;
                        chars.Add(pointer);
                    }
                    if (emptyCounter == inRowToWin)
                    {
                        return chars;
                    }
                    if ((counter + emptyCounter) == inRowToWin)
                    {
                        return chars;
                    }
                    if (col == 0 || row == ticTac.GetLength(0) - 1)
                    {
                        if (row == ticTac.GetLength(0) - 1 && col == 0)
                        {
                            tempRow = 1;
                            tempCol = ticTac.GetLength(1) - 1;
                        }
                        cont = false;
                    }
                    else
                    {
                        col--;
                        row++;
                    }
                }
                fullRunCounter++;
                chars.Clear();
                runCounter = 0;
                emptyCounter = 0;
                if (row == ticTac.GetLength(0) - 1 && col == ticTac.GetLength(1) - 1)
                {
                    runCounter = (ticTac.GetLength(0) * 2);
                }
                row = tempRow;
                col = tempCol;
            }
            chars.Clear();
            return chars;
        }

        //searches the whole array by columns and returns a list of a place where a winning sequence is possible
        //returns an empty list if not
        static List<char> CheckCols(char[,] ticTac, ref byte inRowToWin)
        {
            List<char> chars = new List<char>();
            char temp = '0';
            char pointer;
            int counter;
            int runCounter = 0;
            int emptyCounter = 0;

            //check cols
            for (int i = 0; i < ticTac.GetLength(1); i++)
            {
                counter = 0;
                for (int j = 0; j < ticTac.GetLength(0); j++)
                {
                    pointer = ticTac[j, i];
                    if (pointer == 'X' || pointer == 'O')
                    {
                        if (runCounter == 0)
                        {
                            counter = 0;
                        }
                        if ((runCounter > 0 && pointer == temp) || runCounter == 0)
                        {
                            temp = pointer;
                            chars.Add(pointer);
                            counter++;
                        }
                        else
                        {
                            temp = pointer;
                            if (chars.Count(x => x == '.') != chars.Count)
                            {
                                chars.Clear();
                            }
                            chars.Add(pointer);
                            counter = 1;
                        }
                    }
                    else
                    {
                        emptyCounter++;
                        temp = pointer;
                        chars.Add(pointer);
                    }
                    if (emptyCounter == inRowToWin)
                    {
                        return chars;
                    }
                    if ((counter + emptyCounter) == inRowToWin)
                    {
                        return chars;
                    }
                    runCounter++;
                }
                chars.Clear();
                emptyCounter = 0;
                runCounter = 0;
            }
            chars.Clear();
            return chars;
        }

        //searches the whole array by rows and returns a list of a place where a winning sequence is possible
        //returns an empty list if not
        static List<char> CheckRows(char[,] ticTac, ref byte inRowToWin)
        {
            List<char> chars = new List<char>();
            char temp = '0';
            char pointer;
            int counter;
            int runCounter = 0;
            int emptyCounter;

            //check rows
            for (int i = 0; i < ticTac.GetLength(0); i++)
            {
                emptyCounter = 0;
                counter = 0;
                for (int j = 0; j < ticTac.GetLength(1); j++)
                {
                    pointer = ticTac[i, j];
                    if (pointer == 'X' || pointer == 'O')
                    {
                        if (runCounter == 0)
                        {
                            counter = 0;
                        }
                        if ((runCounter > 0 && pointer == temp) || runCounter == 0)
                        {
                            temp = pointer;
                            chars.Add(pointer);
                            counter++;
                        }
                        else
                        {
                            temp = pointer;
                            if (chars.Count(x => x == '.') != chars.Count)
                            {
                                chars.Clear();
                            }
                            chars.Add(pointer);
                            counter = 1;
                        }
                    }
                    else
                    {
                        emptyCounter++;
                        temp = pointer;
                        chars.Add(pointer);
                    }
                    if (emptyCounter == inRowToWin)
                    {
                        return chars;
                    }
                    if ((counter + emptyCounter) == inRowToWin)
                    {
                        return chars;
                    }
                    runCounter++;
                }
                chars.Clear();
                runCounter = 0;
            }
            chars.Clear();
            return chars;
        }

        //checks whether there is a winner
        static bool CheckWin(char[,] ticTac, ref byte inRowToWin, ref int row, ref int col)
        {
            if (SearchRow(ticTac, ref row, ref col, ref inRowToWin) == inRowToWin)
            {
                return true;
            }
            if (SearchCol(ticTac, ref row, ref col, ref inRowToWin) == inRowToWin)
            {
                return true;
            }
            if (SearchMajorDiagonal(ticTac, ref row, ref col, ref inRowToWin) == inRowToWin)
            {
                return true;
            }
            if (SearchMinorDiagonal(ticTac, ref row, ref col, ref inRowToWin) == inRowToWin)
            {
                return true;
            }
            return false;
        }

        //prints out the array
        static void PrintArray(char[,] ticTac, ref byte paddingSize)
        {
            Console.Write("{0," + paddingSize + "}", "");
            for (int i = 0; i < ticTac.GetLength(1); i++)
            {
                Console.Write("{0," + paddingSize + "}", i + 1);
            }
            Console.WriteLine();

            for (int i = 0; i < ticTac.GetLength(0); i++)
            {
                Console.Write("{0," + -paddingSize + "}", i + 1);

                for (int j = 0; j < ticTac.GetLength(1); j++)
                {
                    Console.Write("{0," + paddingSize + "}", ticTac[i, j]);
                }
                Console.WriteLine();

            }
        }

        //sets all the array elements to .
        static void InitializeArray(char[,] ticTac, ref byte paddingSize)
        {
            Console.WriteLine("Player one is X and player two is O.");
            for (int i = 0; i < ticTac.GetLength(0); i++)
            {
                for (int j = 0; j < ticTac.GetLength(1); j++)
                {
                    ticTac[i, j] = '.';
                }
            }
        }

        //checks if the array has an empty spot or not
        static bool ArrayFull(char[,] ticTac)
        {
            for (int i = 0; i < ticTac.GetLength(0); i++)
            {
                for (int k = 0; k < ticTac.GetLength(1); k++)
                {
                    if (ticTac[i, k] == '.')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //checks if the users input is in range
        static bool IsInRange(int topRange, ref int num)
        {
            return (num >= 0 && num < topRange);
        }

        //sets the size of the field based on user input
        static int FieldSizeSet()
        {
            bool inputCor = false;
            int fieldSize = 0;

            while (!inputCor)
            {
                try
                {
                    Console.WriteLine("Please enter the desired size of the playing field (3-30).");
                    fieldSize = int.Parse(Console.ReadLine());
                    if (fieldSize >= 3 && fieldSize < 31)
                    {
                        inputCor = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("you did not enter an int");
                }
            }
            return fieldSize;
        }

        // places the stone based on user input; returns win is someone has won
        static bool UserInput(char[,] ticTac, ref char stone, ref byte inRowToWin)
        {
            bool win = false;
            bool spotEmpty = true;
            int row = 0;
            int col = 0;

            //ask for user input and check whether it is valid or not, then print the array
            while (spotEmpty)
            {
                try
                {
                    Console.WriteLine("Please enter where to place the " + stone);
                    Console.WriteLine("Please enter the row (1-" + ticTac.GetLength(0) + ")");
                    row = int.Parse(Console.ReadLine()) - 1;
                    Console.WriteLine("Please enter the column (1-" + ticTac.GetLength(1) + ")");
                    col = int.Parse(Console.ReadLine()) - 1;
                }
                catch (FormatException)
                {
                    Console.WriteLine("you did not enter an int");
                    continue;
                }
                if (IsInRange(ticTac.GetLength(1), ref col) && IsInRange(ticTac.GetLength(0), ref row))
                {
                    if (ticTac[row, col] == '.')
                    {
                        ticTac[row, col] = stone;
                        spotEmpty = false;
                    }
                    else
                    {
                        Console.WriteLine("That spot is occupied");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Row or column was outside specified range.");
                    continue;
                }
                win = CheckWin(ticTac, ref inRowToWin, ref row, ref col);


            }
            return win;
        }

        //sets the amount of stones required in a row to win based on the board size
        static byte SetInRowToWin(ref int fieldSize)
        {
            if (fieldSize < 4)
            {
                Console.WriteLine("Three in a row to win.");
                return 3;
            }
            else if (fieldSize >= 4 && fieldSize < 10)
            {
                Console.WriteLine("Four in a row to win.");
                return 4;
            }
            else
            {
                Console.WriteLine("Five in a row to win.");
                return 5;
            }
        }

        static bool FileEmpty(string fileName)
        {
            if (new FileInfo(fileName).Length == 0) { return true; }

            if (new FileInfo(fileName).Length < 6)
            {
                var contents = File.ReadAllText(fileName);
                return contents.Length == 0;
            }
            return false;
        }


        //sets the board to input from a file
        static char[,] FileInput(string[] lines, ref int skipLines, string fileName)
        {
            //@"C:\\Users\\sinner\\Documents\\Tadeáš Juříček\\CIA\\druhyProgram\\ConsoleApp3older\\ConsoleApp3\\ConsoleApp3\\fields.txt"
            //@"C:\\Users\\juric\\Documents\\ConsoleApp3older\\ConsoleApp3\\fields.txt"

            int length = 0;
            int lineCounter = 0;
            int o = 0;
            if (skipLines == 0)
            {
                o = skipLines;
            }
            else
            {
                o = skipLines + 1;
            }

            int index = o;

            while (!string.IsNullOrEmpty(lines[index]))
            {
                lineCounter++;
                index++;
            }
            string[] board = lines.Skip(o).Take(lineCounter).ToArray();
            if (o+3 <= lines.Length)
            {
                length = board[0].Trim().Replace(" ", "").Length;
            }
            else
            {
                length = 0;
            }
                    


            int col = length;
            char[,] ticTac = new char[lineCounter, col];
            byte pad = 3;
            
            if (FileEmpty(fileName))
            {
                Console.WriteLine("File is empty");
                return ticTac;
            }
            if (col < 3 || col > 30)
            {
                if (o + 3 >= lines.Length)
                {
                    Console.WriteLine("There are no more positions");
                }
                else if (col < 3)
                {
                    Console.WriteLine("The board is too small");
                }
                else
                {
                    Console.WriteLine("The board is too big");
                }
                return ticTac;
            }

            int i = 0; int j;

            foreach (var rows in board)
            {
                j = 0;
                foreach (var cols in rows.Trim().Split(' '))
                {
                    if (cols != "")
                    {
                        char val = char.Parse(cols.Trim());
                        if (val != 'X')
                        {
                            if (val != 'O')
                            {
                                if (val != '.')
                                {
                                    val = '0';
                                }
                            }
                        }
                        ticTac[i, j] = val;
                        j++;
                    }
                }
                if (i < lineCounter - 1)
                {
                    i++;
                }
            }

            PrintArray(ticTac, ref pad);
            return ticTac;
        }

        //counts the number of occurances of a specified value on the board
        static int CountValue(char[,] ticTac, ref char searchChar)
        {
            int count = 0;
            for (int i = 0; i < ticTac.GetLength(0); i++)
            {
                for (int j = 0; j < ticTac.GetLength(1); j++)
                {
                    if (ticTac[i, j] == searchChar) { count++; }
                }
            }
            return count;
        }

        private static FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

        static void Main()
        {
            //declare and initialize variables
            byte paddingSize = 3;
            bool win = false;
            int num = 0;
            byte inRowToWin = 0;
            bool noWin = true;
            char[,] ticTac;
            string userInput = "";
            bool inputCor = false;
            char emptySpot = '\0';
            bool fileInput = true;
            char wrongChar = '0';
            int skipLines = 0;
            bool mainLoop = false;
            bool nextPos = true;
            
            string[] lines = new string[File.ReadAllLines(fileName).Count()];

            while (!inputCor)
            {
                Console.WriteLine("Enter 1 if you want to play and 0 if you want to load a position from a file");
                userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "0")
                {
                    inputCor = true;
                }

            }
            if (userInput == "0")
            {
                using (var reader = new StreamReader(fs, Encoding.UTF8, true, 4096, leaveOpen: true))
                {
                    int o = 0;
                    while (!reader.EndOfStream)
                    {
                        lines[o] = reader.ReadLine();
                        o++;
                    }
                }
                ticTac = FileInput(lines, ref skipLines, fileName);
                if (CountValue(ticTac, ref emptySpot) > 0)
                {
                    fileInput = false;
                }
                else if (CountValue(ticTac, ref wrongChar) > 0)
                {
                    fileInput = false;
                }
                else
                {
                    int len = Math.Min(ticTac.GetLength(0), ticTac.GetLength(1));
                    skipLines += Math.Max(ticTac.GetLength(0), ticTac.GetLength(1));
                    inRowToWin = SetInRowToWin(ref len);
                    noWin = WinPossible(ticTac, ref inRowToWin);
                    num = MoveCounter(ticTac);
                }
            }
            else
            {
                mainLoop = true;
                //set the size of the field and how many in a row are needed to win
                int fieldSize = FieldSizeSet();
                ticTac = new char[fieldSize, fieldSize];
                inRowToWin = SetInRowToWin(ref fieldSize);
                InitializeArray(ticTac, ref paddingSize);
            }
            PrintArray(ticTac, ref paddingSize);
            if (mainLoop)
            {
                //main game loop
                while (win == false && noWin == true)
                {
                    RecommendedMove(ticTac, ref num);

                    //makes the game a draw after fieldSize * fieldSize turns (board is full) or when there is no winning combination
                    if (!ArrayFull(ticTac))
                    {
                        if (!ArrayFull(ticTac))
                        {
                            Console.WriteLine("The whole board is occupied and the game is a draw.");
                        }
                    }

                    //switches the stone
                    char stone;
                    if (num % 2 == 0)
                    {
                        stone = 'X';
                    }
                    else
                    {
                        stone = 'O';
                    }

                    //asks user for input and places the stone on the users input coordinates, checks for win
                    win = UserInput(ticTac, ref stone, ref inRowToWin);
                    noWin = WinPossible(ticTac, ref inRowToWin);
                    PrintArray(ticTac, ref paddingSize);
                    num++;

                }
            }
            else
            {
                if (fileInput)
                {
                    while (nextPos)
                    {
                        while (!win && noWin)
                        {

                            //makes the game a draw after fieldSize * fieldSize turns (board is full) or when there is no winning combination
                            if (!ArrayFull(ticTac))
                            {
                                if (!ArrayFull(ticTac))
                                {
                                    Console.WriteLine("The whole board is occupied and the game is a draw.");
                                }
                            }

                            //switches the stone
                            char stone;
                            if (num % 2 == 0)
                            {
                                stone = 'X';
                            }
                            else
                            {
                                stone = 'O';
                            }

                            //asks user for input and places the stone on the users input coordinates, checks for win
                            noWin = WinPossible(ticTac, ref inRowToWin);
                            if (noWin)
                            {
                                win = UserInput(ticTac, ref stone, ref inRowToWin);
                            }

                            PrintArray(ticTac, ref paddingSize);
                            num++;
                        }

                        if (win)
                        {
                            int player = ((num + 1) % 2) + 1;
                            Console.WriteLine("Player " + player + " is the winner");

                        }
                        if (!noWin)
                        {
                            Console.WriteLine("There is no possible winning combination");

                        }

                        string userChoice = "";
                        bool validChoice = false;
                        while (!validChoice)
                        {
                            Console.WriteLine("If you want to enter in another position press N, if you want to quit press Q");
                            userChoice = Console.ReadLine();

                            if (userChoice.ToLower().Equals("q") || userChoice.ToLower().Equals("n"))
                            {
                                validChoice = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice");
                            }
                        }
                        if (userChoice.ToLower().Equals("n"))
                        {

                            Console.WriteLine("Loading next position...");
                            ticTac = FileInput(lines, ref skipLines, fileName);
                            skipLines += Math.Max(ticTac.GetLength(0), ticTac.GetLength(1));
                            skipLines++;
                            win = false;
                            noWin = true;
                        }
                        else
                        {
                            nextPos = false;
                        }
                    }
                }
            }

            //prints which player won
            if (win)
            {
                int player = ((num + 1) % 2) + 1;
                Console.WriteLine("Player " + player + " is the winner");
            }
            
            if (!noWin)
            {
                Console.WriteLine("There is no possible winning combination");
            }
            if (!fileInput)
            {
                Console.WriteLine("The file input was invalid");
            }
            Console.WriteLine("PRESS ANY KEY TO END");
            Console.ReadKey();
        }
    }
}