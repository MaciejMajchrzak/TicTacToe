using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TicTacToe
{
    public class TicTacToeConsole
    {
        public TicTacToeConsole()
        {
            MenuLoop();     
        }

        private void MenuLoop()
        {
            while(true)
            {
                ShowMenuOption();

                Console.WriteLine();
                Console.Write("  Option: ");

                if (int.TryParse(Console.ReadLine(), out int resultFirst) == true) 
                {
                    if (resultFirst == 1) { Console.Clear(); PlayerVsPlayer(); }
                    if (resultFirst == 2) { Console.Clear(); PlayerVsComputer(); }
                    if (resultFirst == 3) { Console.Clear(); ComputerVsComputer(); }
                    if (resultFirst == 4) { Console.Clear(); ShowScoreTable(); }
                    if (resultFirst == 5) { Console.Clear(); Environment.Exit(0); }
                }

                Console.Clear();
            }
        }

        private void PlayerVsPlayer()
        {
            List<string> board = GetClearBoard();

            Console.Write("  First player name: ");

            string playerOne = Console.ReadLine();

            Console.Write("  Seccond player name: ");

            string playerTwo = Console.ReadLine();

            Console.Clear();

            for (int i = 1; i < 11; i++)
            {
                if (new TicTacToeRepository().BoardWin(board) == false)
                {
                    ShowBoard(board);

                    Console.WriteLine();
                    Console.WriteLine($"  Turn {new TicTacToeRepository().WhoseTurn(board)}");

                    int chose = CheckPlayer(board);

                    Console.Clear();

                    if (chose != 0)
                    {
                        if (i % 2 != 0) { board[chose - 1] = "o"; }
                        if (i % 2 == 0) { board[chose - 1] = "x"; }
                        ShowBoard(board);
                        Console.Clear();
                    }
                    else { ShowBoard(board); Console.WriteLine(); ShowScore("draw", playerOne, playerTwo); }
                }
                else { ShowBoard(board); Console.WriteLine(); ShowScore($"{new TicTacToeRepository().WhoWin(board) }", playerOne, playerTwo); break; }
            }
        }

        private void PlayerVsComputer()
        {
            List<string> board = GetClearBoard();

            Console.Write("  Player name: ");

            string player = Console.ReadLine();

            Console.Clear();

            for (int i = 1; i < 11; i++)
            {
                if (new TicTacToeRepository().BoardWin(board) == false)
                {
                    int chose;

                    if(i % 2 == 0)
                    {
                        ShowBoard(board);

                        Console.WriteLine();
                        Console.WriteLine($"  Turn {new TicTacToeRepository().WhoseTurn(board)}");

                        chose = CheckPlayer(board);

                        Console.Clear();
                    }
                    else { chose = new TicTacToeRepository().Brain(board); }


                    if (chose != 0)
                    {
                        if (i % 2 != 0) { board[chose - 1] = "o"; }
                        if (i % 2 == 0) { board[chose - 1] = "x"; }
                    }
                    else { ShowBoard(board); Console.WriteLine(); ShowScore("draw", "Computer", player); }
                }
                else { ShowBoard(board); Console.WriteLine(); ShowScore($"{new TicTacToeRepository().WhoWin(board) }", "Computer", player); break; }
            }
        }

        private void ComputerVsComputer()
        {
            List<string> board = GetClearBoard();

            for (int i = 1; i < 11; i++)
            {
                if (new TicTacToeRepository().BoardWin(board) == false)
                {
                    int chose = new TicTacToeRepository().Brain(board);
                    if (chose != 0)
                    {
                        if (i % 2 != 0) { board[chose - 1] = "o"; }
                        if (i % 2 == 0) { board[chose - 1] = "x"; }
                        ShowBoard(board);
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else { ShowBoard(board); Console.WriteLine(); ShowScore("draw", "Computer One", "Computer Two"); }
                }
                else { ShowBoard(board); Console.WriteLine(); ShowScore($"{new TicTacToeRepository().WhoWin(board) }", "Computer One", "Computer Two"); break; }
            }
        }

        private void ShowScoreTable()
        {
            if(File.Exists("score.txt") == true)
            {
                string[] scoreFile = File.ReadAllLines("score.txt");

                Console.WriteLine("     Lp             Player 1             Player 2");
                Console.WriteLine("  ===== ==================== ====================");

                int count = 0;

                foreach(var row in scoreFile)
                {
                    count++;

                    Console.Write($"{count.ToString().PadLeft(7,' ')}");
                    
                    if (row.Split(';')[2] == "o") { Console.ForegroundColor = ConsoleColor.Green; }
                    if (row.Split(';')[2] == "x") { Console.ForegroundColor = ConsoleColor.Red; }
                    if (row.Split(';')[2] == "draw") { Console.ForegroundColor = ConsoleColor.Yellow; }

                    Console.Write($"{row.Split(';')[0].PadLeft(21,' ')}");

                    Console.ResetColor();

                    if (row.Split(';')[2] == "x") { Console.ForegroundColor = ConsoleColor.Green; }
                    if (row.Split(';')[2] == "o") { Console.ForegroundColor = ConsoleColor.Red; }
                    if (row.Split(';')[2] == "draw") { Console.ForegroundColor = ConsoleColor.Yellow; }

                    Console.WriteLine($"{row.Split(';')[1].PadLeft(20,' ')}");

                    Console.ResetColor();
                }
            }

            Console.ReadKey();
        }

        private void ShowScore(string score, string playerOne, string playerTwo)
        {
            if (score == "o") { File.AppendAllText("score.txt", $"{playerOne};{playerTwo};{score}\n"); }
            if (score == "draw") { File.AppendAllText("score.txt", $"{playerTwo};{playerOne};{score}\n"); }

            Console.ForegroundColor = ConsoleColor.Green;

            if (score == "draw") { Console.WriteLine("  Draw"); }
            else { Console.WriteLine($"  Win {score}"); }

            Console.ResetColor();

            Console.ReadKey();
        }

        private int CheckPlayer(List<string> board)
        {
            while (true)
            {
                Console.Write("  Coordinate: ");

                if (int.TryParse(Console.ReadLine(), out int chose) == true && chose >= 1 && chose <= 9) { if (board[chose - 1] == "") { return chose; } }

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("  Wrong chose");

                Console.ResetColor();
            }
        }

        private List<string> GetClearBoard()
        {
            List<string> board = new List<string>();

            for (int i = 0; i < 9; i++) { board.Add(""); }

            return board;
        }

        private void ShowBoard(List<string> board)
        {
            Console.WriteLine($"  Coordinates Current game");
            Console.WriteLine($"  =========== ============");
            Console.WriteLine($"   { ShowBoardItems(board[0]) } | { ShowBoardItems(board[1]) } | { ShowBoardItems(board[2]) }   1 | 2 | 3");
            Console.WriteLine($"  ---+---+--- ---+---+---");
            Console.WriteLine($"   { ShowBoardItems(board[3]) } | { ShowBoardItems(board[4]) } | { ShowBoardItems(board[5]) }   4 | 5 | 6");
            Console.WriteLine($"  ---+---+--- ---+---+---");
            Console.WriteLine($"   { ShowBoardItems(board[6]) } | { ShowBoardItems(board[7]) } | { ShowBoardItems(board[8]) }   7 | 8 | 9");
        }

        private string ShowBoardItems(string boardItem)
        {
            if (boardItem == "") { return " "; }
            return boardItem;
        }

        private void ShowMenuOption()
        {
            Console.WriteLine("         Options");
            Console.WriteLine("  ========================");
            Console.WriteLine("  1 - Player vs Player");
            Console.WriteLine("  2 - Player vs Computer");
            Console.WriteLine("  3 - Computer vs Computer");
            Console.WriteLine("  4 - Scores");
            Console.WriteLine("  5 - Exit");
        }
    }
}
