using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{

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
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(7, 6);
            board.CreateBoard();
        }
    }
}
