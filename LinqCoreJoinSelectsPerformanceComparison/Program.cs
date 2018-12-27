using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqCoreJoinSelectsPerformanceComparison
{
    class Program
    {
        private const int CELL1_SIZE = 50;
        private const int CELL2_SIZE = 20;
        private static readonly Random Rng = new Random();

        // helper method
        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Main(string[] args)
        {
            const int n = 500000;

            // 1st Collection: Main dto objects list
            List<SomeBigDTO> bigList = new List<SomeBigDTO>();
            for (int i = 0; i < n; i++)
            {
                bigList.Add(new SomeBigDTO() { Id = i, Field1 = "field1_" + i, Field2 = "field2_" + i, Field3 = "field3_" + i, Field4 = i, Field5 = DateTime.Now, Field6 = DateTime.Now });
            }
            Program.Shuffle(bigList);

            // 2nd Collection: array of Id, that we want to work with (15 times smaller that big list)
            int[] targetIds = new int[n / 15];
            for (int i = 0; i < n / 15; i++)
            {
                targetIds[i] = i * 15;
            }
            Program.Shuffle(targetIds);

            var watch = new System.Diagnostics.Stopwatch();


            watch.Start();
            IEnumerable<SomeBigDTO> results = bigList.Join(targetIds, mdb => mdb.Id, p => p,
                (x, y) => x).ToList();
            watch.Stop();
            Console.WriteLine($"{"Join() Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results.Count()}");

        

            watch.Restart();
            HashSet<int> priceHash = new HashSet<int>(targetIds);
            IEnumerable<SomeBigDTO> resultsHash2 = bigList.Where(p => priceHash.Contains(p.Id)).ToList();
            watch.Stop();
            Console.WriteLine($"{"Hashset Execution Time:",CELL1_SIZE}" + $"{ watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {resultsHash2.Count()}");

            watch.Restart();
            var priceDict = bigList.ToDictionary(l => l.Id, l => l);
            IEnumerable<SomeBigDTO> resultsHash = targetIds.Select(p => priceDict[p]).ToList();
            watch.Stop();
            Console.WriteLine($"{"Dictionary Execution Time:",CELL1_SIZE}" + $"{ watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {resultsHash.Count()}");

            watch.Start();
            var results12 = bigList.Select(l => l.Id).Intersect(targetIds).ToList();
            watch.Stop();
            Console.WriteLine($"{"Intersect() Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results12.Count()}");

            watch.Restart();
            IEnumerable<SomeBigDTO> results16 = bigList.AsParallel().Where(x => Array.IndexOf(targetIds, x.Id) >= 0).ToList();
            watch.Stop();
            Console.WriteLine($"{"Where(IndexOf).AsParallel() Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results16.Count()}");
            
            watch.Restart();
            IEnumerable<SomeBigDTO> results9 = bigList.AsParallel().Where(x => targetIds.Contains(x.Id)).ToList();
            watch.Stop();
            Console.WriteLine($"{"Where(Contains).AsParallel() Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results9.Count()}");
            
            watch.Restart();
            IEnumerable<SomeBigDTO> results4 = bigList.FindAll(x => targetIds.Contains(x.Id)).ToList();
            watch.Stop();
            Console.WriteLine($"{"FindAll(Contains) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results4.Count()}");

            watch.Restart();
            IEnumerable<SomeBigDTO> results6 = bigList.Where(x => Array.IndexOf(targetIds, x.Id) >= 0).ToList();
            watch.Stop();
            Console.WriteLine($"{"Where(IndexOf) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results6.Count()}");

            watch.Restart();
            IEnumerable<SomeBigDTO> results2 = bigList.Where(x => targetIds.Contains(x.Id)).ToList();
            watch.Stop();
            Console.WriteLine($"{"Where(Contains) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results2.Count()}");

            watch.Restart();
            IEnumerable<SomeBigDTO> results5 = bigList.Where(x => targetIds.Count(i => i == x.Id) > 0).ToList();
            watch.Stop();
            Console.WriteLine($"{"Where(Count) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results5.Count()}");

            IEnumerable<SomeBigDTO> results3 = bigList.Where(x => targetIds.Any(y => x.Id == y)).ToList();
            watch.Stop();
            Console.WriteLine($"{"Where(Any) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results3.Count()}");

            watch.Restart();
            IEnumerable<SomeBigDTO> results7 = targetIds.Select(p => bigList.Find(item => item.Id == p)).ToList();
            watch.Stop();
            Console.WriteLine($"{"Select(Find) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results7.Count()}");
            
            watch.Restart();
            IEnumerable<SomeBigDTO> results8 = targetIds.Select(p => bigList.First(item => item.Id == p)).ToList();
            watch.Stop();
            Console.WriteLine($"{"Select(First) Execution Time:",CELL1_SIZE}" + $"{watch.ElapsedMilliseconds,CELL2_SIZE} ms, list size {results8.Count()}");

            watch.Restart();


            Console.ReadLine();
        }
    }
}
