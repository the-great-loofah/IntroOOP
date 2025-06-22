using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace connect4 //Ignore commented out code please.
{
    //==========================Player Object==============================================
    public class Player
    {
        public string Name { get; set; }
        public int Wins;
        public char Token { get; set; } //assigned X or O in the game creation method.

        public Player(string name)
        {
            Name = name;
            Wins = 0;
        }
     

    }
    //================================Board Object================================================
    public class Board
    {
        private int Height = 6;
        private int Width = 7;
        public string[,] Array; 
        public Board()
        {
            Array = new string[Height, Width];
        }
        //===============================Board Creation method==========================================
        public void CreateBoard()// was printing twice so changed this method
        {
            {
                for (int i = 0; i < Array.GetLength(0); i++)
                {
                    for (int j = 0; j < Array.GetLength(1); j++)
                    {
                        Array[i, j] = "#";
                    }
                }
            }

        }
        //======Board print method=========================
        public void PrintBoard() //This method reprints the board after every move. board.PrintBoard should be invoked after EVERYMOVE
        {
            for (int i = 0; i < Height; i++) //Prints the board with the current values in the array                                 //| DEBUG|
            {
                Console.Write("|   "); //When the line starts, prints the board border
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(Array[i, j] + " ");//Adds a space between each cell
                }
                Console.Write("   |"); //When the line ends, print the board border
                Console.WriteLine(" ");
            }

            Console.Write("|   ");
            for (int i = 0; i < Width; i++)
            {
                Console.Write(i + " "); //Prints the column number underneath the board
            }
            Console.WriteLine("   |");
        }

        /*public void Turn()
        {
            if(PlayerTurn == true)                                      //|DEBUG|
            {
                PlayerCard = 'X';
                Console.WriteLine("Select a column");

                PlayerTurn = !PlayerTurn;
            }
            else
            {
                PlayerCard = 'O';
                Console.WriteLine("Select a column");
                PlayerTurn = !PlayerTurn;
            }
        }*/
        public bool DropToken(int column, char token)
        {
            for (int row = Array.GetLength(0) - 1; row >= 0; row--)
            {
                if (Array[row, column] == "#")
                {
                    Array[row, column] = token.ToString(); // places true if token is places
                    return true;
                }
            }
            return false; // returns false if collunm is full
        }
    }
    //============================================Game Creation and Gameplay============================================================
    public class CreateGame
    {
        private Board Arena;
        private Player Player1;
        private Player Player2;
        public CreateGame(Board board, Player player1, Player player2)
        {
            Arena = board;
            Player1 = player1;
            Player2 = player2;
        }

        public void GameStart() //Still trying to figure out how we want to do this part. We could pass the board to a turn object to make the moves and then send it back to a method in here?
        {
            List<Player> playerList = new List<Player>();

            playerList.Add(Player1);
            playerList.Add(Player2);

            playerList[0].Token = 'X';
            playerList[1].Token = 'O';

            Arena.CreateBoard();
            Arena.PrintBoard();
        }

        public void GamePlay() //drops tokens
        {
            List<Player> players = new List<Player> { Player1, Player2 };
            int currentPlayer = 0;//keeps track of wich player is playing

            while (true) // will run until either a draw or win conditon(not yet implemented
            {
                Player CurrentPlayer = players[currentPlayer];//checks whos turn it is
                Console.WriteLine($"\n{CurrentPlayer.Name}'s Turn (Token: {CurrentPlayer.Token})");//tells whos turn it is and what there token is
                Console.Write("Choose a column (0–6): ");

                string input = Console.ReadLine(); //reads player choice
                int column; //variable to hold collumn choice

                if (!int.TryParse(input, out column) || column < 0 || column >= 7)// converts string to into integer. will fail if it can't be converted
                {                                                                 //or it the integer is outside of the 0-6 range
                    Console.WriteLine("Invalid column. Enter a number from 0 to 6.");
                    continue;// redoes the loop and asks the same player again
                }

                bool success = Arena.DropToken(column, CurrentPlayer.Token);// drops token, will come back false if collumn is full
                if (!success)
                {
                    Console.WriteLine("Column is full. Try another one.");
                    continue;
                }
                Arena.PrintBoard();//reprints the board
                currentPlayer = 1 - currentPlayer;//switches turn
            }
        }
        //====================================MAIN LOGIC===============================
        public class GameManager//main logic goes here
        {
            public void Run()
            {
                Console.WriteLine("Welcome to Connect 4\nPlease input 2 for 2 player mode, or 'exit' to quit:\n(AI mode not implemented yet)");

                string command = Console.ReadLine();

                while (!command.Equals("exit", StringComparison.OrdinalIgnoreCase))// the EXIT and exit was giving soms problems, this way it ignores caps
                {
                    if (command == "2")
                    {
                        TwoPlayerGame();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input! Please type '2' to play or 'exit' to quit.");
                    }

                    Console.WriteLine("\nType '2' to play again or 'exit' to quit:");
                    command = Console.ReadLine();
                }

                Console.WriteLine("\nThanks for playing!");
            }

            private void TwoPlayerGame()
            {
                Console.WriteLine("Please choose Player 1's name:");
                string player1 = Console.ReadLine();
                Player p1 = new Player(player1);

                Console.WriteLine("Please choose Player 2's name:");
                string player2 = Console.ReadLine();
                Player p2 = new Player(player2);

                Board board = new Board();
                CreateGame game = new CreateGame(board, p1, p2);

                game.GameStart();
                game.GamePlay(); // 
            }
        }
        //=================================================================MAIN==========================================================================
        internal class Program
        {
            static void Main(string[] args)
            {
                GameManager manager = new GameManager();// moved logic into class GameManager
                manager.Run();
            }

        }
    }
}
