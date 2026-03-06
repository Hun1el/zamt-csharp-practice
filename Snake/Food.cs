using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    public class Food
    {
        public Point Position { get; private set; }
        private Random random;

        public Food(int width, int height, List<Point> rocks)
        {
            random = new Random();
            NewGenerate(width, height, rocks);
        }

        public void NewGenerate(int width, int height, List<Point> rocks)
        {
            do
            {
                Position = new Point(random.Next(0, width), random.Next(0, height));
            }
            while (rocks.Contains(Position));
        }
    }
}
