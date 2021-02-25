``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.103
  [Host]    : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  RyuJitX64 : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT

Job=RyuJitX64  Jit=RyuJit  Platform=X64  

```
|                                            Method |      Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------------------------------- |----------:|----------:|----------:|-------:|------:|------:|----------:|
| Multiple_Iteration_FindingItem_ShuffledArray_LINQ |  9.863 μs | 0.0865 μs | 0.0675 μs | 0.0305 |     - |     - |     208 B |
|   Multiple_Iteration_FindingItem_SortedArray_LINQ | 10.309 μs | 0.0669 μs | 0.0593 μs | 0.0305 |     - |     - |     208 B |
|  Multiple_Iteration_FindingItem_ShuffledArray_For |  2.155 μs | 0.0199 μs | 0.0166 μs |      - |     - |     - |         - |
|    Multiple_Iteration_FindingItem_SortedArray_For |  2.077 μs | 0.0112 μs | 0.0104 μs |      - |     - |     - |         - |
