using System;
using System.Linq;


SetupGame SetupGame = new SetupGame();
RunGame GameState = new RunGame(SetupGame.playerOne, SetupGame.playerTwo);


public class SetupGame
{
    public string playerOne { get; private set; }
    public string playerTwo { get; private set; }

    public SetupGame()
    {
        SetNames();
        CreateBoard();
        
        void SetNames()
        {
            Console.WriteLine("Player 1: ");
            Console.ForegroundColor = ConsoleColor.Red;
            playerOne = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Clear();

            Console.WriteLine("Player 2: ");
            Console.ForegroundColor = ConsoleColor.Red;
            playerTwo = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Clear();
        }

        void CreateBoard()
        {
            Console.WriteLine("This will be the board:");
            Console.WriteLine("");
            Console.WriteLine($"---|---|---");
            Console.WriteLine($"---|---|---");
            Console.WriteLine($"---|---|---");
            Console.WriteLine("");
            Console.WriteLine("Press any key to start the game:");
            Console.ReadKey(true);

            Console.Clear();
        }
    }
}

public class RunGame
{
    private int round = 0;
    public int inputOne { get; private set; } = 0;
    public int inputTwo { get; private set; } = 0;

    public int[] rounds { get; private set; } = new int[9];
    public int[] inputs { get; private set; } = new int[9];
    public string[] progress { get; private set; } = new string[9];

    public RunGame(string playerOne, string playerTwo)
    {
        progress[0] = "-";
        progress[1] = "-";
        progress[2] = "-";
        progress[3] = "-";
        progress[4] = "-";
        progress[5] = "-";
        progress[6] = "-";
        progress[7] = "-";
        progress[8] = "-";

        while (true)
        {
            round++;

            if (round > 9)
            {
                Console.Clear();
                break;
            }
            else if (round % 2 != 0) //check for even or odd numbers to determine player 1/2 --------------------------
            {
                // ---------------- Display round and whos turn it is ---------------------------------------------

                rounds[round - 1] = round;
                Console.WriteLine($"It's round {rounds[round - 1]}");
                Console.WriteLine($"its {playerOne}'s turn");

                // ---------------- Check if it's the the first turn ----------------------------------------------
                // ---------------- If not, display what the other player chose the previous round ----------------

                if (inputs[0] > 0)
                {
                    Console.WriteLine($"{playerTwo} chose {inputs[round - 2]}");
                }

                // ---------------- Let player choose a value ----------------------------------------------------

                Console.WriteLine($"Choose a number");
                Console.ForegroundColor = ConsoleColor.Red;
                inputOne = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Gray;

                // ---------------- Check if the value is already used -------------------------------------------
                // ---------------- Loop untill valid value is picked --------------------------------------------

                while (inputs.Contains(inputOne))
                {
                    Console.WriteLine($"Sorry, {inputOne} has already been chosen");
                    Console.WriteLine($"Choose again: ");

                    Console.ForegroundColor = ConsoleColor.Red;
                    inputOne = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                // ---------------- Assign value to array for this round -----------------------------------------

                inputs[round - 1] = inputOne;
                Console.Clear();

                // ---------------- Display board to player -----------------------------------------------------

                DisplayBoard(playerOne, round);

                // ---------------- Check if there is a winner --------------------------------------------------

                if (CheckForWinner(playerOne, "X") == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.ReadKey(true);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                //-----------------------------------------------------------------------------------------------
                // ---------------- Repeat for the other player--------------------------------------------------

            }
            else
            {
                rounds[round - 1] = round;

                Console.WriteLine($"It's round {rounds[round - 1]}");
                Console.WriteLine($"its {playerTwo}'s turn");
                Console.WriteLine($"{playerOne} chose {inputs[round - 2]}");

                Console.WriteLine($"Choose a number");
                Console.ForegroundColor = ConsoleColor.Red;
                inputTwo = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Gray;

                while (inputs.Contains(inputTwo))
                {
                    Console.WriteLine($"Sorry, {inputTwo} has already been chosen");
                    Console.WriteLine($"Choose again: ");

                    Console.ForegroundColor = ConsoleColor.Red;
                    inputTwo = Convert.ToInt32(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                inputs[round - 1] = inputTwo;
                Console.Clear();

                DisplayBoard(playerTwo, round);

                if (CheckForWinner(playerTwo, "O") == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.ReadKey(true);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
            }

            Console.WriteLine("-------------------");
        }

        void DisplayBoard(string player, int round)
        {
            string mark;

            //---------------- Assign a "X" or an "O" depending on the player ------------------------------------

            if (player == playerOne)
            {
                mark = "X";
            }
            else
            {
                mark = "O";
            }

            //Each time the method is called it checks if the input value equals a spot on the baord -------------
            //Assigns a "X" or a "O" to that spot in the array depending on the user that calls the method -------

            if (inputs[round - 1] == 1)
            {
                progress[0] = mark;
            }

            if (inputs[round - 1] == 2)
            {
                progress[1] = mark;
            }

            if (inputs[round - 1] == 3)
            {
                progress[2] = mark;
            }

            if (inputs[round - 1] == 4)
            {
                progress[3] = mark;
            }

            if (inputs[round - 1] == 5)
            {
                progress[4] = mark;
            }

            if (inputs[round - 1] == 6)
            {
                progress[5] = mark;
            }

            if (inputs[round - 1] == 7)
            {
                progress[6] = mark;
            }

            if (inputs[round - 1] == 8)
            {
                progress[7] = mark;
            }

            if (inputs[round - 1] == 9)
            {
                progress[8] = mark;
            }

            //---------------- After checking all spots on the board display the board -------------------------

            Console.WriteLine($"-{progress[6]}-|-{progress[7]}-|-{progress[8]}-");
            Console.WriteLine($"-{progress[3]}-|-{progress[4]}-|-{progress[5]}-");
            Console.WriteLine($"-{progress[0]}-|-{progress[1]}-|-{progress[2]}-");
            Console.WriteLine("-------------");
        }

        bool CheckForWinner(string player, string mark)
        {
            //each time a player calls this method it checks for the "X" or "O" assigned to that player----------
            //Than check if any of the conditions are met and return true/false to end/continue the game --------

            if (progress[0] == mark && progress[1] == mark && progress[2] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[3] == mark && progress[4] == mark && progress[5] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[6] == mark && progress[7] == mark && progress[8] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[0] == mark && progress[3] == mark && progress[6] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[1] == mark && progress[4] == mark && progress[7] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[2] == mark && progress[5] == mark && progress[8] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[0] == mark && progress[4] == mark && progress[8] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }

            if (progress[2] == mark && progress[4] == mark && progress[6] == mark)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player} has won the game!!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("-------------------");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}



//---------------- Below is check to see if the arrays are filled correctly -------------------------------

//foreach (int _rounds in rounds)
//{
    //Console.ForegroundColor = ConsoleColor.Green;
    //Console.WriteLine($"Round {_rounds}: Number: {inputs[_rounds-1]} Mark: {progress[_rounds-1]}");
    //Console.ForegroundColor = ConsoleColor.Gray;
//}

//--------------------------------------------------------------------------------------------------------

