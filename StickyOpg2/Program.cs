
using global::System;
using global::System.Collections.Generic;

public class Program
{
    public static int x;
    public static int y;
    public static int z;
    public static int[,] map;
    
    public static void Main()
    {
        string[] input = Console.ReadLine().Split();
        x = int.Parse(input[0]);
        y = int.Parse(input[1]);
        z = int.Parse(input[2]);
        map = new int[x, y];
        
        for (int i = 0; i < y; i++)
        {
            string temp = Console.ReadLine();
            for (int j = 0; j < x; j++)
            {
                map[j, i] = int.Parse(temp[j].ToString());
            }
        }
        
        Point min = new Point(0, 0, map[0,0]);
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (map[j, i] < min.z) min = new Point(j, i, map[j, i]);
                if (min.z == 0) break;
            }
        }

        if (map[min.x, min.y] >= z)
        {
            Console.WriteLine(0);
            return;
        }

        HashSet<Point> toCheck = new HashSet<Point>();
        HashSet<Point> result = new HashSet<Point>();
        HashSet<Point> check = new HashSet<Point>();
        toCheck.Add(min);
        result = Algorithm(toCheck, result, check);
        
        long res = 0;
        foreach (Point p in result) res += (z - p.z);
        
        Console.WriteLine(res);
    }

    private static HashSet<Point> Algorithm(HashSet<Point> toCheck, HashSet<Point> result, HashSet<Point> check)
    {
        if (toCheck.Count == 0) return result;
        
        HashSet<Point> temp = new HashSet<Point>();
        
        foreach (Point p in toCheck)
        {
            check.Add(p);
            
            CheckMap(temp, check, p.x - 1, p.y); // left
            CheckMap(temp, check,p.x + 1, p.y); // right
            CheckMap(temp, check,p.x, p.y - 1); // up
            CheckMap(temp, check,p.x, p.y + 1); // down

            result.Add(p);
        }
        return Algorithm(temp, result, check);
    }

    private static void CheckMap(HashSet<Point> temp, HashSet<Point> check, int i, int j)
    {
        if (i < 0 || i >= x) return;
        if (j < 0 || j >= y) return;
        
        if(map[i, j] < z)
        {
            Point p = new Point(i, j, map[i, j]);
            if (!check.Contains(p))
            {
                temp.Add(p);
            }
        }
    }
} 

public struct Point
{
    public int x;
    public int y;
    public int z;

    public Point(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}