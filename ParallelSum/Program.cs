using System.Diagnostics;
using System.Management;

namespace ParallelSum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] sizes = { 100000, 1000000, 10000000 };

            foreach (int size in sizes)
            {
                int[] array = new int[size];
                Random rand = new Random();

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rand.Next(1, 100);
                }

                // Обычное вычисление
                Stopwatch sw = Stopwatch.StartNew();
                long sequentialSum = SumSequential(array);
                sw.Stop();
                Console.WriteLine($"Sequential sum for {size} elements: {sequentialSum}, Time: {sw.ElapsedMilliseconds} ms");

                // Параллельное вычисление с Thread
                sw.Restart();
                long parallelSumWithThreads = SumParallelWithThreads(array);
                sw.Stop();
                Console.WriteLine($"Parallel sum with Threads for {size} elements: {parallelSumWithThreads}, Time: {sw.ElapsedMilliseconds} ms");

                // Параллельное вычисление с LINQ
                sw.Restart();
                long parallelSumWithLinq = SumParallelWithLinq(array);
                sw.Stop();
                Console.WriteLine($"Parallel sum with LINQ for {size} elements: {parallelSumWithLinq}, Time: {sw.ElapsedMilliseconds} ms");

                Console.WriteLine();
            }
        }
        public static long SumSequential(int[] array)
        {
            long sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        public static long SumParallelWithThreads(int[] array)
        {
            int numberOfThreads = Environment.ProcessorCount; // Количество потоков = количество ядер процессора
            int partSize = array.Length / numberOfThreads;
            long totalSum = 0;

            List<Thread> threads = new List<Thread>();
            long[] partialSums = new long[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                int start = i * partSize;
                int end = (i == numberOfThreads - 1) ? array.Length : start + partSize;

                int threadIndex = i;

                threads.Add(new Thread(() =>
                {
                    long sum = 0;
                    for (int j = start; j < end; j++)
                    {
                        sum += array[j];
                    }
                    partialSums[threadIndex] = sum;
                }));
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                totalSum += partialSums[i];
            }

            return totalSum;
        }

        public static long SumParallelWithLinq(int[] array)
        {
            return array.AsParallel().Sum(i => (long)i);
        }
    }
}
