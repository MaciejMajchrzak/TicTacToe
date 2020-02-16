using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class TicTacToeRepository
    {
        public int Brain(List<string> board)
        {
            string myTurn = "";
            string opponetTurn = "";

            if(DataCorrectness(board) == true)
            {
                myTurn = WhoseTurn(board);

                if (myTurn == "o") { opponetTurn = "x"; }
                if (myTurn == "x") { opponetTurn = "o"; }

                if (WinOrDontLose(board, myTurn) == 0)
                {
                    if (WinOrDontLose(board, opponetTurn) == 0)
                    {
                        return RandomFreeField(board);
                    }
                    else { return WinOrDontLose(board, opponetTurn); }
                }
                else { return WinOrDontLose(board, myTurn); }
            }

            return 0;
        }

        private bool DataCorrectness(List<string> board)
        {
            int o = 0;
            int x = 0;

            if(board.Count == 9)
            {
                if(BoardWin(board) == false)
                {
                    foreach (var item in board)
                    {
                        if (item == "o") { o++; }
                        if (item == "x") { x++; }
                        if (item != "o" && item != "x" && item != "") { return false; }
                    }

                    if (o == 5 && x == 4) { return false; }
                    if (o == x || o > x) { return true; }
                }
            }

            return false;
        }

        public bool BoardWin(List<string> board)
        {
            if ((board[0] == "o" || board[0] == "x") && (board[0] == board[1] && board[0] == board[2])) { return true; } // left right
            if ((board[3] == "o" || board[3] == "x") && (board[3] == board[4] && board[3] == board[5])) { return true; } // left right
            if ((board[6] == "o" || board[6] == "x") && (board[6] == board[7] && board[6] == board[8])) { return true; } // left right

            if ((board[0] == "o" || board[0] == "x") && (board[0] == board[3] && board[0] == board[6])) { return true; } // up down
            if ((board[1] == "o" || board[1] == "x") && (board[1] == board[4] && board[1] == board[7])) { return true; } // up down
            if ((board[2] == "o" || board[2] == "x") && (board[2] == board[5] && board[2] == board[8])) { return true; } // up down

            if ((board[0] == "o" || board[0] == "x") && (board[0] == board[4] && board[0] == board[8])) { return true; } // first cross 
            if ((board[2] == "o" || board[2] == "x") && (board[2] == board[4] && board[2] == board[6])) { return true; } // seccond cross 

            return false;
        }

        public string WhoWin(List<string> board)
        {
            if ((board[0] == "o" || board[0] == "x") && (board[0] == board[1] && board[0] == board[2])) { if (board[0] == "o") { return "o"; } return "x"; } // left right
            if ((board[3] == "o" || board[3] == "x") && (board[3] == board[4] && board[3] == board[5])) { if (board[3] == "o") { return "o"; } return "x"; } // left right
            if ((board[6] == "o" || board[6] == "x") && (board[6] == board[7] && board[6] == board[8])) { if (board[6] == "o") { return "o"; } return "x"; } // left right

            if ((board[0] == "o" || board[0] == "x") && (board[0] == board[3] && board[0] == board[6])) { if (board[0] == "o") { return "o"; } return "x"; } // up down
            if ((board[1] == "o" || board[1] == "x") && (board[1] == board[4] && board[1] == board[7])) { if (board[1] == "o") { return "o"; } return "x"; } // up down
            if ((board[2] == "o" || board[2] == "x") && (board[2] == board[5] && board[2] == board[8])) { if (board[2] == "o") { return "o"; } return "x"; } // up down

            if ((board[0] == "o" || board[0] == "x") && (board[0] == board[4] && board[0] == board[8])) { if (board[0] == "o") { return "o"; } return "x"; } // first cross 
            if ((board[2] == "o" || board[2] == "x") && (board[2] == board[4] && board[2] == board[6])) { if (board[2] == "o") { return "o"; } return "x"; } // seccond cross 

            return "";
        }

        public string WhoseTurn(List<string> board)
        {
            int o = 0;
            int x = 0;

            foreach(var item in board)
            {
                if (item == "o") { o++; }
                if (item == "x") { x++; }
            }

            if (o == x) { return "o"; }
            if (o > x) { return "x"; }

            return "";
        }

        private int WinOrDontLose(List<string> board, string whoseTurn)
        {
            if (board[0] == whoseTurn && board[3] == whoseTurn && board[6] == "") { return 7; } // up to down
            if (board[1] == whoseTurn && board[4] == whoseTurn && board[7] == "") { return 8; } // up to down
            if (board[2] == whoseTurn && board[5] == whoseTurn && board[8] == "") { return 9; } // up to down

            if (board[6] == whoseTurn && board[3] == whoseTurn && board[0] == "") { return 1; } // down to up
            if (board[7] == whoseTurn && board[4] == whoseTurn && board[1] == "") { return 2; } // down to up
            if (board[8] == whoseTurn && board[5] == whoseTurn && board[2] == "") { return 3; } // down to up

            if (board[0] == whoseTurn && board[1] == whoseTurn && board[2] == "") { return 3; } // left to right
            if (board[3] == whoseTurn && board[4] == whoseTurn && board[5] == "") { return 6; } // left to right
            if (board[6] == whoseTurn && board[7] == whoseTurn && board[8] == "") { return 9; } // left to right

            if (board[2] == whoseTurn && board[1] == whoseTurn && board[0] == "") { return 1; } // right to left
            if (board[5] == whoseTurn && board[4] == whoseTurn && board[3] == "") { return 4; } // right to left
            if (board[8] == whoseTurn && board[7] == whoseTurn && board[6] == "") { return 7; } // right to left

            if (board[0] == whoseTurn && board[4] == whoseTurn && board[8] == "") { return 9; } // up left to down rigth

            if (board[6] == whoseTurn && board[4] == whoseTurn && board[2] == "") { return 3; } // down left to up rigth

            if (board[2] == whoseTurn && board[4] == whoseTurn && board[6] == "") { return 7; } // up right to down left

            if (board[8] == whoseTurn && board[4] == whoseTurn && board[0] == "") { return 1; } // down right to up left

            if (board[0] == whoseTurn && board[6] == whoseTurn && board[3] == "") { return 4; } // column
            if (board[1] == whoseTurn && board[7] == whoseTurn && board[4] == "") { return 5; } // column
            if (board[2] == whoseTurn && board[8] == whoseTurn && board[5] == "") { return 6; } // column

            if (board[0] == whoseTurn && board[2] == whoseTurn && board[1] == "") { return 2; } // row
            if (board[3] == whoseTurn && board[5] == whoseTurn && board[4] == "") { return 5; } // row
            if (board[6] == whoseTurn && board[8] == whoseTurn && board[7] == "") { return 8; } // row

            if (board[0] == whoseTurn && board[8] == whoseTurn && board[4] == "") { return 5; } // cross
            if (board[6] == whoseTurn && board[2] == whoseTurn && board[4] == "") { return 5; } // cross

            return 0;
        }

        private int RandomFreeField(List<string> board)
        {
            int i = 1;

            List<int> freeFields = new List<int>();

            foreach (var item in board)
            {
                if (item == "") { freeFields.Add(i); }

                i++;
            }

            if (freeFields.Count != 0) { return freeFields[new Random().Next(0, freeFields.Count - 1)]; }

            return 0;
        }
    }
}
