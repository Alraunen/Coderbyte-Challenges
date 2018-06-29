using System;
using System.Collections.Generic;
using System.Linq;

namespace Alraunen.ClosestEnemyII
{
    public class Program
    {
        private static Func<string[], int[][]> _toIntArray = input =>
            input.Select(x => x.Select(y => Int32.Parse(y.ToString())).ToArray()).ToArray();

        private static void Main(string[] args)
        {
            Console.WriteLine(ClosestEnemyII(new string[]
            {
                "0000",
                "1000",
                "0002",
                "0002"
            }));
            Console.WriteLine(ClosestEnemyII(new string[]
            {
                "0000",
                "0001",
                "2000",
                "2000"
            }));
            Console.WriteLine(ClosestEnemyII(new string[]
            {
                "000",
                "100",
                "200"
            }));
            Console.WriteLine(ClosestEnemyII(new string[]
            {
                "200",
                "000",
                "100"
            }));
            Console.WriteLine(ClosestEnemyII(new string[]
            {
                "002",
                "000",
                "100"
            }));
            Console.WriteLine(ClosestEnemyII(new string[]
            {
                "0000",
                "2010",
                "0000",
                "2002"
            }));
            Console.ReadKey();
        }

        public static int ClosestEnemyII(string[] strArr)
        {
            var result = 0;

            var data = _locate(_toIntArray(strArr));
            foreach (var enemy in data.Twos)
            {
                var distance =
                    new int[]
                    {
                        Math.Abs(data.One.X - enemy.X),
                        Math.Abs(data.One.X - enemy.X + data.Rows),
                        Math.Abs(data.One.X - enemy.X - data.Rows)
                    }.Min() +
                    new int[]
                    {
                        Math.Abs(data.One.Y - enemy.Y),
                        Math.Abs(data.One.Y - enemy.Y + data.Columns),
                        Math.Abs(data.One.Y - enemy.Y - data.Columns)
                    }.Min();
                result = result == 0 ? distance : Math.Min(result, distance);
            }

            return result;
        }

        private static FieldData _locate(int[][] input)
        {
            var xLength = input.Length;
            var yLength = input.Max(x => x.Length);
            var result = new FieldData(xLength, yLength);
            for (int row = 0; row < xLength; row++)
                for (int column = 0; column < yLength; column++)
                {
                    if (input[row].Length < column)
                        throw new ArgumentException("Ungleiche Spaltenlängen gefunden; Fläche ist kein Rechteck.");

                    switch (input[row][column])
                    {
                        case 0:
                            continue;
                        case 1:
                            result.SetOne(new Point(row, column));
                            break;
                        case 2:
                            result.AddTwo(new Point(row, column));
                            break;
                        default:
                            throw new ArgumentException("Ungültiger Wert gefunden: " + input[row][column]);
                    }
                }
            return result;
        }

        #region Nested Classes
        private struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        private class FieldData
        {
            private List<Point> _twos;
            private bool _isOneSet;

            public Point One { get; private set; }
            public IEnumerable<Point> Twos { get { return _twos; } }
            public int Rows { get; }
            public int Columns { get; }

            public FieldData(int rows, int columns)
            {
                Rows = rows;
                Columns = columns;
                _twos = new List<Point>();
            }

            public void SetOne(Point one)
            {
                if (_isOneSet)
                    throw new ArgumentException("Mehrere Startpunkte gefunden.");
                One = one;
                _isOneSet = true;
            }

            public void AddTwo(Point two)
            {
                _twos.Add(two);
            }
        }
        #endregion
    }
}
