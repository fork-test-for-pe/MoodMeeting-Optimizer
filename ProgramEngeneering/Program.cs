using System.Diagnostics;
using TspSolver;

namespace ProgramEngeneering
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            int[,] dist =
            {
                {  0, 29, 82, 46, 68, 52, 72, 42, 51, 55, 29, 74 },
                { 29,  0, 55, 46, 42, 43, 43, 23, 23, 31, 41, 51 },
                { 82, 55,  0, 68, 46, 55, 23, 43, 41, 29, 79, 21 },
                { 46, 46, 68,  0, 82, 15, 72, 31, 51, 21, 51, 51 },
                { 68, 42, 46, 82,  0, 74, 23, 52, 21, 46, 82, 58 },
                { 52, 43, 55, 15, 74,  0, 61, 23, 55, 31, 33, 37 },
                { 72, 43, 23, 72, 23, 61,  0, 42, 23, 31, 77, 37 },
                { 42, 23, 43, 31, 52, 23, 42,  0, 33, 21, 37, 51 },
                { 51, 23, 41, 51, 21, 55, 23, 33,  0, 29, 62, 42 },
                { 55, 31, 29, 21, 46, 31, 31, 21, 29,  0, 51, 21 },
                { 29, 41, 79, 51, 82, 33, 77, 37, 62, 51,  0, 51 },
                { 74, 51, 21, 51, 58, 37, 37, 51, 42, 21, 51,  0 },
            };

            Console.WriteLine("=== Задача коммивояжёра: метод ветвей и границ ===");
            Console.WriteLine($"Городов: {dist.GetLength(0)}\n");

            var sw = Stopwatch.StartNew();
            var (path, cost) = BranchAndBoundTsp.Solve(dist);
            sw.Stop();

            Console.WriteLine($"Оптимальный маршрут: {string.Join(" -> ", path)}");
            Console.WriteLine($"Минимальная длина:   {cost}");
            Console.WriteLine($"Время работы:        {sw.ElapsedMilliseconds} мс");
        }
    }
}
