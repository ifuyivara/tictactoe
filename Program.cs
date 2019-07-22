using System;
using System.Collections.Generic;

namespace tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe. You will play against the Machine.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();

            // Initialize the board
            Board board = new Board();

            // We keep a loop until the board status indicates it has reached an ending
            // And we have not reached the max turns (9)
            int turn = 1;
            while (!board.GetBoardStatus() && turn <= 9) {
                
                // If turn is even, pick a random cell
                if (turn % 2 == 0) 
                {
                    board.MarkRandomCell();
                }
                else
                {
                    // If the turn is an odd number then it means it the player 1 turn
                    Console.WriteLine("Player 1 pick a position and press enter...");
                    try 
                    {
                        // Mark the picked cell with an X
                        int pick = int.Parse(Console.ReadLine());
                        board.SetBoardCell(pick, "X");
                    }
                    catch (Exception e)
                    {
                        // If there was a problem marking the cell restart the while without
                        // advancing a turn
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                
                // On each turn clear the board on the screen and print the new board
                Console.Clear();
                board.PrintBoard();
                turn++;  
            }

            // The board only gets a win status if it't not tied
            if (!board.GetBoardStatus()) 
            {
                Console.WriteLine("GameOver - Tied Game");
            }
            else
            {
                // else it means someone won. Whoever played the last turn is the winner
                string winner = turn % 2 == 0 ? "Player 1" : "The Machine";
                Console.WriteLine($"GameOver - Winner is: {winner}");
            }
        }
    }

    /*
     * This class that will control the board of the game.
     * A board holds an collection of cells that at any given momment 
     * can have values. A player wins when the values match a winning criteria
     */
    class Board
    {
        private Cell[] _board = new Cell[9];
        private List<int> _available = new List<int>();

        /*
         * Constructor, will reset the board and print it to the screen
         */
        public Board()
        {
            ResetBoard();
            PrintBoard();
        }

        /* 
         * Resets a board
         */
        public void ResetBoard()
        {
            _board = new Cell[9];
            _available = new List<int>();

            // We want to set al the board cells and also keep an avaliable list of cells
            // To be used when picking a random cell
            for (int i = 0; i < 9; i++) {
                _board[i] = new Cell(i);
                _available.Add(i);
            }
            
        }

        /*
         * Prints the current board to console
         */
        public void PrintBoard()
        {
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {_board[0].GetValue()} | {_board[1].GetValue()} | {_board[2].GetValue()} ");
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {_board[3].GetValue()} | {_board[4].GetValue()} | {_board[5].GetValue()}");
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {_board[6].GetValue()} | {_board[7].GetValue()} | {_board[8].GetValue()}");
            Console.WriteLine("   |   |   ");
        }

        /*
         * Determines the status of the board.
         * false = Board is tied 
         * true = Someone won
         */ 
        public bool GetBoardStatus()
        {
            // Are there any rows that are all equals?
            if (_board[0].GetValue() == _board[1].GetValue() && _board[1].GetValue() == _board[2].GetValue())
                return true;
            if (_board[3].GetValue() == _board[4].GetValue() && _board[4].GetValue() == _board[5].GetValue())
                return true;
            if (_board[6].GetValue() == _board[7].GetValue() && _board[7].GetValue() == _board[8].GetValue())
                return true;
            
            // Are there any columns that are all equals?
            if (_board[0].GetValue() == _board[3].GetValue() && _board[3].GetValue() == _board[6].GetValue())
                return true;
            if (_board[1].GetValue() == _board[4].GetValue() && _board[4].GetValue() == _board[7].GetValue())
                return true;
            if (_board[2].GetValue() == _board[5].GetValue() && _board[5].GetValue() == _board[8].GetValue())
                return true;
            
            // Are there any diagonals that are equal?
            if (_board[0].GetValue() == _board[4].GetValue() && _board[4].GetValue() == _board[8].GetValue())
                return true;
            if (_board[2].GetValue() == _board[4].GetValue() && _board[4].GetValue() == _board[6].GetValue())
                return true;

            // if not the board is tied
            return false;
        }

        /*
         * Marks a board cell with either X or 0
         */
        public void SetBoardCell(int cell, string value)
        {
            if (cell > 8 || cell < 0) {
                throw new System.ArgumentException("Position is outside of the board. Pick a different position...");
            }
            _board[cell].SetValue(value);
            _available.Remove(cell);
        }

        /*
         * Mark a random cell from the available cells
         */
        public void MarkRandomCell()
        {
            Random rnd = new Random();
            int position = rnd.Next(_available.Count);
            SetBoardCell(_available[position], "O");
        }
    }

    /*
     * A board cell
     */
    class Cell
    {
        private string _value = "";
        public int _index;

        /*
         * Constructor
         */
        public Cell(int index)
        {
            _index = index;
        }

        /*
         * Set the value of the cell, only if value is empty
         */
        public void SetValue(string value)
        {
            if (_value != "") 
            {
                throw new System.ArgumentException("Cell is already taken, pick another cell");
            }
            _value = value;
        }

        /*
         * Return the current value of the cell or it's index if no value is set
         */
        public string GetValue() 
        {
            return _value == "" ? $"{_index}" : _value;
        }
    }
}
