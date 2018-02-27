using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHex
{
    class Hex
    {
        public Hex[] neighbor; 
        public Hex()
        {
            neighbor = new Hex[6];
            for(int i = 0;i<6;i++)
            {
                neighbor[i] = null;
            }
        }
    }
    class BoardHex:Hex
    {
        public string teststr;

        private bool filled;
        public bool Filled
        {
            get
            {
                return filled;
            }
        }

        public BoardHex[] neighbor;
        public BoardHex()
        {
            teststr = "O";
            neighbor = new BoardHex[6];
            for(int i = 0;i<6;i++)
            {
                neighbor[i] = null;
            }
            filled = false;
        }
        public void Fill()
        {
            if(filled)
            {
                throw new System.ArgumentException("already filled");
                return;
            }
            filled = true;
        }
        public void Unfill()
        {
            filled = false;
        }

        public void PrintSelf()
        {
            if(filled)
            {
                Console.Write("P" + " ");
            }
            else
            {
                Console.Write(teststr + " ");
            }
            
            if (neighbor[0] != null) neighbor[0].PrintSelf();
            else Console.WriteLine();
        }
    }
}
