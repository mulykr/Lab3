using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace Lab3
{
    public class KramerMethod : ISolveAlgorithm
    {
        const int THREADS_COUNT = 4;
        private static object obj = new object();

        public double[] Solve(double[,] matrix, double[] koeficients)
        {
            System.Console.WriteLine("Solving by Kramer (Parallel) is started!");
            System.Console.WriteLine("Proccessing...");
            Stopwatch sw = new Stopwatch();
            
            int size = matrix.GetLength(0);
            double[] results = new double[size];
            System.Console.WriteLine("Calculating D...");
            double D = Determinant.Calculate(matrix);
            System.Console.WriteLine("Calculating D finished!");
            sw.Start();
            double[] d = new double[size];
            //double[,] tempMatrix = new double[size, size];
            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        for (int k = 0; k < size; k++)
            //        {
            //            if (k == i)
            //            {
            //                tempMatrix[j, k] = koeficients[j];
            //                continue;
            //            }
            //            tempMatrix[j, k] = matrix[j, k];
            //        }
            //    }

            //    d[i] = Task.Factory.StartNew(() => Determinant.Calculate(tempMatrix)).Result;
            //    System.Console.WriteLine($"d[{i + 1}] = {d[i]}");
            //}
            Thread[] list = new Thread[THREADS_COUNT];
            for (int i = 0; i < THREADS_COUNT; ++i)
            {
                double diapazone = size / THREADS_COUNT;
                int begin = (int)(i * diapazone);
                int end = i == THREADS_COUNT-1 ? size : (int)((i + 1) * diapazone);

                list[i] = new Thread(() => GetPartialDeterminants(begin, end, size, ref matrix, ref koeficients, ref d));
                list[i].Start();
            }

            for (int i = 0; i < THREADS_COUNT; i++)
            {
                list[i].Join();
            }

            for (int i = 0; i < size; i++)
            {
                results[i] = d[i] / D;
            }
            sw.Stop();
            System.Console.WriteLine($"Finished. Time elapsed: {sw.ElapsedMilliseconds}");
            NonParallelSolve(matrix, koeficients);
            return results;
        }

        private void GetPartialDeterminants(int start, int finish, int size, ref double[,] matrix, ref double[] koeficients, ref double[] d)
        {
            lock (obj)
            {
                double[,] tempMatrix = new double[size, size];
                for (int i = start; i < finish; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < size; k++)
                        {
                            if (k == i)
                            {
                                tempMatrix[j, k] = koeficients[j];
                                continue;
                            }
                            tempMatrix[j, k] = matrix[j, k];
                        }
                    }
                    d[i] = Determinant.Calculate(tempMatrix);
                    System.Console.WriteLine($"d[{i + 1}] = {d[i]}");
                }
            }
        }

        private void NonParallelSolve(double[,] matrix, double[] koeficients)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            System.Console.WriteLine("Solving by Kramer (Non-Parallel) is started!");
            System.Console.WriteLine("Proccessing...");
            
            int size = matrix.GetLength(0);
            double[] results = new double[size];
            System.Console.WriteLine("Calculating D...");
            double D = Determinant.Calculate(matrix);
            System.Console.WriteLine("Calculating D finished!");
            double[] d = new double[matrix.GetLength(0)];
            double[,] tempMatrix;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                tempMatrix = new double[size, size];
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (k == i)
                        {
                            tempMatrix[j, k] = koeficients[j];
                            continue;
                        }
                        tempMatrix[j, k] = matrix[j, k];
                    }
                }

                d[i] = Determinant.Calculate(tempMatrix);
                System.Console.WriteLine($"d[{i + 1}] = {d[i]}");
            }

            for (int i = 0; i < size; i++)
            {
                results[i] = d[i] / D;
            }

            sw.Stop();
            System.Console.WriteLine($"Finished. Time elapsed: {sw.ElapsedMilliseconds}");
        }
    }
}
