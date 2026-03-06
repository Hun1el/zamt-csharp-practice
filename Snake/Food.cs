using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Food
    {
        public Point Position { get; private set; }
        private Random random;

        public Food(int width, int height)
        {
            random = new Random();
            NewGenerate(width, height, null);
        }

        public void NewGenerate(int width, int height, List<Point> snakeBody)
        {
            do
            {
                Position = new Point(random.Next(0, width), random.Next(0, height));
            }
            while (snakeBody != null && snakeBody.Contains(Position));
        }
    }
}
