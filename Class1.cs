using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace GreedySnakeGame
{
    public enum EDirection
    {
        Up,Down,Left,Right
    }


    public class Entity : IEntity
    {
        public Color color;
        public int location_X;
        public int location_Y;
        public Color EntityColor
        {
            get { return color; }
            set { color = value; }
        }
        public int Location_X
        {
            get { return this.location_X; }
            set { this.location_X = value; }
        }
        public int Location_Y
        {
            get { return this.location_Y; }
            set { this.location_Y = value; }
        }

        public void Clear()
        {
        }
    }



    public class Snake{

        public EDirection direction;
        public List<SnakeBodyPoint> bodyList = new List<SnakeBodyPoint>();
        public void Turn(EDirection direction) 
        {
            this.direction = direction;
        }

        public void Move()
        {
            int i = bodyList.Count - 1;
            switch (this.direction)
            {
                case EDirection.Up:
                    ChangeSnakeLine();
                    bodyList[i].Location_Y -= 10;
                    break;
                case EDirection.Left:
                    ChangeSnakeLine();
                    bodyList[i].Location_X -= 10;
                    break;
                case EDirection.Down:
                    ChangeSnakeLine();
                    bodyList[i].Location_Y += 10;
                    break;
                case EDirection.Right:
                    ChangeSnakeLine();
                    bodyList[i].Location_X += 10;
                    break;
            }
        }

        public void ChangeSnakeLine()
        {
            int j = 0;
            int x, y;
            int i = bodyList.Count - 1;
            for (j = 0; j < i; j++)
            {
                x = bodyList[j + 1].Location_X;
                y = bodyList[j + 1].Location_Y;
                bodyList[j].Location_Y = y;
                bodyList[j].Location_X = x;
            }
        }



        public Snake()
        {
            for(int i = 0; i < 4; i++)
            {
                SnakeBodyPoint bodyPoint = new SnakeBodyPoint(10 + i * 10, 200);
                bodyList.Add(bodyPoint);
            }
        }
        public Boolean tryEat()
        {
            Boolean IsAte = false;
            foreach(Egg egg in GreedySnake.eggList)
            {
                if (this.bodyList[this.bodyList.Count - 1].Location_X == egg.Location_X&& this.bodyList[this.bodyList.Count - 1].Location_Y == egg.Location_Y)
                {
                    this.bodyList.Add(ConvertEggToSnakeBodyPoint(egg));
                    IsAte= true; break;
                }
            }
            return IsAte;

        }

        public SnakeBodyPoint ConvertEggToSnakeBodyPoint(Egg egg)
        {
            SnakeBodyPoint snakeBodyPoint = new SnakeBodyPoint(egg.location_X, egg.location_Y);
            return snakeBodyPoint;
        }
    }

    public class Egg : Entity
    {
        public Egg(int location_X, int location_Y)
        {
            this.color = Color.Goldenrod;
            this.Location_X = location_X;
            this.Location_Y = location_Y;
        }
        public void RandomPos()
        {
        }
    }
    public class SnakeBodyPoint : Entity
    {
        public SnakeBodyPoint(int location_X, int location_Y)
        {
            this.color = Color.Green;
            this.Location_X = location_X;
            this.Location_Y = location_Y;
        }
    }
}
