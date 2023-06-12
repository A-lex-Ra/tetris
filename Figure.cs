using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{

    public class Figure
    {
        // Figures` models
        Block[][] variants =
        {
            new Block[]
            {
                new Block(0,0),
                new Block(0,1),//████
                new Block(1,0),//████
                new Block(1,1),
            },
            new Block[]
            {
                new Block(0,0),
                new Block(-1,0),//██
                new Block(1,0), //██
                new Block(2,0), //██   
            },                  //██ ░▒▓┤╠╡╢╖╕╠╣║╠╗╠╝╜╠╛┐└┴┬├─┼╞╡╟╚╔╠╩╠╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌ █ 219 ▄▌▐▀юяЁёЄєЇїЎў°∙·√№¤■ ☺Û‡
            new Block[]
            {
                new Block(0,0),
                new Block(-1,0),//██
                new Block(0,1), //████
                new Block(1,1), //  ██
            },
            new Block[]
            {
                new Block(0,0),
                new Block(0,-1),//████
                new Block(1,0), //  ████
                new Block(1,1), //
            },
            new Block[]
            {
                new Block(0,0),
                new Block(0,1),//██████
                new Block(1,0),//  ██
                new Block(0,-1),
            },
            new Block[]
            {
                new Block(0,0),
                new Block(-1,0),//██
                new Block(1,0), //██
                new Block(1,1), //████
            },
            new Block[]
            {
                new Block(0,0),
                new Block(-1,0),//  ██
                new Block(1,0), //  ██
                new Block(1,-1),//████
            },
        };

        public int[] unrotatableVariants = { 0 };

        public Block[] blocks;
        public bool isRotatable;
        public ConsoleColor figureColor;

        public void NextShape()
        {
            Random randomVariant = new Random();
            blocks = Generate(randomVariant.Next(0,7));
            figureColor = (ConsoleColor)randomVariant.Next(1, 16);
            for (int i = 0; i < randomVariant.Next(0, 4); i++)
            {
                Rotate();
            }
        }

        Block[] Generate(int variant) {
            isRotatable = GetRotatability(variant); 
            return variants[variant]; 
        }

        bool GetRotatability(int variant)
        {
            if (unrotatableVariants.Contains(variant)) return false;
            else return true;
        }
        
        public void Rotate()
        {
            int previousX;
            for (int i = 0; i < blocks.Length; i++)
            {
                previousX = blocks[i].X;
                blocks[i].X = blocks[i].Y;
                blocks[i].Y = -previousX;
            }
        }
    }
}
