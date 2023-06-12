using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Tetris
{
    class CollisionManager
    {
        Mutex mutex = new Mutex();
        BlockMap mapModel;
        Figure figure;
        public CollisionManager(BlockMap map, Figure figure)
        {
            mapModel = map;
            this.figure = figure;
        }

        public bool CheckVerticalPossibility(int direction)
        {
            mutex.WaitOne();

            bool isPossible = true;
            int supposedCenterX = mapModel.centerBlock.X + direction;
            for (int i = 0; i < mapModel.BLOCK_COUNT; i++)
            {
                if (SupposedBlockHasXCollision(supposedCenterX + figure.blocks[i].X, mapModel.centerBlock.Y + figure.blocks[i].Y, direction))
                {
                    isPossible = false;
                    break;
                }
            }

            mutex.ReleaseMutex();

            return isPossible;
        }

        bool SupposedBlockHasXCollision(int supposedX, int supposedY, int direction)
        {
            switch (direction)
            {
                case 1:
                    return (IsBeyondTheBottomLine(supposedX) ||
                            CellIsBusy(supposedX, supposedY));
                case -1:
                    return (IsBeyondTheTopLine(supposedX) || 
                            CellIsBusy(supposedX, supposedY));
                default:
                    return true;
            }

        }

        bool IsBeyondTheBottomLine(int x) => (x >= mapModel.map.GetLength(0));
        bool IsBeyondTheTopLine(int x) => (x < 0);

        bool CellIsBusy(int x, int y) => (mapModel.map[x, y] != GameWindow.BG_COLOR);


        public bool CheckHorizontalPossibility(int direction)
        {
            bool isPossible = true;
            int supposedCenterY = mapModel.centerBlock.Y + direction;
            for (int i = 0; i < mapModel.BLOCK_COUNT; i++)
            {
                if (SupposedBlockHasYCollision(mapModel.centerBlock.X + figure.blocks[i].X, supposedCenterY + figure.blocks[i].Y, direction))
                {
                    isPossible = false;
                    break;
                }
            }

            return isPossible;
        }

        bool SupposedBlockHasYCollision(int supposedX, int supposedY, int direction)
        {
            switch (direction)
            {
                case 1:
                    return (IsBeyondTheRightLine(supposedY) ||
                            CellIsBusy(supposedX, supposedY));
                case -1:
                    return (IsBeyondTheLeftLine(supposedY) ||
                            CellIsBusy(supposedX, supposedY));
                default:
                    return true;
            }
        }

        bool IsBeyondTheRightLine(int y) => (y >= mapModel.map.GetLength(1));
        bool IsBeyondTheLeftLine(int y) => (y < 0);

        public bool CheckRotationPossibility()
        {
            bool isPossible = true;
            int supposedX, supposedY;
            for (int i = 0; i < mapModel.BLOCK_COUNT; i++)
            {
                supposedX = figure.blocks[i].Y + mapModel.centerBlock.X;
                supposedY = -figure.blocks[i].X+ mapModel.centerBlock.Y;
                if ( IsBeyondTheLeftLine(supposedY) || IsBeyondTheRightLine(supposedY)  ||
                     IsBeyondTheTopLine(supposedX)  || IsBeyondTheBottomLine(supposedX) ||
                     CellIsBusy(supposedX, supposedY) )
                {
                    isPossible = false;
                    break;
                }
            }

            return isPossible;
        }
    }
}
