namespace Lab3
{
    public interface ISolveAlgorithm
    {
        double[] Solve(double[,] matrix, double[] koeficients);
        double[] NonParallelSolve(double[,] matrix, double[] koeficients);
    }
}
