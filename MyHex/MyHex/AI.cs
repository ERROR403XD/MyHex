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
            int highScore = int.MinValue;
            List<int> can = new List<int>();
            for(int i = 0;i<board.Count;i++)
            {
                AnalBoard tempBoard = new AnalBoard(board);
                if (tempBoard.AddBlock(i, block)==-1) continue;
                int sc = tempBoard.GetScore();
                if(sc>highScore)
                {
                    can.Clear();
                    can.Add(i);
                    highScore = sc;
                }
                else if(sc == highScore)
                {
                    can.Add(i);
                }
            }
            if (highScore == int.MinValue) return -1;
            Console.WriteLine("highsc = " + highScore.ToString());
            foreach(int q in can)
            {
                Console.Write(q);
                Console.Write("  ");
            }
            Console.WriteLine();
            Random rd = new Random(1);
            return can[rd.Next(can.Count)];
        }
    }
}
