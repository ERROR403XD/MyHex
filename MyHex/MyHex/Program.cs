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
            Random rd = new Random();
            Block.PrintBlockList();
            Console.WriteLine(Block.Types);
            Block b1 = new Block(rd.Next(Block.Types), rd.Next(6));
            b1.PrintSelf();
            //board.RandomAdd(b1);
            while (true) 
            {
                Block b2 = new Block(rd.Next(Block.Types), rd.Next(6));
                int index = AI.GetNextMove(board, b2);
                if (index == -1) break;
                board.AddBlock(index, b2);

                Console.Clear();
                board.PrintSelf();
                Thread.Sleep(500);
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
