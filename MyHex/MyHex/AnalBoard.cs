using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHex
{
    class AnalBoard:Board
    {
        public AnalBoard(int l):base(l)
        {

        }
        public AnalBoard(Board target):base(target)
        {

        }

        public int GetScore()
        {
            int score = 0;

            //
            score = Analysis();
            //
            return score;
        }

        private int Analysis()
        {
            int score = 0;
            score += GetSpaceScore();
            score += GetPScore();
            score += GetNullScore();
            return score;
        }
        private int GetSpaceScore()
        {
            int score = 0;
            foreach (BoardHex i in hexList)
            {
                if (!i.Filled) score += 10;
            }
            return score;
        }
        private int GetPScore()
        {
            int score = 0;
            for (int t = 1; t < Block.Types; t++)
            {
                for (int r = 0; r < 5; r++)
                {
                    Block testBlock = new Block(t, r);
                    //score += IfAvilable(testBlock) * 2;
                    if (IfAvilable(testBlock) != 0) score += 20;
                }
            }
            return score;
        }
        private int GetNullScore()
        {
            int score = 0;
            foreach (BoardHex hex in hexList)
            {
                if (SuitAbleNum(hex) == 0) score -= 50;
            }
            return score;
        }
        private int IfAvilable(Block b)
        {
            int res = 0;
            foreach (BoardHex i in hexList)
            {
                if (JudgeAddable(i, b))
                {
                    res++;
                }
            }
            return res;
        }
        private int SuitAbleNum(BoardHex hex)
        {
            int res = 0;
            for(int t = 1;t<Block.Types;t++)
            {
                for(int r = 0;r<5;r++)
                {
                    Block testBlock = new Block(t, r);
                    if (JudgeAddable(hex, testBlock)) res++;
                }
            }
            return res;
        }

    }
}
