using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHex
{
    class AI
    {
        public static int GetNextMove(Board board,Block block)
        {
            int highScore = 0;
            List<int> can = new List<int>();
            for(int i = 0;i<board.Count;i++)
            {
                Board tempBoard = new Board(board);
                if (!tempBoard.AddBlock(i, block)) continue;
                int sc = tempBoard.GetScore();
                if(sc>highScore)
                {
                    can.Clear();
                    can.Add(i);
                    highScore = sc;
                }
                if(sc == highScore)
                {
                    can.Add(i);
                }
            }
            if (highScore == 0) return -1;
            Random rd = new Random();
            return can[rd.Next(0, can.Count)];
        }
    }
}
