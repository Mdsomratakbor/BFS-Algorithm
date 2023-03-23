using System;
using System.Collections.Generic;

class Robot
{
    static int[] dx = { -1, 0, 1, 0 }; // relative x coordinates of neighbors
    static int[] dy = { 0, 1, 0, -1 }; // relative y coordinates of neighbors

    static bool IsValid(int[,] grid, int x, int y)
    {
        // check if x, y are valid coordinates within the grid
        int n = grid.GetLength(0), m = grid.GetLength(1);
        return (x >= 0 && x < n && y >= 0 && y < m);
    }

    static List<int[]> FindPath(int[,] grid, int[] start, int[] end)
    {
        int n = grid.GetLength(0), m = grid.GetLength(1);
        bool[,] visited = new bool[n, m];
        int[,] parent = new int[n, m];
        Queue<int[]> q = new Queue<int[]>();

        q.Enqueue(start);
        visited[start[0], start[1]] = true;
        parent[start[0], start[1]] = -1;

        while (q.Count > 0)
        {
            int[] curr = q.Dequeue();
            int x = curr[0], y = curr[1];
            if (x == end[0] && y == end[1])
            {
                break; // found the end point
            }
            for (int i = 0; i < 4; i++)
            { // explore neighbors
                int nx = x + dx[i], ny = y + dy[i];
                if (IsValid(grid, nx, ny) && !visited[nx, ny] && grid[nx, ny] == 0)
                {
                    visited[nx, ny] = true;
                    parent[nx, ny] = x * m + y; // encode parent as a single integer
                    q.Enqueue(new int[] { nx, ny });
                }
            }
        }

        List<int[]> path = new List<int[]>();
        int currX = end[0], currY = end[1];
        while (currX != -1 && currY != -1)
        {
            path.Add(new int[] { currX, currY });
            int prev = parent[currX, currY];
            if (prev != -1)
            {
                currX = prev / m;
                currY = prev % m;
            }
            else
            {
                currX = -1;
                currY = -1;
            }
        }
        path.Reverse(); // reverse the path to get start to end order
        return path;
    }

    static void Main()
    {
        int[,] grid = new int[,] {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        };
        int[] start = { 0, 0 };
        int[] end = { 4, 4 };
        List<int[]> path = FindPath(grid, start, end);
        Console.WriteLine("Path:");
        foreach (int[] p in path)
        {
            Console.WriteLine("(" + p[0] + ", " + p[1] + ")");
        }
    }
}
