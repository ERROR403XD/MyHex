using System;
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
        public Board()
        {
            hexList = new List<BoardHex>();
            checkList = new List<BoardHex>[3];
            checkList[0] = new List<BoardHex>();
            checkList[1] = new List<BoardHex>();
            checkList[2] = new List<BoardHex>();

            int length = 5;
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
                head.PrintSelf();
                head = next;
            }
            checkList[1].Add(head);
            for(int i = 0;i<length-1;i++)
            {
                checkList[2].Add(head);
                BoardHex next = GenerateRow(2 * length - 1 - i - 1);
                FetchRight(head, next);
                head.PrintSelf();
                head = next;
            }
            checkList[2].Add(head);
            head.PrintSelf();


            Random rd = new Random();
            BoardHex target = hexList[rd.Next(hexList.Count)];
            target.teststr = "I";
            for(int i =0;i<target.neighbor.Length;i++)
            {
                if(target.neighbor[i]!=null)
                {
                    target.neighbor[i].teststr = "X";
                }
            }

            Console.WriteLine();
            PrintSelf();

            

        }
        private BoardHex GenerateRow(int count)
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
        private void FetchLeft(BoardHex above,BoardHex below)
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
        private void FetchRight(BoardHex above,BoardHex below)
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

        private void PrintSelf()
        {
            for (int i = 0; i < checkList[1].Count; i++)
            {
                Console.Write(new string(' ', checkList[1].Count - i));
                checkList[1][i].PrintSelf();
            }
            for(int i = 1;i<checkList[2].Count;i++)
            {
                Console.Write(new string(' ', i+1));
                checkList[2][i].PrintSelf();
            }
        }
    }
}
