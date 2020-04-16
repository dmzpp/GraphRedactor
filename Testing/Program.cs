using System;

namespace Testing
{
    class Data
    {
        public int x, y;
        public Data(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Program
    {
        static double interpolate(Data[] f, int xi, int n)
        {
            double result = 0;

            for (int i = 0; i < n; i++)
            {
                double term = f[i].y;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                        term = term * (xi - f[j].x) /
                                  (f[i].x - f[j].x);
                }

                result += term;
            }
            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
