using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct Block
    {
        int xPos;
        int yPos;
        public Block(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public int X { get => xPos; set => xPos = value; }
        public int Y { get => yPos; set => yPos = value; }
    }
}
