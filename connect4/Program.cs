using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace connect4 //Ignore commented out code please.
{
    //==========================Player Object==============================================
    public abstract class Player //changed to abstract to allow for an AI split dreived from these base attributes.
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
    //================================Human player object============================================
    public class HumanPlayer : Player //inherited from player, allows for a split to create AI from aswell.
    {
        public HumanPlayer(string name) : base(name)
        {
            Name = name;
            Wins = 0;
        }

    }
    //=================================AI Player Object===========================================
    public class AiPlayer : Player
    {
        public AiPlayer(string name) : base(name)
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
        public char[,] Array; 
        public Board()
        {
            Array = new char[Height, Width];
        }
        //===============================Board Creation method==========================================
        public void CreateBoard()// was printing twice so changed this method
        {
            {
                for (int i = 0; i < Array.GetLength(0); i++)
                {
                    for (int j = 0; j < Array.GetLength(1); j++)
                    {
                        Array[i, j] = '#';
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
            for (int i = 1; i < Width + 1; i++)
            {
                Console.Write(i + " "); //Prints the column number underneath the board
            }
            Console.WriteLine("   |");
        }

        public bool DropToken(int column, char token)
        {
            for (int row = Array.GetLength(0) - 1; row >= 0; row--)
            {
                if (Array[row, column] == '#')
                {
                    Array[row, column] = token; // places true if token is places
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
        private bool Win = false; //checks for a player winning
        private bool GameEnd = false; // singular flag to break the while loop that runs the game.

        public CreateGame(Board board, Player player1) // overloaded constructor for single player
        {
            Arena = board;
            Player1 = player1;
            Player2 = new AiPlayer("AI"); // not sure how to write that AI mode not ready and then loop back to the gamemanager
        }
        public CreateGame(Board board, Player player1, Player player2)
        {
            Arena = board;
            Player1 = player1;
            Player2 = player2;
        }

        private void GameStart() //Still trying to figure out how we want to do this part. We could pass the board to a turn object to make the moves and then send it back to a method in here?
        {
            List<Player> playerList = new List<Player>();

            playerList.Add(Player1);
            playerList.Add(Player2);

            playerList[0].Token = 'X';
            playerList[1].Token = 'O';

            Arena.CreateBoard();
            Arena.PrintBoard();
        }

        private void GamePlay() //drops tokens
        {
            List<Player> players = new List<Player> { Player1, Player2 };
            int currentPlayer = 0;//keeps track of wich player is playing
            GameEnd = false; //resets the game end flag so the game can be played again.

            while (!GameEnd) // will run until either a draw or win conditon(not yet implemented
            {   
                Player CurrentPlayer = players[currentPlayer];//checks whos turn it is
                Console.WriteLine($"\n{CurrentPlayer.Name}'s Turn (Token: {CurrentPlayer.Token})");//tells whos turn it is and what there token is
                Console.Write("Choose a column (1–7): ");

                string input = Console.ReadLine(); //reads player choice
                int column; //variable to hold collumn choice

                if (!int.TryParse(input, out column) || column <= 0 || column >= 8)// converts string to into integer. will fail if it can't be converted
                {                                                                 //or it the integer is outside of the 0-6 range
                    Console.WriteLine("Invalid column. Enter a number from 1 to 7.");
                    continue;// redoes the loop and asks the same player again
                }

                bool success = Arena.DropToken(column - 1, CurrentPlayer.Token);// drops token, will come back false if collumn is full
                if (!success)
                {
                    Console.WriteLine("Column is full. Try another one.");
                    continue;
                }
                //WIN CONDITIONS START HERE
                //DIAGONAL DOWN AND TO THE RIGHT WIN LOGIC
                for (int i = 0; i <= Arena.Array.GetLength(0) - 4; i++)//this part keeps both i + 3 and j + 3 in bounds
                {                                                      
                    for (int j = 0; j <= Arena.Array.GetLength(1) - 4; j++)
                    {
                        if (Arena.Array[i, j] == CurrentPlayer.Token &&
                            Arena.Array[i + 1, j + 1] == CurrentPlayer.Token &&
                            Arena.Array[i + 2, j + 2] == CurrentPlayer.Token &&
                            Arena.Array[i + 3, j + 3] == CurrentPlayer.Token)
                        {
                            Console.WriteLine("\n" + CurrentPlayer.Name + " Wins");
                            GameEnd = true;
                        }
                    }
                }

                // //DIAGONAL DOWN AND TO THE LEFT LOGIC
                for (int i = 0; i <= Arena.Array.GetLength(0) - 4; i++) //makes sure  j -3 doesn't go bolow column 0 and i + 3 stays smaller or equal to 5
                {
                    for (int j = 3; j < Arena.Array.GetLength(1); j++)
                    {
                        if (Arena.Array[i, j] == CurrentPlayer.Token &&
                            Arena.Array[i + 1, j - 1] == CurrentPlayer.Token &&
                            Arena.Array[i + 2, j - 2] == CurrentPlayer.Token &&
                            Arena.Array[i + 3, j - 3] == CurrentPlayer.Token)
                        {
                            Console.WriteLine("\n" + CurrentPlayer.Name + " Wins");
                            GameEnd = true;
                        }
                    }
                }

                // UP AND DOWN WIN LOGIC
                for (int i = 0; i <= Arena.Array.GetLength(0) - 4; i++)//makes sure i + 3 doesn't go above row index 5
                {
                    for (int j = 0; j < Arena.Array.GetLength(1); j++)
                    {
                        if (Arena.Array[i, j] == CurrentPlayer.Token &&
                            Arena.Array[i + 1, j] == CurrentPlayer.Token &&
                            Arena.Array[i + 2, j] == CurrentPlayer.Token &&
                            Arena.Array[i + 3, j] == CurrentPlayer.Token)
                        {
                            Console.WriteLine("\n" + CurrentPlayer.Name + " Wins");
                            GameEnd = true;
                        }
                    }
                }

                // LEFT AND RIGHT WIN LOGIC
                for (int i = 0; i < Arena.Array.GetLength(0); i++)
                {
                    for (int j = 0; j <= Arena.Array.GetLength(1) - 4; j++)//make sure j + 3 doesn't go above column index 6
                    {
                        if (Arena.Array[i, j] == CurrentPlayer.Token &&
                            Arena.Array[i, j + 1] == CurrentPlayer.Token &&
                            Arena.Array[i, j + 2] == CurrentPlayer.Token &&
                            Arena.Array[i, j + 3] == CurrentPlayer.Token)
                        {
                            Console.WriteLine("\n" + CurrentPlayer.Name + " Wins");
                            GameEnd = true;
                        }
                    }
                }


                //TIE GAME LOGIC
                int counter = 0;
                for(int i = 0; i < Arena.Array.GetLength(0); i++)
                {
                    for(int j = 0; j < Arena.Array.GetLength(1); j++)
                    {
                        if (Arena.Array[i,j] != '#')
                        {
                            counter++;
                        }                      
                    }
                }
                if(counter >= 42 && GameEnd != true) //tried a few different methods that didnt work. Ended up with this. Will only trigger tie game if a win condition hasnt occured
                {
                    GameEnd = true;
                    Console.WriteLine("\nThe game was a tie");
                }
                counter = 0;//resets the counter after every pass through the array, redundancy incase future code needs to be placed here

                Arena.PrintBoard();//reprints the board
                
                currentPlayer = 1 - currentPlayer;//switches turn, THIS IS A GENIUS PIECE OF CODE - J
            }            
        }
        //====================================GAME START LOGIC===============================
        public class GameManager//main logic goes here
        {
            public void Run()
            {
                Console.WriteLine("Welcome to Connect 4\nPlease input 2 for 2 player mode, or 'exit' to quit:\n(AI mode not implemented yet)");

                string command = Console.ReadLine();

                while (!command.Equals("exit", StringComparison.OrdinalIgnoreCase))// the EXIT and exit was giving soms problems, this way it ignores caps
                {
                    if(command == "1")
                    {
                        OnePlayerGame();
                    }
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

            private void OnePlayerGame()
            {
                Console.WriteLine("Sorry, AI player has not yet been implemented");
                Run();//repeats the game start question.
            }

            private void TwoPlayerGame()
            {
                Console.WriteLine("Please choose Player 1's name:");
                string player1 = Console.ReadLine();
                Player p1 = new HumanPlayer(player1); //Setting it up this way allows us to have a list composed of players or a player and AI 

                Console.WriteLine("Please choose Player 2's name:");
                string player2 = Console.ReadLine();
                Player p2 = new HumanPlayer(player2); 

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
