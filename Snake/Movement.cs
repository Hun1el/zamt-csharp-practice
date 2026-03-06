using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Movement
    {
        private string direction = "Right";
        private string nextdirection = "Right";
        public string Direction => direction;

        public void SetDirection(string newdirection)
        {
            if ((newdirection == "Up" && direction != "Down") ||
                (newdirection == "Down" && direction != "Up") ||
                (newdirection == "Left" && direction != "Right") ||
                (newdirection == "Right" && direction != "Left"))
            {
                nextdirection = newdirection;
            }
        }

        public void Update()
        {
            direction = nextdirection;
        }

        public Point GetNextPosition(Point currentHead)
        {
            switch (direction)
            {
                case "Up":
                    return new Point(currentHead.X, currentHead.Y - 1);
                case "Down":
                    return new Point(currentHead.X, currentHead.Y + 1);
                case "Left":
                    return new Point(currentHead.X - 1, currentHead.Y);
                case "Right":
                    return new Point(currentHead.X + 1, currentHead.Y);
                default:
                    return currentHead;
            }
        }
    }
}
