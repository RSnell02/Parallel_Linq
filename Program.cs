/*
 * Project: ParallelLINQ
 * Filename: Program.cs
 * Author: R. Snell
 * Date: Jan. 2, 2021
 * Purpose: To demonstrate how LINQ can use multiple cores
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            //  Create an array of random ints in the range of 1-999
            int[] values = Enumerable.Range(1, 10000000)
                            .Select(x => random.Next(1, 1000))
                            .ToArray();

            // Time the Min, Max, and Average LINQ extension methods
            Console.WriteLine("Min, Max, and Average with LINQ to Objects " +
               "using a single core");
            var linqStart = DateTime.Now;
            var linqMin = values.Min();
            var linqMax = values.Max();
            var linqAverage = values.Average();
            var linqEnd = DateTime.Now;

            // Display results and total time in milliseconds
            var linqTime = linqEnd.Subtract(linqStart).TotalMilliseconds;
            DisplayResults(linqMin, linqMax, linqAverage, linqTime);

            // Tiem the Min, Max, Average PLINQ extension methods
            Console.WriteLine("\nMin, Max, Average with PLINQ using multiple cores");
            var plinqStart = DateTime.Now;
            var plinqMin = values.AsParallel().Min();
            var plinqMax = values.AsParallel().Max();
            var plinqAverage = values.AsParallel().Average();
            var plinqEnd = DateTime.Now;

            // Display the results and total time in milliseconds
            var plinqTime = plinqEnd.Subtract(plinqStart).TotalMilliseconds;
            DisplayResults(plinqMin, plinqMax, plinqAverage, plinqTime);

            // Display time difference as percentage
            Console.WriteLine("\nPLINQ took " +
                        $"{((linqTime - plinqTime) / linqTime):P0}" +
                        " less time than LINQ");
        }   // end Main()

        // Display results and total time in ms.
        static void DisplayResults(int min, int max, double average, double time)
        {
            Console.WriteLine($"Min: {min}\nMax: {max}\n" +
                $"Average: {average:F}\nTotal time in milliseconds: {time:F}");
        }   // end DisplayResults
    }   // end class
}   // end namespace
