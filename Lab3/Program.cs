namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            SolverUI ui = new SolverUI(new KramerMethod());
            ui.Launch();
        }
    }
}
