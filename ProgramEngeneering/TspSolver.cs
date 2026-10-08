namespace TspSolver;

public static class BranchAndBoundTsp
{
    // Оптимальное решение задачи коммивояжёра методом ветвей и границ.
    // Нижняя граница: текущая стоимость + для каждого непосещённого города
    // минимальное входящее ребро + для текущего города минимальное исходящее
    // ребро до непосещённого (или возврат в 0).
    public static (int[] Path, long Cost) Solve(int[,] dist)
    {
        int n = dist.GetLength(0);

        long bestCost = long.MaxValue;
        int[]? bestPath = null;
        long[] minIncoming = Array.Empty<long>();

        // Поиск в глубину по дереву частичных маршрутов
        // с отсечением бесперспективных ветвей.
        void Dfs(int current, long currentCost, List<int> currentPath, bool[] visited)
        {
            if (currentCost + LowerBound(current, visited) >= bestCost)
                return;

            if (currentPath.Count == n)
            {
                long total = currentCost + dist[current, 0];
                if (total < bestCost)
                {
                    bestCost = total;
                    bestPath = currentPath.Append(0).ToArray();
                }
                return;
            }

            for (int next = 0; next < n; next++)
            {
                if (visited[next]) continue;

                long newCost = currentCost + dist[current, next];


                if (newCost + LowerBound(next, visited, next) >= bestCost)
                    continue;

                visited[next] = true;
                currentPath.Add(next);

                Dfs(next, newCost, currentPath, visited);

                currentPath.RemoveAt(currentPath.Count - 1);
                visited[next] = false;
            }
        }

        // Нижняя граница: min входящие рёбра для непосещённых
        // + min исходящее ребро из current.
        long LowerBound(int current, bool[] visited, int? aboutToVisit = null)
        {
            long bound = 0;

            for (int j = 0; j < n; j++)
            {
                if (visited[j]) continue;
                if (aboutToVisit.HasValue && j == aboutToVisit.Value) continue;
                bound += minIncoming[j];
            }

            long minOut = long.MaxValue;
            for (int j = 0; j < n; j++)
            {
                if (j == current) continue;
                if (visited[j]) continue;
                if (dist[current, j] < minOut) minOut = dist[current, j];
            }

            if (minOut == long.MaxValue)
                minOut = dist[current, 0];

            bound += minOut;
            return bound;
        }


        if (n == 0) return (Array.Empty<int>(), 0);
        if (n == 1) return (new[] { 0 }, 0);

        minIncoming = new long[n];
        for (int j = 0; j < n; j++)
        {
            long best = long.MaxValue;
            for (int i = 0; i < n; i++)
            {
                if (i == j) continue;
                if (dist[i, j] < best) best = dist[i, j];
            }
            minIncoming[j] = best;
        }

        var path = new List<int> { 0 };
        var visited = new bool[n];
        visited[0] = true;

        Dfs(0, 0, path, visited);
        return (bestPath ?? new[] { 0 }, bestCost);
    }
}