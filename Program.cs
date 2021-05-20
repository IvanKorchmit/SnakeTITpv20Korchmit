using System;
using System.Threading;
namespace SnakeTITpv20Korchmit
{
    class Program
    {
        public static Tile[,] tiles;
        public static Snake snake;
        public static Snake tail;
        static void Main(string[] args)
        {
            Random rand = new Random();
            tiles = new Tile[25, 25];
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[y, x] = new Tile(' ', x, y);
                    if (x == tiles.GetLength(0) / 2 && y == tiles.GetLength(1) / 2)
                    {
                        Snake snake = new Snake('S', true, y, x);
                        snake.direction = Direction.LEFT;
                        tiles[y, x] = snake;
                        Program.snake = snake;
                        tail = snake;
                    }
                }
            }
            int x2 = rand.Next(0, tiles.GetLength(0) - 1);
            int y2 = rand.Next(0, tiles.GetLength(1) - 1);
            tiles[x2, y2] = new Food(false, 'F', x2, y2);
            while (true)
            {
                snake.Move();
                if(snake != tail)
                {
                    tail.Move();
                }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo k = Console.ReadKey();
                    if (k.Key == ConsoleKey.RightArrow)
                    {
                        snake.direction = Direction.RIGHT;

                    }
                    else if (k.Key == ConsoleKey.LeftArrow)
                    {
                        snake.direction = Direction.LEFT;
                    }
                    else if (k.Key == ConsoleKey.UpArrow)
                    {
                        snake.direction = Direction.UP;
                    }
                    else if (k.Key == ConsoleKey.DownArrow)
                    {
                        snake.direction = Direction.DOWN;
                    }
                }
                render();
                Thread.Sleep(100);
            }
        }
        static void render()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    Console.Write(tiles[x, y].symbol);
                }
                Console.WriteLine();
            }
        }
    }
    public class Tile
    {
        public char symbol;
        public int x, y;
        public Direction prevDirection;
        public Tile(char symbol, int x, int y)
        {
            this.symbol = symbol;
            this.x = x;
            this.y = y;
        }
        public Tile(char symbol, int x, int y, Direction prevDirection)
        {
            this.symbol = symbol;
            this.x = x;
            this.y = y;
            this.prevDirection = prevDirection;
        }
        public Tile(Tile tile)
        {
            symbol = tile.symbol;
            x = tile.x;
            y = tile.y;
        }
        public Tile Empty
        {
            get
            {
                return new Tile(' ', x, y, prevDirection);
            }
        }
    }
    public class Snake : Tile
    {
        public bool Head;
        public Direction direction;
        public Snake(char symbol, bool Head, int x, int y) : base(symbol, x, y)
        {
            this.Head = Head;
        }
        public void Move()
        {
            switch (direction)
            {
                case Direction.UP:
                    if (Program.tiles[x - 1, y] is Food)
                    {
                        Grow();
                    }
                        if (!Head)
                        {
                            direction = 
                        }
                    Program.tiles[x - 1, y] = this;
                    Program.tiles[x, y].prevDirection = direction;
                    Program.tiles[x, y] = Program.tiles[x, y].Empty;
                    x--;
                    break;
                case Direction.RIGHT:
                    if (Program.tiles[x, y + 1] is Food)
                    {
                        Grow();
                    }
                    else if (Program.tiles[x, y + 1] is Snake s)
                    {
                        if (!Head)
                        {
                            direction = s.direction;
                        }
                    }
                    Program.tiles[x, y + 1] = this;
                    Program.tiles[x, y].prevDirection = direction;
                    Program.tiles[x, y] = Program.tiles[x, y].Empty;
                    y++;
                    break;
                case Direction.LEFT:
                    if (Program.tiles[x, y - 1] is Food)
                    {
                        Grow();
                    }
                    else if (Program.tiles[x, y - 1] is Snake s)
                    {
                        if (!Head)
                        {
                            direction = s.direction;
                        }
                    }
                    Program.tiles[x, y - 1] = this;
                    Program.tiles[x, y].prevDirection = direction;
                    Program.tiles[x, y] = Program.tiles[x, y].Empty;
                    y--;
                    break;
                case Direction.DOWN:
                    if (Program.tiles[x + 1, y] is Food)
                    {
                        Grow();
                    }
                    else if (Program.tiles[x + 1, y] is Snake s)
                    {
                        if (!Head)
                        {
                            direction = s.direction;
                        }
                    }
                    Program.tiles[x + 1, y] = this;
                    Program.tiles[x, y] = Program.tiles[x, y].Empty;
                    Program.tiles[x, y].prevDirection = direction;
                    x++;
                    break;
            }
        }
        public void Grow()
        {
            switch (Program.tail.direction)
            {
                case Direction.UP:
                    Program.tail = new Snake('X', false, x + 1, y);
                    Program.tiles[x + 1, y] = Program.tail;
                    break;
                case Direction.RIGHT:
                    Program.tail = new Snake('X', false, x, y - 1);
                    Program.tiles[x, y - 1] = Program.tail;
                    break;
                case Direction.LEFT:
                    Program.tail = new Snake('X', false, x, y + 1);
                    Program.tiles[x, y + 1] = Program.tail;
                    break;
                case Direction.DOWN:
                    Program.tail = new Snake('X', false, x - 1, y);
                    Program.tiles[x - 1, y] = Program.tail;
                    break;
                default:
                    break;
            }
            Program.tail.direction = Program.snake.direction;
        }
    }
    public class Food : Tile
    {
        public bool isBad;
        public Food(bool isBad, char symbol, int x, int y) : base(symbol, x, y)
        {
            this.isBad = isBad;
        }
    }
    public enum Direction
    {
        UP, RIGHT, LEFT, DOWN
    }


}