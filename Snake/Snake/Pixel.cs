using System;


namespace Snake
{
    public readonly struct Pixel
    {
        public const char PixelChar = '█';
        private readonly int PixelSize;

        public Pixel(int x, int y, ConsoleColor color, int pixelSize = 3)
        {
            X = x;
            Y = y;
            Color = color;
            this.PixelSize = pixelSize;
        }
        public int X { get; }
        public int Y { get; }

        public ConsoleColor Color { get; }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(left: X * PixelSize + x, top: Y * PixelSize + y);
                    Console.Write(PixelChar);
                }
            }
          
        }

        public void Clear()
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(left: X * PixelSize + x, top: Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }

    }

    
}
