# ParallelSum
A project was implemented where the performance of three methods for calculating the sum of an integer array was compared: sequentially, using threads, and using parallel LINQ.

## System Environment

| Parameter                       | Value                                                 |
|---------------------------------|-------------------------------------------------------|
| **Processor**                   | Intel(R) Core(TM) i7-10510U CPU @ 1.80GHz             |
| **Total Physical Memory (RAM)** | 16 GB                                                 |
| **Disk**                        | NVMe INTEL SSDPEKNW010T8                              |
| **Operating System**            | Windows 11, Version 10.0.22631, 64-bit                |

## Results

### Execution Time Comparison

| Array Size  | Sequential (ms) | Parallel with Threads (ms) | Parallel with LINQ (ms) |
|-------------|-----------------|----------------------------|-------------------------|
| 100,000     | 17              | 204                        | 62                      |
| 1,000,000   | 2               | 72                         | 15                      |
| 10,000,000  | 26              | 143                        | 46                      |
