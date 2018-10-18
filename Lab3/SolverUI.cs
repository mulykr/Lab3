using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class SolverUI
    {
        private ISolveAlgorithm algorithm;
        private int size;
        private double[,] matrix;
        private double[] koeficients;

        public SolverUI(ISolveAlgorithm solveAlgorithm)
        {
            algorithm = solveAlgorithm;
        }

        public void Launch()
        {
            Console.WriteLine("Try random filling? (1/0)");
            int ans = int.Parse(Console.ReadLine());
            if (ans == 1)
            {
                InputSizeAndInit();
                Random rnd = new Random();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = rnd.NextDouble() * 10;
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    koeficients[i] = rnd.NextDouble() * 10;
                }
            }
            else
            {
                InputSizeAndInit();
                InputMatrix();
                InputKoeficients();
            }

            ShowResults();
        }

        private void InputSizeAndInit()
        {
            Console.WriteLine("Please, enter matrix size :)");
            size = int.Parse(Console.ReadLine());
            matrix = new double[size, size];
            koeficients = new double[size];
        }
        private void InputMatrix()
        {
            Console.WriteLine("Please, enter matrix of the system :)");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write($"M[{i + 1}, {j + 1}] = ");
                    matrix[i, j] = double.Parse(Console.ReadLine());
                }
            }
        }

        private void InputKoeficients()
        {
            Console.WriteLine("Please, enter koeficients :)");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"K[{i + 1}] = ");
                koeficients[i] = double.Parse(Console.ReadLine());
            }
        }

        private void ShowResults()
        {
            Console.WriteLine("Results:");
            double[] result = algorithm.NonParallelSolve(matrix, koeficients);
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"X{i + 1} = {result[i]}");
            }
            Console.WriteLine("Results:");
            result = algorithm.Solve(matrix, koeficients);
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"X{i + 1} = {result[i]}");
            }
        }
    }
}
