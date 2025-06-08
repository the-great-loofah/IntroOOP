using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{   public class Player
    {
        public Player(string player1name)
        {

        }

        public Player(string player1name, string player2name)
        {

        }

        public void CreatePlayer(string number)
        {
            if (number == "1")
            {

            }
            else if (number == "2")
            {

            }
            else
            {
                Console.WriteLine("Invalid player count");
                return;
            }
        }
    }

    public class Board
    {
        public int Height;
        public int Width;
        public string[,] Array;
        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            Array = new string[Width + 1, Height + 1];

        }

        public void CreateBoard()
        {   
            for (int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    Array[i, j] = "#";
                }
            }

            for (int i = 0; i < Height; i++)
            {
                Console.Write("|   ");
                for(int j = 0; j < Width; j++)
                {
                    Console.Write(Array[i, j] + " ");
                }
                Console.Write("   |");
                Console.WriteLine(" ");
            }
        }

        public void Choice(int row, int column)
        {

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string i;
            Board board = new Board(7, 6);
            board.CreateBoard();
            Console.WriteLine("How many players, 1 or 2?");
        }
    }
}
