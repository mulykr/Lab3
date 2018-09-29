namespace Lab3
{
    public class Determinant
    {
        /// <summary>
        /// Calculates matrix's determinant
        /// </summary>
        /// <param name="matrix">Matrix to be calculated</param>
        /// <returns>Determinant, <see cref="double"/></returns>
        public static double Calculate(double[,] matrix)
        {
            if (System.Math.Sqrt(matrix.Length) != System.Math.Round(System.Math.Sqrt(matrix.Length)))
            {
                throw new System.ArgumentException("Incorrect matrix size");
            }

            if (matrix.Length == 1)
            {
                return matrix[0, 0];
            }
            else if (matrix.Length == 4)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                double sign = 1, result = 0;
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    double[,] minor = GetMinor(matrix, i);
                    result += sign * matrix[0, i] * Calculate(minor);
                    sign = -sign;
                }
                return result;
            }
        }

        private static double[,] GetMinor(double[,] matrix, int n)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result;
        }
    }
}
