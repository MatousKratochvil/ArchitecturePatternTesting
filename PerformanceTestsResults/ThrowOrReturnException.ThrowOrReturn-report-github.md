``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-10750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.103
  [Host]    : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT
  RyuJitX64 : .NET Core 5.0.3 (CoreCLR 5.0.321.7212, CoreFX 5.0.321.7212), X64 RyuJIT

Job=RyuJitX64  Jit=RyuJit  Platform=X64  

```
|                                      Method |        Mean |     Error |    StdDev |      Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------------------------- |------------:|----------:|----------:|------------:|-------:|------:|------:|----------:|
|                         Exception_Is_Thrown | 5,320.76 ns | 24.698 ns | 23.103 ns | 5,331.28 ns | 0.0458 |     - |     - |     320 B |
|       Exception_Is_Returned_In_WrappedClass |    24.02 ns |  0.499 ns |  0.937 ns |    23.61 ns | 0.0306 |     - |     - |     192 B |
| Exception_Is_Catched_In_LanguageExtTryClass | 6,361.52 ns | 84.020 ns | 78.593 ns | 6,339.88 ns | 0.0992 |     - |     - |     664 B |
