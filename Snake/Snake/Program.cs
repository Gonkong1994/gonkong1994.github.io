using System;
using static System.Console;
using System.Diagnostics;

namespace Snake
{
    class Program
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;

        private const int ScreenWidth = MapWidth * 3;
        private const int ScreenHeight = MapHeight * 3;

        private const int FrameMs = 200;

        private const ConsoleColor BorderColor = ConsoleColor.Gray;

        private const ConsoleColor HeadColor = ConsoleColor.DarkBlue;
        private const ConsoleColor BodyColor = ConsoleColor.Cyan;

        private const ConsoleColor FoodColor = ConsoleColor.Green;

        private static readonly Random Random = new Random();

        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;

            while (true)
            {
                StartGame();
                Thread.Sleep(1000);
                ReadKey();
            }

            
        }

        static void StartGame()
        {
            Clear();
            DrawBorder();
            Direction currentMovement = Direction.Right;
            var snake = new snake(initialX: 10, initialY: 5, HeadColor, BodyColor);

            int score = 0;

            int lagMs = 0;

            Pixel food = GenFood(snake);
            food.Draw();

            var sw = new Stopwatch();

            while (true)
            {
                sw.Restart();

                Direction oldMovement = currentMovement;

                while (sw.ElapsedMilliseconds <= FrameMs - lagMs)
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadMovement(currentMovement);
                    }

                }

                sw.Restart();

                if(snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMovement, true);

                    food = GenFood(snake);
                    food.Draw();

                    score++;

                    Task.Run(() => Beep(1200, 200));
                }
                else
                {
                    snake.Move(currentMovement);
                }
                

                if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;

                lagMs = (int)sw.ElapsedMilliseconds;

            }

            snake.Clear();
            food.Clear();

            SetCursorPosition(ScreenWidth / 3, ScreenHeight / 2);
            WriteLine($"GameOver, score: {score}");

            Task.Run(() => Beep(200, 600));
        }

        static Pixel GenFood(snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(x: Random.Next(1, MapWidth - 2), y: Random.Next(1, MapHeight - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
                || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }

            static Direction ReadMovement(Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;
            ConsoleKey key = ReadKey(intercept:true).Key;
            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.DownArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };
            return currentDirection;
        }

            static void DrawBorder()
            {
                for (int i = 0; i < MapWidth; i++)
                {
                    new Pixel(x:i, y: 0, BorderColor).Draw();
                    new Pixel(x: i, y: MapHeight - 1, BorderColor).Draw();
                }


                for (int i = 0; i < MapHeight; i++)
                {
                    new Pixel(x: 0, y: i, BorderColor).Draw();
                    new Pixel(x: MapWidth - 1 , y: i, BorderColor).Draw();
                }

            }
        
    }
}
