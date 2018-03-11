using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHex
{
    class Block
    {
        private static List<List<int[]>> blockList = InitBlockList();
        private int type;
        private int rotate;
        private List<int[]> hexes;
        public static int Types
        {
            get
            {
                return blockList.Count;
            }
        }

        public int Tpye
        {
            get
            {
                return type;
            }
        }
        public int Rotate
        {
            get
            {
                return rotate;
            }
        }
        public int Count
        {
            get
            {
                int count = 1;
                foreach(int[] temp in hexes)
                {
                    count += temp.Length;
                }
                return count;
            }
        }
        public List<int[]> Hexes
        {
            get
            {
                List<int[]> res = new List<int[]>();
                foreach (int[] temp in hexes)
                {
                    res.Add((int[])temp.Clone());
                }
                return res;
            }
        }
        public Block(int t,int r)
        {
            if(blockList == null)
            {
                blockList = InitBlockList();
            }

            if(t<0||t>Types||r<0)
            {
                throw new IndexOutOfRangeException();
            }

            type = t;
            rotate = r;
            hexes = new List<int[]>();
            foreach(int[] temp in blockList[type])
            {
                hexes.Add((int[])temp.Clone());
            }
            
            foreach(int[] temp in hexes)
            {
                for(int i = 0;i<temp.Length;i++)
                {
                    temp[i] = (temp[i] + rotate) % 6;
                }
            }
        }
        private static List<List<int[]>> InitBlockList()
        {
            List<List<int[]>> res = new List<List<int[]>>()
                {
                    new List<int[]>(),//单个
                    new List<int[]>() { new int[] { 0 }, new int[] { 0 }, new int[] { 0 }},//长条
                    new List<int[]>() { new int[] { 0 }, new int[] { 0,1 } },
                    new List<int[]>() { new int[] { 0 }, new int[] { 0,5 } },
                    new List<int[]>() { new int[] { 0 }, new int[] { 1 }, new int[] { 2 } },
                    new List<int[]>() { new int[] { 0 }, new int[] { 1, 5 } },
                    new List<int[]>() { new int[] { 5, 0, 1} }
                };
            return res;
        }
        public void PrintSelf()
        {
            if (hexes.Count == 0) Console.Write("NULL");
            foreach(int[] temp in hexes)
            {
                for(int i = 0;i<temp.Length;i++)
                {
                    Console.Write(temp[i]);
                    Console.Write(" ");
                }
                Console.Write(";");
            }
            Console.Write("("+type.ToString()+","+rotate.ToString()+")");
            Console.WriteLine();
        }
        public static void PrintBlockList()
        {
            foreach(List<int[]> templist in blockList)
            {
                if (templist.Count == 0) Console.Write("NULL");
                foreach(int[] temp in templist)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        Console.Write(temp[i]);
                        Console.Write(" ");
                    }
                    Console.Write(";");
                }
                Console.WriteLine();
            }
        }


    }
}
