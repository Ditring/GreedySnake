using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace GreedySnakeGame
{
    public partial class GreedySnake : Form
    {
        public GreedySnake()
        {
            InitializeComponent();
            eggList.Add(new Egg(200, 90));
            GameStart();
            DrawFood();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
            keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);
        }

        public Snake snake;

        public static List<Egg> eggList = new List<Egg>();


        public void DrawSnake()
        {
            foreach(SnakeBodyPoint snakeBodyPoint in snake.bodyList)
            {
                DrawPoint(snakeBodyPoint);
            }
        }

        public void DrawPoint(IEntity entity)
        {
            panel1.Invalidate();
            panel1.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                SolidBrush myBrush = new SolidBrush(entity.EntityColor);
                g.FillRectangle(myBrush, entity.Location_X, entity.Location_Y, 10, 10);
            };
        }
        public void DrawFood()
        {
            foreach (Egg egg in eggList)
            {
                DrawPoint(egg);
            }
        }



        public void LiveOrDie(SnakeBodyPoint snakeHead)
        {
            Boolean IsDead = false;
            if (!(snakeHead.Location_X <= this.panel1.Width - 10 && snakeHead.Location_X >= 10 && snakeHead.Location_Y <= this.panel1.Height - 10 && snakeHead.Location_Y >= 10))
            {
                IsDead = true;
            }
            foreach(SnakeBodyPoint snakeBodyPoint in snake.bodyList)
            {
                if (snakeBodyPoint.Equals(snakeHead)) continue;
                if (snakeHead.Location_X == snakeBodyPoint.Location_X && snakeHead.Location_Y == snakeBodyPoint.Location_Y)
                {
                    IsDead = true;
                }
            }
            if (IsDead)
            {
                StartTimer.Stop();
                
                if (MessageBox.Show("游戏将重启", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    foreach (SnakeBodyPoint snakeBodyPoint in snake.bodyList)
                    {
                        snakeBodyPoint.EntityColor = panel1.BackColor;
                        DrawPoint(snakeBodyPoint);
                    }
                    GameStart();
                }
            }
            else
            {
                if (snake.tryEat())
                {
                    CreateNewEgg();
                }
                
            }
                
        }

        public void CreateNewEgg()
        {
            bool IsCreate = false;
            while (!IsCreate)
            {
                eggList[0].Location_X = new Random().Next(0, 49) * 10;
                eggList[0].location_Y = new Random().Next(0, 49) * 10;
                IsCreate = true;
                for (int i = 0; i < snake.bodyList.Count; i++)
                {
                    if (snake.bodyList[i].Location_X == eggList[0].Location_X && snake.bodyList[i].Location_Y == eggList[0].Location_Y)
                    {
                        IsCreate = false;
                        break;
                    }
                }
                if (IsCreate)
                {
                    if (!(eggList[0].Location_X <= this.panel1.Width - 10 && eggList[0].Location_X >= 10 && eggList[0].Location_Y <= this.panel1.Height - 10 && eggList[0].Location_Y >= 10))
                    {
                        IsCreate = false;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if(snake.direction != EDirection.Down)
                        snake.direction = EDirection.Up;
                    break;
                case Keys.Down:
                    if (snake.direction != EDirection.Up)
                        snake.direction = EDirection.Down;
                    break;
                case Keys.Left:
                    if (snake.direction != EDirection.Right)
                        snake.direction = EDirection.Left;
                    break;
                case Keys.Right:
                    if( snake.direction != EDirection.Left)
                     snake.direction = EDirection.Right;
                    break;
            }
        }



        private void GameStart()
        {
            snake = new Snake();
            CreateNewEgg();
            StartTimer.Start();
            StartTimer.Interval = 200;
        }

        private void Clear()
        {
            panel1.Invalidate();
            panel1.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.Clear(panel1.BackColor);
            };
        }

        private void StartTimer_Tick(object sender, EventArgs e)
        {
            DrawSnake();
            DrawFood();
            LiveOrDie(snake.bodyList[snake.bodyList.Count-1]);
            snake.Move();
        }
    }
}
