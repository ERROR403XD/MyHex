﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHex
{
    class Board
    {
        private List<BoardHex> hexList;
        private List<BoardHex>[] checkList;
        private int length;
        public int Length
        {
            get
            {
                return length;
            }
        }
        public int Count
        {
            get
            {
                return hexList.Count;
            }
        }
        public bool[] State
        {
            get
            {
                bool[] res = new bool[hexList.Count];
                for(int i = 0;i<hexList.Count;i++)
                {
                    res[i] = hexList[i].Filled;
                }
                return res;
            }
        }
        public Board(int l)
        {
            hexList = new List<BoardHex>();
            checkList = new List<BoardHex>[3];
            checkList[0] = new List<BoardHex>();
            checkList[1] = new List<BoardHex>();
            checkList[2] = new List<BoardHex>();

            length = l;
            BoardHex head = GenerateRow(length);

            foreach(BoardHex temp in hexList)
            {
                checkList[0].Add(temp);
                // temp.teststr = "X";
            }

            for (int i = 0;i<length-1;i++)
            {
                checkList[1].Add(head);
                BoardHex next = GenerateRow(length + i+1);
                FetchLeft(head, next);
                head = next;
            }
            checkList[1].Add(head);
            for(int i = 0;i<length-1;i++)
            {
                checkList[2].Add(head);
                BoardHex next = GenerateRow(2 * length - 1 - i - 1);
                FetchRight(head, next);
                head = next;
            }
            checkList[2].Add(head);
        }
        public Board(Board target)
        {
            hexList = new List<BoardHex>();
            checkList = new List<BoardHex>[3];
            checkList[0] = new List<BoardHex>();
            checkList[1] = new List<BoardHex>();
            checkList[2] = new List<BoardHex>();

            length = target.length;
            BoardHex head = GenerateRow(length);

            foreach (BoardHex temp in hexList)
            {
                checkList[0].Add(temp);
                // temp.teststr = "X";
            }

            for (int i = 0; i < length - 1; i++)
            {
                checkList[1].Add(head);
                BoardHex next = GenerateRow(length + i + 1);
                FetchLeft(head, next);
                head = next;
            }
            checkList[1].Add(head);
            for (int i = 0; i < length - 1; i++)
            {
                checkList[2].Add(head);
                BoardHex next = GenerateRow(2 * length - 1 - i - 1);
                FetchRight(head, next);
                head = next;
            }
            checkList[2].Add(head);

            bool[] state = target.State;
            for(int i = 0;i<state.Length;i++)
            {
                if(state[i])
                {
                    hexList[i].Fill();
                }
            }

        }

        //generate board
        protected BoardHex GenerateRow(int count)
        {
            BoardHex head = new BoardHex();
            hexList.Add(head);
            BoardHex current = head;
            while(count>1)
            {
                BoardHex temp = new BoardHex();
                hexList.Add(temp);
                current.neighbor[0] = temp;
                temp.neighbor[3] = current;
                current = temp;
                count--;
            }
            return head;
        }
        protected void FetchLeft(BoardHex above,BoardHex below)
        {
            while(above!=null&&below!=null)
            {
                below.neighbor[5] = above;
                above.neighbor[2] = below;
                above.neighbor[1] = below.neighbor[0];
                if (below.neighbor[0] != null) below.neighbor[0].neighbor[4] = above;

                above = above.neighbor[0];
                below = below.neighbor[0];
            }
        }
        protected void FetchRight(BoardHex above,BoardHex below)
        {
            while (above != null && below != null)
            {
                below.neighbor[4] = above;
                above.neighbor[1] = below;
                below.neighbor[5] = above.neighbor[0];
                if (above.neighbor[0] != null) above.neighbor[0].neighbor[2] = below;

                above = above.neighbor[0];
                below = below.neighbor[0];
            }
        }

        //basic func
        public bool JudgeAddable(BoardHex pos,Block target)
        {
            if(!hexList.Contains(pos))
            {
                throw new IndexOutOfRangeException();
            }
            List<BoardHex> judgeList = new List<BoardHex>() {pos };
            List<int[]> blockList = target.Hexes;
            int i = 0;
            while(judgeList.Count!=0)
            {
                if(judgeList[0].Filled)
                {
                    return false;
                }
                
                if(i<blockList.Count)
                {
                    for(int j=0;j<blockList[i].Length;j++)
                    {
                        if(judgeList[0].neighbor[blockList[i][j]]==null)
                        {
                            return false;
                        }
                        else
                        {
                            judgeList.Add(judgeList[0].neighbor[blockList[i][j]]);
                        }
                    }
                    i++;
                }
                judgeList.RemoveAt(0);

            }
            return true;


        }
        public bool AddBlock(BoardHex pos,Block target)
        {
            if (!JudgeAddable(pos, target)) return false;
            List<BoardHex> judgeList = new List<BoardHex>() { pos };
            List<int[]> blockList = target.Hexes;
            int i = 0;
            while (judgeList.Count != 0)
            {
                judgeList[0].Fill();
                if (i < blockList.Count)
                {
                    for (int j = 0; j < blockList[i].Length; j++)
                    {
                        judgeList.Add(judgeList[0].neighbor[blockList[i][j]]);
                    }
                    i++;
                }
                judgeList.RemoveAt(0);
            }

            List<BoardHex> toUnfil = new List<BoardHex>();
            foreach(BoardHex hex in checkList[0])
            {
                Check(hex, 2, toUnfil);
                Check(hex, 1, toUnfil);
            }
            foreach (BoardHex hex in checkList[1])
            {
                Check(hex, 1, toUnfil);
                Check(hex, 0, toUnfil);
            }
            foreach (BoardHex hex in checkList[2])
            {
                Check(hex, 5, toUnfil);
                Check(hex, 0, toUnfil);
            }
            foreach(BoardHex hex in toUnfil)
            {
                hex.Unfill();
            }
            return true;
        }
        public bool AddBlock(int index,Block target)
        {
            return AddBlock(hexList[index], target);
        }
        protected int Check(BoardHex start, int dir, List<BoardHex> list)
        {
            if(dir<0||dir>5)
            {
                throw new IndexOutOfRangeException();
            }
            int score = 0;
            List<BoardHex> tempList = new List<BoardHex>();
            while(start!=null&&start.Filled)
            {
                tempList.Add(start);
                start = start.neighbor[dir];
            }
            if(start!=null)
            {
                return 0;
            }
            foreach(BoardHex hex in tempList)
            {
                list.Add(hex);
            }
            return score;
        }

        //AI support
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
            return score;
        }
        private int GetSpaceScore()
        {
            int score = 0;
            foreach(BoardHex i in hexList)
            {
                if (!i.Filled) score += 10;
            }
            return score;
        }
        private int GetPScore()
        {
            int score = 0;
            for(int t = 0;t<Block.Types;t++)
            {
                for(int r= 0;r<5;r++)
                {
                    Block testBlock = new Block(t, r);
                    //score += IfAvilable(testBlock) * 2;
                    if (IfAvilable(testBlock) != 0) score += 20;
                }
            }
            return score;
        }

        protected int IfAvilable(Block b)
        {
            int res = 0;
            foreach(BoardHex i in hexList)
            {
                if(JudgeAddable(i,b))
                {
                    res++;
                }
            }
            return res;
        }

        //test func
        public void RandomAdd(Block b)
        {
            Random rd = new Random();
            BoardHex pos = hexList[rd.Next(hexList.Count)];

            if(JudgeAddable(pos,b))
            {
                pos.teststr = "Y";
                AddBlock(pos, b);
            }
            else
            {
                pos.teststr = "N";
            }
            PrintSelf();
        }
        public void TestAdd(Block b)
        {
            foreach(BoardHex hex in hexList)
            {
                if(JudgeAddable(hex,b))
                {
                    AddBlock(hex, b);
                    PrintSelf();
                    return;
                }
                
            }
            Console.WriteLine("game over");
        }
        public void PrintSelf()
        {
            for (int i = 0; i < checkList[1].Count; i++)
            {
                Console.Write(new string(' ', checkList[1].Count - i));
                checkList[1][i].PrintSelf();
            }
            for (int i = 1; i < checkList[2].Count; i++)
            {
                Console.Write(new string(' ', i + 1));
                checkList[2][i].PrintSelf();
            }
        }
        public void RandomChange()
        {
            Random rd = new Random();
            BoardHex target = hexList[rd.Next(hexList.Count)];
            target.teststr = "I";
            for (int i = 0; i < target.neighbor.Length; i++)
            {
                if (target.neighbor[i] != null)
                {
                    target.neighbor[i].teststr = "X";
                }
            }
            PrintSelf();
            target.teststr = "O";
            for (int i = 0; i < target.neighbor.Length; i++)
            {
                if (target.neighbor[i] != null)
                {
                    target.neighbor[i].teststr = "O";
                }
            }
        }
        public void TestAvi(Block b)
        {
            foreach(BoardHex i in hexList)
            {
                if(JudgeAddable(i,b))
                {
                    i.teststr = "Y";
                }
                else
                {
                    i.teststr = "N";
                }
            }
        }

    }
}
