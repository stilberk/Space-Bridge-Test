using System;
using System.Collections.Generic;
using System.Threading;

namespace Space_Bridge_Test
{
    public class SpaceBridge
    {
        public static void Main()
        {

            //2.Draw playfield
            const int playfieldWidth = 35;
            Console.BufferHeight = Console.WindowHeight = 10;//This is the size of the console window height.
            Console.BufferWidth = Console.WindowWidth = 49;//size of window width.                              
            Console.BackgroundColor = ConsoleColor.Gray;//color of playfield

            int xCoordinate = 23;
            int yCoordinate = Console.WindowHeight - 2;
            string symbolForBridge = "---";
            ConsoleColor colorOfBridge = ConsoleColor.DarkRed;
            Object bridge = new Object(xCoordinate, yCoordinate, symbolForBridge, colorOfBridge, false);
            bool bridgeHitted = false;
            Object cash = new Object(9, 0, "$", ConsoleColor.Green, bridgeHitted);
            int lives = 3;
            int points = 0;
            double speed = 10.0;
            double acceleration = 0.2;
            List<Object> dollars = new List<Object>();
            dollars.Add(cash);
            Random rnd = new Random();

            while (true)
            {
                speed += acceleration;
                if (speed > 400)
                {
                    speed = 400;
                }
                Thread.Sleep(500 - (int)speed);

                int chance = rnd.Next(0, 50);
                //Add more cosmonauts
                if (points > 2 && chance < 70 && dollars.Count < 3)
                {
                    //check if it is possible to be played
                    if (dollars.TrueForAll(x => x.X < chance))
                    {
                        Object cos = new Object(9, 0, "$", ConsoleColor.Green, false);
                        dollars.Add(cos);
                    }
                }
                //4.Move the bridge
                MoveTheBridge(bridge, playfieldWidth);
                //Moving our alien....
                for (int i = 0; i < dollars.Count; i++)
                {
                    bridgeHitted = dollars[i].BridgeHitted;
                    if (!bridgeHitted)
                    {
                        if (dollars[i].Y < Console.WindowHeight)
                        {
                            dollars[i].Y++;
                        }
                        if (dollars[i].X < Console.WindowWidth)
                        {
                            if (dollars[i].X == 15 || dollars[i].X == 25 || dollars[i].X == 24 || dollars[i].X == 33 || dollars[i].X ==34)
                            {
                                dollars[i].X--;
                            }
                            dollars[i].X++;
                        }
                        if (dollars[i].Y == bridge.Y && dollars[i].X == bridge.X + 1)
                        {
                            points++;
                            dollars[i].BridgeHitted = true;
                        }
                        if (dollars[i].Y > Console.WindowHeight - 1)
                        {
                            lives--;
                            dollars.Remove(dollars[i]);
                            if (dollars.Count == 0)
                            {
                                dollars.Add(new Object(9, 0, "$", ConsoleColor.Green, bridgeHitted));
                            }
                            if (lives == 0)
                            {
                                break;
                            }
                        }
                    }
                    if (lives == 0)
                    {
                        break;
                    }
                }
                if (lives == 0)
                {
                    break;
                }
                //Moving the opposite direction after bridge was hitted
                for (int i = 0; i < dollars.Count; i++)
                {
                    bridgeHitted = dollars[i].BridgeHitted;
                    if (bridgeHitted)
                    {
                        if (dollars[i].Y > Console.WindowHeight - 7)
                        {
                            dollars[i].Y--;
                        }
                        else
                        {
                            dollars[i].BridgeHitted = false;
                        }
                        if (dollars[i].X < Console.WindowWidth)
                        {
                            dollars[i].X++;
                        }
                    }
                }
                //remove objects which are outside the field and create new      
                for (int i = 0; i < dollars.Count; i++)
                {
                    if (dollars[i].X > 42)
                    {
                        dollars.Remove(dollars[i]);
                        dollars.Add(new Object(9, 0, "$", ConsoleColor.Green, false));
                    }
                }
                //7.Clear the console with 
                Console.Clear();
                //8. Print Basket
                PrintOnPosition(41, 7, "\\", ConsoleColor.Green);
                PrintOnPosition(42, 7, "___", ConsoleColor.Green);
                PrintOnPosition(45, 7, "/", ConsoleColor.Green);
                //Print Lives
                PrintOnPosition(41, 1, "Lives:" + lives.ToString(), ConsoleColor.DarkRed);
                //Print other object  
                foreach (var cosmo in dollars)
                {
                    if (cosmo.X < 48 && cosmo.Y < 10)
                    {
                        PrintOnPosition(cosmo.X, cosmo.Y, cosmo.Symbol, cosmo.Color);
                    }
                }
                PrintHoles();  
                //8.print our object
                PrintOnPosition(bridge.X, bridge.Y, bridge.Symbol, bridge.Color);
                //printing points
                PrintOnPosition(41, 0, "$:" + points, ConsoleColor.DarkRed);

                //Print board for the field
                //PrintTheSideBoard(8);
                //PrintTheSideBoard(40);
            }
            PrintOnPosition(8, 5, "Game Over!!!", ConsoleColor.DarkRed);
        }

        private static void PrintHoles()
        {
            PrintOnPosition(13, 9, "|   | ", ConsoleColor.DarkRed);
            PrintOnPosition(22, 9, "|   | ", ConsoleColor.DarkRed);
            PrintOnPosition(31, 9, "|   | ", ConsoleColor.DarkRed);
        }

        /// <summary>
        /// Move the bridge with keys.
        /// </summary>
        /// <param name="userObject">bridge object</param>
        /// <param name="playfieldWidth">play field</param>
        private static void MoveTheBridge(Object userObject, int playfieldWidth)
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (userObject.X > 15)
                    {
                        userObject.X -= 9;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (userObject.X + 1 < playfieldWidth - 7)
                    {
                        userObject.X += 9;
                    }
                }
            }
            
        }

        /// <summary>
        /// Print the side board
        /// </summary>
        /// <param name="x">x coordinate</param>
        private static void PrintTheSideBoard(int x)
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                PrintOnPosition(x, i, "|", ConsoleColor.White);
            }
        }

        /// <summary>
        /// Print on position.Console.SetCursorPosition move our cursor in place of what we write.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="symbol">our symbol</param>
        /// <param name="color">color Of symbol</param>
        private static void PrintOnPosition(int x, int y, string symbol, ConsoleColor color = ConsoleColor.Green)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
    }
}