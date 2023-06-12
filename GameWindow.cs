using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class GameWindow
    {
        #region Special Console Parameters
        const int W_HEIGHT = 20;
        const int W_WIDTH = 10;
        const int BLOCK_WIDTH = 4;
        const int BLOCK_HEIGHT = 2;
        public static ConsoleColor BG_COLOR = ConsoleColor.Black;
        #endregion


        public BlockMap mapModel;
        ConsoleColor[,] map;
        public GameWindow()
        {
            mapModel = new BlockMap(W_HEIGHT,W_WIDTH,this);
            map = mapModel.map;

            Console.SetWindowSize((W_WIDTH) * BLOCK_WIDTH, (W_HEIGHT) * BLOCK_HEIGHT);
            Console.SetBufferSize((W_WIDTH) * BLOCK_WIDTH+1, (W_HEIGHT) * BLOCK_HEIGHT+1);
            Console.CursorVisible = false;

        }
        public void Update()
        {
            DisplayFigure();
        }
        //public void AlternativeTestUpdate()
        //{
        //    Console.Clear();
        //    for (int i = 0; i < colorMap.GetLength(0); i++)
        //        for (int j = 0; j < colorMap.GetLength(1); j++)
        //        {
        //            Console.ForegroundColor = colorMap[i, j];
        //            Console.SetCursorPosition(j, i);
        //            Console.Write("Ш");
        //        }
        //}
        public void Reset()
        {
            mapModel.ClearMap();
            mapModel.Summon();
        }

        public void DisplayMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    DisplayBlock(i, j, map[i, j]);
        }

        public void DisplayFigure()
        {
            for (int i=0; i < mapModel.BLOCK_COUNT; i++)
            {
                Block block = mapModel.controlledFigure.blocks[i];
                DisplayBlock(block.X + mapModel.centerBlock.X, block.Y + mapModel.centerBlock.Y, mapModel.controlledFigure.figureColor);
            }
        }
        public void EraseFigure()
        {
            for (int i = 0; i < mapModel.BLOCK_COUNT; i++)
            {
                Block block = mapModel.controlledFigure.blocks[i];
                DisplayBlock(block.X + mapModel.centerBlock.X, block.Y + mapModel.centerBlock.Y, BG_COLOR);
            }
        }
        void DisplayBlock(int x, int y, ConsoleColor blockColor)
        {
            Console.ForegroundColor = blockColor;
            for (int i = y * BLOCK_WIDTH; i < (y + 1) * BLOCK_WIDTH; i++)
                for (int j = x * BLOCK_HEIGHT; j < (x + 1) * BLOCK_HEIGHT; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write("█");
                }
        }

        public void EraseLine(int lineNumber)
        {
            for (int j = 0; j < map.GetLength(1); j++)
                DisplayBlock(lineNumber, j, BG_COLOR);
        }


        public void MoveLeft() => mapModel.HorizontalMoving(-1);
        public void MoveRight() => mapModel.HorizontalMoving(1);
        public void ForceDown() => mapModel.VerticalMoving(1);

        public void Rotate() => mapModel.RotateFigure();

    }
}
