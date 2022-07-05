using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class snake
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;

        public Pixel Head { get; private set; }

        public snake(int initialX, 
            int initialY, 
            ConsoleColor headColor, 
            ConsoleColor bodyColor, 
            int bodyLength = 3)
        {
           
            this._headColor = headColor;
            this._bodyColor = bodyColor;

            Head = new Pixel(initialX, initialY, _headColor);

            for (int i = bodyLength; i>=0; i--)
            {
                Body.Enqueue(item: new Pixel(x: Head.X - i - 1, initialY, _bodyColor));
            }

            Draw();
        }

        public void Move(Direction direction, bool eat = false)
        {
            Clear();
            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor));
           
            if(!eat)
                Body.Dequeue();
           
            Head = direction switch
            {
                Direction.Right => new Pixel(Head.X + 1, Head.Y, _headColor),
                Direction.Left => new Pixel(Head.X - 1, Head.Y, _headColor),
                Direction.Up => new Pixel(Head.X, Head.Y + 1, _headColor),
                Direction.Down => new Pixel(Head.X, Head.Y - 1, _headColor),
                _ => Head
            };

            Draw();
        }

        public Queue<Pixel> Body { get; } = new Queue<Pixel>();


        public void Draw()
        {
            Head.Draw();

            foreach(Pixel pixel in Body)
            {
                pixel.Draw();
            }
        }

        public void Clear()
        {
            Head.Clear();

            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }

    }
}
