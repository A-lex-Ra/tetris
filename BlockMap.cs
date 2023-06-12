using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class BlockMap
    {
        /* --------------------> Y AXIS
         |
         |
         | 
         |
         V 
       X AXIS
             */

        public ConsoleColor[,] map;
        public Figure controlledFigure = new Figure();
        public Block centerBlock;
        public int BLOCK_COUNT = 4;

        GameWindow window;
        CollisionManager collisionManager;
        Mutex mutex = new Mutex();
        

        public BlockMap(int height, int width, GameWindow parentWindow)
        {
            window = parentWindow;
            map = new ConsoleColor[height, width];
            collisionManager = new CollisionManager(this, controlledFigure);
        }
        
        public void ClearMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                ClearLine(i);
        }
        void ClearLine(int lineNumber)
        {
            for (int j = 0; j < map.GetLength(1); j++)
                map[lineNumber, j] = GameWindow.BG_COLOR;
        }


        public void Summon()
        {
            centerBlock = new Block(2, 5);
            controlledFigure.NextShape();
        }

        //public void RemoveFigure()
        //{
        //    for (int i = 0; i < BLOCK_COUNT; i++)
        //    {
        //        Block block = controlledFigure.blocks[i];
        //        map[centerBlock.X + block.X, centerBlock.Y + block.Y] = GameWindow.BG_COLOR;
        //    }
        //}
        public void ProjectFigure()
        {
            for (int i = 0; i < BLOCK_COUNT; i++)
            {
                Block block = controlledFigure.blocks[i];
                map[centerBlock.X + block.X, centerBlock.Y + block.Y] = controlledFigure.figureColor;
            }
        }


        public void RotateFigure()
        {
            if (controlledFigure.isRotatable && collisionManager.CheckRotationPossibility())
            {
                window.EraseFigure();
                controlledFigure.Rotate();
            }
        }

        public void HorizontalMoving(sbyte direction = 1)
        {
             if (collisionManager.CheckHorizontalPossibility(direction))
             {
                window.EraseFigure();
                centerBlock.Y += direction;
             }
        }
        public void VerticalMoving(sbyte direction = 1)
        {
            mutex.WaitOne();

            if (collisionManager.CheckVerticalPossibility(direction))
            {
                window.EraseFigure();
                centerBlock.X += direction;
            }
            else FellFigureHandler();

            mutex.ReleaseMutex();
        }


        void FellFigureHandler()
        {
            ProjectFigure();
            ClearExistsFilledLines();
            window.DisplayMap();
            Summon();
        }




        
        
        
        void ClearExistsFilledLines()
        {
            int[] filledLines = new int[4] { -1, -1, -1, -1 };
            int cursor = 0;

            for (int i = map.GetLength(0) - 1; (i >= 0) && (cursor < filledLines.Length); i--)
            {
                if (IsFilledLine(i)) {
                    filledLines[cursor] = i;//maybe it needs a limiter of checkouts in succession (4 in tetramino)
                    cursor += 1;//I doubt about capability with 3 checks in header
                }
            }

            for (int i = 0; i < cursor; i++)
            {
                if (filledLines[i] != -1) {
                    SqueezeLine(filledLines[i] + i);
                    filledLines[i] = -1;
                }
                else {
                    break;
                }
            }
            cursor = 0;
        }

        void SqueezeLine(int lineNumber)
        {
            for (int i = lineNumber - 1; i >= 0; i--)
            {
                DuplicateLine(i, i+1);
                if (IsEmptyLine(i)){
                    break;
                }
            }
            ClearLine(0);
        }
        void DuplicateLine(int fromLineNumber, int toLineNumber)
        {
            for (int j = 0; j < map.GetLength(1); j++)
                map[toLineNumber, j] = map[fromLineNumber, j];
        }
        bool IsEmptyLine(int lineNumber)
        {
            bool isEmpty = true;
            for (int j = 0; j < map.GetLength(1); j++)
                if (map[lineNumber, j] != GameWindow.BG_COLOR)
                {
                    isEmpty = false;
                    break;
                }
            return isEmpty;
        }
        bool IsFilledLine(int lineNumber)
        {
            bool isFilled = true;
            for (int j = 0; j < map.GetLength(1); j++)
                if (map[lineNumber, j] == GameWindow.BG_COLOR)
                {
                    isFilled = false;
                    break;
                }
            return isFilled;
        }
    }
}
