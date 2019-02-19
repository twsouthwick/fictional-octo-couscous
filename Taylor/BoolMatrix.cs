using System;
using System.Text;

namespace Taylor
{
    public class BoolMatrix
    {
        private int _size;
        private bool[,] _array;

        public BoolMatrix(int size)
        {
            _size = size;
            _array = new bool[size, size];
        }

        public static BoolMatrix Parse(string text)
        {
            var rows = text.Split("\n");
            var matrix = new BoolMatrix(rows.Length);
            for (int i = 0; i < rows.Length; i++)
            {
                var values = rows[i].Trim().Split(" ");
                if (values.Length != rows.Length)
                {
                    throw new ArgumentException("Rows and columns don't match");
                }
                for (int j = 0; j < values.Length; j++)
                {
                    bool value;
                    switch (values[j])
                    {
                        case "0":
                            value = false;
                            break;
                        case "1":
                            value = true;
                            break;
                        default:
                            throw new ArgumentException("Invalid character in input");
                    }
                    matrix[i, j] = value;
                }
            }
            return matrix;
        }
        public override string ToString()
        {
            var buf = new StringBuilder(); // TODO estimate the size 
            var rowSeparator = "";
            for (int i = 0; i < _size; i++)
            {
                buf.Append(rowSeparator);
                string columnSeparator = "";
                for (int j = 0; j < _size; j++)
                {
                    buf.Append(columnSeparator);
                    buf.Append(this[i, j] ? "1" : "0");
                    columnSeparator = " ";
                }
                rowSeparator = "\r\n";
            }
            return buf.ToString();
        }

        public bool this[int x, int y]
        {
            get => _array[x, y];
            set => _array[x, y] = value;
        }

        public LargestSquareResult GetLargestSquare()
        {
            var result = new LargestSquareResult
            {
                Size = 0,
                StartX = -1,
                StartY = -1,
            };

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    var size = GetSizeOfLargestSquare(i, j);
                    if (size > result.Size)
                    {
                        result.Size = size;
                        result.StartX = j;
                        result.StartY = i;
                    }
                }
            }
            // TODO - short circuit if largest possible square for a given i/j is smaller than the current largest found

            return result;
        }
        private int GetSizeOfLargestSquare(int startX, int startY)
        {
            int largestSquare = 0;
            while (startX + largestSquare < _size && startY + largestSquare < _size)
            {
                // check new x-axis values
                for (int i = startX; i <= startX + largestSquare; i++)
                {
                    if (!this[i, startY + largestSquare])
                    {
                        // found a false - return current largest square
                        return largestSquare;
                    }
                }

                // check new y-axis values (we can skip the last value as it overlaps with the x-axis checks)
                for (int j = startY; j < startY + largestSquare; j++)
                {
                    if (!this[startX + largestSquare, j])
                    {
                        // found a false - return current largest square
                        return largestSquare;
                    }
                }
                largestSquare++;
            }

            return largestSquare;
        }
    }
}
