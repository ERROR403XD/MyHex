using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyHex
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(5);
            Random rd = new Random(1);
            /*
            Block.PrintBlockList();
            Console.WriteLine(Block.Types);
            Block b1 = new Block(rd.Next(Block.Types), rd.Next(6));
            b1.PrintSelf();
            */
            
            int[] step = new int[] { };
            int score = 0;
            //board.RandomAdd(b1);
            while (true) 
            {
                Block b2 = new Block(rd.Next(Block.Types), rd.Next(6));
                Block.PrintBlockList();
                b2.PrintSelf();
                int index = AI.GetNextMove(board, b2);
                /*
                
                 board.TestAvi(b2);
                 */
                if (index == -1) break;
                score += board.AddBlock(index, b2);
                Console.WriteLine(index);
                board.PrintSelf();
                Console.WriteLine("score = " + score.ToString());
                
                
                //Thread.Sleep(500);
            }
            /*
            while(true)
            {
                board.RandomChange();
                Thread.Sleep(500);
                Console.Clear();
            }*/
            Console.ReadKey();
        }
    }
}
