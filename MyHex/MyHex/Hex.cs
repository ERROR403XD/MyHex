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
        private bool filled;
        public bool Filled
        {
            get
            {
                return filled;
            }
        }
        public BoardHex()
        {
            neighbor = new BoardHex[6];
            for(int i = 0;i<6;i++)
            {
                neighbor[i] = null;
            }
            filled = false;
        }
    }
}
