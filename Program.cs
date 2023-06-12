using System;
using System.Text;
using System.Timers;

namespace Tetris
{
    class Program
    {

        public static bool gamePlaying = true;
        public static System.Timers.Timer tact = new System.Timers.Timer(500);//TODO relocate to GameWindow class
        private static GameWindow game = new GameWindow();
        static void Main(string[] args)
        {


            PlayGame();
            
            Console.ReadKey();
        }

        static void PlayGame()
        {
            game.Reset();
            
            tact.Elapsed += FallEventHandler;
            tact.AutoReset = true;
            tact.Enabled = true;

            while (gamePlaying)
            {
                ConsoleKey key = Console.ReadKey(true).Key;//TODO - WinAPI assistance (single keystrokes)
                
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        game.MoveRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        game.MoveLeft();
                        break;                                   //                   ██████   ██     █   ██   ██████████
                    case ConsoleKey.R:                           //                  ██        ██     █            █
                        game.Rotate();                           //                   ███      ██     █   ██       █
                        break;                                   //                      ██    ████████   ██       █
                    case ConsoleKey.DownArrow:                   //                       ██   ██     █   ██       █
                        game.ForceDown();                        //   Remake me!      █████    ██     █   ██       █
                        break;
                }
                game.Update();

            }
        }

        private static void FallEventHandler(Object source, System.Timers.ElapsedEventArgs _)
        {
            game.ForceDown();
            game.Update();
        }
    }
}
