using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Snake
    {
        public List <Point> Body { get; private set; }
        public Movement Movement { get; private set; }

        private int width;
        private int height;

        public Snake(int StartX, int StartY, int width, int height)
        {
            Body = new List<Point> { new Point(StartX, StartY) };
            Movement = new Movement();
            this.width = width;
            this.height = height;
        }

        public void Move(bool grow)
        {
            Movement.Update();
            Point newHead = Movement.GetNextPosition(Body[0]);
            Body.Insert(0, newHead);

            if (!grow)
            {
                Body.RemoveAt(Body.Count - 1);
            }
        }

        public bool CheckSelfCollision()
        {
            Point head = Body[0];
            for (int i = 1; i < Body.Count; i++)
            {
                if (Body[i] == head)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckWallCollision()
        {
            Point head = Body[0];
            return head.X < 0 || head.X >= width || head.Y < 0 || head.Y >= height;
        }
    }
}
