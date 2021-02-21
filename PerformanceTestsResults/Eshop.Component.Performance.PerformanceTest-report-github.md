``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.103
  [Host]    : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  RyuJitX64 : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT

Job=RyuJitX64  Jit=RyuJit  Platform=X64  

```
|                      Method |      Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|---------------------------- |----------:|----------:|----------:|-------:|-------:|------:|----------:|
| GetCategory_ExceptionThrown | 66.344 μs | 0.6818 μs | 0.6044 μs | 1.8311 | 0.4883 |     - |  11.63 KB |
|    GetCategory_ResultReturn |  2.356 μs | 0.0306 μs | 0.0286 μs | 0.3624 | 0.0916 |     - |   2.23 KB |
