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
            board.RandomAdd(b1);
            for (int i = 0; i < 5;i++)
            {
                Block b2 = new Block(rd.Next(Block.Types), rd.Next(6));
                board.TestAdd(b2);
                Thread.Sleep(500);
                Console.Clear();
            }
            board.PrintSelf();
            Board board2 = new Board(board);
            board2.PrintSelf();
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
