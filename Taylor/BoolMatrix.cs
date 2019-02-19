using System;
using System.Text;

namespace Taylor
{
    public class BoolMatrix
    {
        private int _size;
        private bool[,] _array;

        public BoolMatrix(int size) // TODO generalize to m x n rather than n x n
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

        public LargestSquareResult GetLargestSquare_Naive()
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
        public LargestSquareResult GetLargestSquare_Accumulating()
        {
            var result = new LargestSquareResult
            {
                Size = 0,
                StartX = -1,
                StartY = -1,
            };

            // track the maximum size square with (x,y) as its bottom right (as value x,y in accumulationMatrix)
            var accumulationMatrix = new int[_size, _size];

            // see the accumulation matrix with the top and left rows values (mapping true => 1, false => 0)
            for (int i = 0; i < _size; i++)
            {
                int value = this[i, 0] ? 1 : 0;
                accumulationMatrix[i, 0] = value;
                if (value > result.Size)
                {
                    result.Size = value;
                    result.StartX = 0;
                    result.StartY = i;
                }
            }
            for (int j = 1; j < _size; j++)
            {
                int value = this[0, j] ? 1 : 0;
                accumulationMatrix[0, j] = value;
                if (value > result.Size)
                {
                    result.Size = value;
                    result.StartX = j;
                    result.StartY = 0;
                }
            }

            // now iterate row by row calculating the maximum square size
            // using the previously calculated values above and to the left 
            for (int i = 1; i < _size; i++)
            {
                for (int j = 1; j < _size; j++)
                {
                    if (this[i, j])
                    {
                        int minimalNeighbouringSquare = Math.Min(
                                                            accumulationMatrix[i, j - 1],
                                                            Math.Min(accumulationMatrix[i - 1, j], accumulationMatrix[i - 1, j - 1]));
                        int maximumSquare = 1 + minimalNeighbouringSquare;
                        accumulationMatrix[i, j] = maximumSquare;
                        if (maximumSquare > result.Size)
                        {
                            result.Size = maximumSquare;
                            // translate bottom right to top left
                            result.StartX = j - maximumSquare + 1;
                            result.StartY = i - maximumSquare + 1;
                        }
                    }
                    else
                    {
                        accumulationMatrix[i, j] = 0;
                    }
                }
            }
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
