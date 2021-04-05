using System;
using term2d;

namespace Term2DGame
{
    class TrailDemoGame : Game
    {
        public static void Main(string[] args)
        {
            Term2D.Start(new TrailDemoGame());
        }

        override public void Init(Canvas canvas)
        {
            Console.Title = "Trail Demo";
            canvas.DefaultBackgroundColor = ConsoleColor.Gray;
            canvas.Clear();
        }

        // Timer, Used For Consistent Motion Intervals
        double timer = 0.0;
        // Position Of Block
        int colPos = 0;
        int rowPos = 0;
        // Motion Direction
        bool moveRight = true;
        bool moveUp = true;
        // Used For Block Colors
        Random random = new Random();
        string[] colorNames = Enum.GetNames(typeof(ConsoleColor));
        ConsoleColor blockColor = ConsoleColor.Red;
        override public bool Update(UpdateInfo updateInfo)
        {
            // Input
            if (updateInfo.HasUnreadInput)
            {
                switch (updateInfo.LastInput)
                {
                    case ConsoleKey.UpArrow:
                        moveUp = true;
                        break;
                    case ConsoleKey.DownArrow:
                        moveUp = false;
                        break;
                    case ConsoleKey.RightArrow:
                        moveRight = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        moveRight = false;
                        break;
                    case ConsoleKey.Escape:
                        return false;
                }
            }
            // Render
            Canvas canvas = updateInfo.ActiveCanvas;
            // canvas.Clear(); - Enable To Clear Trails
            timer += updateInfo.DeltaTime;
            // Choose Random Color For Block (Though Only Set If Wall Collision)
            ConsoleColor randomColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), colorNames[random.Next(colorNames.Length)]);
            // Move Every Time Timer Reaches Interval, Then Reset Timer
            if (timer >= 0.05)
            {
                // Keep Moving Right / Left
                if (moveRight)
                {
                    if (colPos < canvas.GetWidth() - 1)
                    {
                        colPos++;
                    }
                    else
                    {
                        moveRight = false;
                        blockColor = randomColor;
                    }
                }
                else
                {
                    if (colPos > 0)
                    {
                        colPos--;
                    }
                    else
                    {
                        moveRight = true;
                        blockColor = randomColor;
                    }
                }
                // Keep Moving Up / Down
                if (moveUp)
                {
                    if (rowPos > 0)
                    {
                        rowPos--;
                    }
                    else
                    {
                        moveUp = false;
                        blockColor = randomColor;
                    }
                }
                else
                {
                    if (rowPos < canvas.GetHeight() - 1)
                    {
                        rowPos++;
                    }
                    else
                    {
                        
                        moveUp = true;
                        blockColor = randomColor;
                    }
                }
                // Reset Timer
                timer = 0.0;
            }
            canvas.Draw(rowPos, colPos, '█', blockColor, ConsoleColor.Gray);
            return true;
        }
    }
}