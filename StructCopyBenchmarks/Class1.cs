using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace StructCopyBenchmarks;

using T = System.String;

public readonly ref struct S8
{
    public T O1 { get; init; }
}

public readonly ref struct S16
{
    public T O1 { get; init; }
    public T O2 { get; init; }
}

public readonly ref struct S24
{
    public T O1 { get; init; }
    public T O2 { get; init; }
    public T O3 { get; init; }
}

public readonly ref struct S32
{
    public T O1 { get; init; }
    public T O2 { get; init; }
    public T O3 { get; init; }
    public T O4 { get; init; }
}

public readonly ref struct S40
{
    public T O1 { get; init; }
    public T O2 { get; init; }
    public T O3 { get; init; }
    public T O4 { get; init; }
    public T O5 { get; init; }
}

public readonly ref struct S48
{
    public T O1 { get; init; }
    public T O2 { get; init; }
    public T O3 { get; init; }
    public T O4 { get; init; }
    public T O5 { get; init; }
    public T O6 { get; init; }
}

public readonly ref struct S64
{
    public T O1 { get; init; }
    public T O2 { get; init; }
    public T O3 { get; init; }
    public T O4 { get; init; }
    public T O5 { get; init; }
    public T O6 { get; init; }
    public T O7 { get; init; }
    public T O8 { get; init; }
}

public readonly ref struct S128
{
    public readonly S64 A;
    public readonly S64 B;

    public S128(S64 a, S64 b)
    {
        A = a;
        B = b;
    }
}

public readonly ref struct S256
{
    public readonly S128 A;
    public readonly S128 B;

    public S256(S128 a, S128 b)
    {
        A = a;
        B = b;
    }
}

// Same structs, but mutable, and without the ref modifier
public struct S8M
{
    public T O1 { get; set; }
}

public struct S16M
{
    public T O1 { get; set; }
    public T O2 { get; set; }
}

public struct S24M
{
    public T O1 { get; set; }
    public T O2 { get; set; }
    public T O3 { get; set; }
}

public struct S32M
{
    public T O1 { get; set; }
    public T O2 { get; set; }
    public T O3 { get; set; }
    public T O4 { get; set; }
}

public struct S40M
{
    public T O1 { get; set; }
    public T O2 { get; set; }
    public T O3 { get; set; }
    public T O4 { get; set; }
    public T O5 { get; set; }
}

public struct S48M
{
    public T O1 { get; set; }
    public T O2 { get; set; }
    public T O3 { get; set; }
    public T O4 { get; set; }
    public T O5 { get; set; }
    public T O6 { get; set; }
}

public struct S64M
{
    public T O1 { get; set; }
    public T O2 { get; set; }
    public T O3 { get; set; }
    public T O4 { get; set; }
    public T O5 { get; set; }
    public T O6 { get; set; }
    public T O7 { get; set; }
    public T O8 { get; set; }
}

public struct S128M
{
    public S64M A;
    public S64M B;

    public S128M(S64M a, S64M b)
    {
        A = a;
        B = b;
    }
}

public struct S256M
{
    public S128M A;
    public S128M B;

    public S256M(S128M a, S128M b)
    {
        A = a;
        B = b;
    }
}

// 
public static class Operations
{
    // Fake operations that do some work with the struct.
    // For every struct, we have 2 variations: one that takes the struct by value, and one that takes it by ref.
    // The operations will count the number of characters in the struct's fields, and return the sum.
    // The operations are static, so that we can use them in the benchmarking code.
    // All operations must not be inlined, so that we can measure the cost of copying the struct.
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8(S8 s)
    {
        return s.O1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8(ref S8 s)
    {
        return s.O1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16(S16 s)
    {
        return s.O1.Length + s.O2.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16(ref S16 s)
    {
        return s.O1.Length + s.O2.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op24(S24 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op24(ref S24 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32(S32 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32(ref S32 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op40(S40 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op40(ref S40 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op48(S48 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op48(ref S48 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64(S64 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64(ref S64 s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128(S128 a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            sum = s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        {
            ref readonly var s = ref a.B;
            sum += s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128(ref S128 a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            sum = s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        {
            ref readonly var s = ref a.B;
            sum += s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256(S256 a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            {
                ref readonly var s2 = ref s.A;
                sum = s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        {
            ref readonly var s = ref a.B;
            {
                ref readonly var s2 = ref s.A;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256(ref S256 a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            {
                ref readonly var s2 = ref s.A;
                sum = s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        {
            ref readonly var s = ref a.B;
            {
                ref readonly var s2 = ref s.A;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8(S8M s)
    {
        return s.O1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8(ref S8M s)
    {
        return s.O1.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16(S16M s)
    {
        return s.O1.Length + s.O2.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16(ref S16M s)
    {
        return s.O1.Length + s.O2.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op24(S24M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op24(ref S24M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32(S32M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32(ref S32M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op40(S40M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op40(ref S40M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op48(S48M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op48(ref S48M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64(S64M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64(ref S64M s)
    {
        return s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128(S128M a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            sum = s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        {
            ref readonly var s = ref a.B;
            sum += s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128(ref S128M a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            sum = s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        {
            ref readonly var s = ref a.B;
            sum += s.O1.Length + s.O2.Length + s.O3.Length + s.O4.Length + s.O5.Length + s.O6.Length + s.O7.Length + s.O8.Length;
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256(S256M a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            {
                ref readonly var s2 = ref s.A;
                sum = s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        {
            ref readonly var s = ref a.B;
            {
                ref readonly var s2 = ref s.A;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256(ref S256M a)
    {
        int sum = 0;
        {
            ref readonly var s = ref a.A;
            {
                ref readonly var s2 = ref s.A;
                sum = s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        {
            ref readonly var s = ref a.B;
            {
                ref readonly var s2 = ref s.A;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
            {
                ref readonly var s2 = ref s.B;
                sum += s2.O1.Length + s2.O2.Length + s2.O3.Length + s2.O4.Length + s2.O5.Length + s2.O6.Length + s2.O7.Length + s2.O8.Length;
            }
        }
        return sum;
    }

    // For reference, also provide non-inlined methods that do nothing
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8_NoOp(ref S8 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8_NoOp(S8 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16_NoOp(ref S16 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16_NoOp(S16 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32_NoOp(ref S32 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32_NoOp(S32 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64_NoOp(ref S64 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64_NoOp(S64 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128_NoOp(ref S128 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128_NoOp(S128 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256_NoOp(ref S256 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256_NoOp(S256 s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8_NoOp(ref S8M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op8_NoOp(S8M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16_NoOp(ref S16M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op16_NoOp(S16M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32_NoOp(ref S32M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op32_NoOp(S32M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64_NoOp(ref S64M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op64_NoOp(S64M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128_NoOp(ref S128M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op128_NoOp(S128M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256_NoOp(ref S256M s)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Op256_NoOp(S256M s)
    {
        return 0;
    }
}

// Benchmarks for all of the methods above
[IterationTime(25)]
public class Benchmarks
{
    [Params(1, 10, 100, 1000)] public int N { get; set; }

    [Benchmark]
    public int S8_Ref()
    {
        var s = new S8
        {
            O1 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op8(ref s);
        return sum;
    }

    [Benchmark]
    public int S8_Copy()
    {
        var s = new S8
        {
            O1 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op8(s);
        return sum;
    }

    [Benchmark]
    public int S16_Ref()
    {
        var s = new S16
        {
            O1 = "Hello",
            O2 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op16(ref s);
        return sum;
    }

    [Benchmark]
    public int S16_Copy()
    {
        var s = new S16
        {
            O1 = "Hello",
            O2 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op16(s);
        return sum;
    }

    [Benchmark]
    public int S32_Ref()
    {
        var s = new S32
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op32(ref s);
        return sum;
    }

    [Benchmark]
    public int S32_Copy()
    {
        var s = new S32
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op32(s);
        return sum;
    }

    [Benchmark]
    public int S64_Ref()
    {
        var s = new S64
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
            O5 = "Hello",
            O6 = "Hello",
            O7 = "Hello",
            O8 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op64(ref s);
        return sum;
    }

    [Benchmark]
    public int S64_Copy()
    {
        var s = new S64
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
            O5 = "Hello",
            O6 = "Hello",
            O7 = "Hello",
            O8 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op64(s);
        return sum;
    }

    [Benchmark]
    public int S128_Ref()
    {
        var s = new S128
        (
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            },
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            }
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op128(ref s);
        return sum;
    }

    [Benchmark]
    public int S128_Copy()
    {
        var s = new S128
        (
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            },
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            }
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op128(s);
        return sum;
    }

    [Benchmark]
    public int S256_Ref()
    {
        var s = new S256(
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            ),
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            )
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op256(ref s);
        return sum;
    }

    [Benchmark]
    public int S256_Copy()
    {
        var s = new S256(
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            ),
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            )
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op256(s);
        return sum;
    }

    [Benchmark]
    public int S8M_Ref()
    {
        var s = new S8M
        {
            O1 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op8(ref s);
        return sum;
    }

    [Benchmark]
    public int S8M_Copy()
    {
        var s = new S8M
        {
            O1 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op8(s);
        return sum;
    }

    [Benchmark]
    public int S16M_Ref()
    {
        var s = new S16M
        {
            O1 = "Hello",
            O2 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op16(ref s);
        return sum;
    }

    [Benchmark]
    public int S16M_Copy()
    {
        var s = new S16M
        {
            O1 = "Hello",
            O2 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op16(s);
        return sum;
    }

    [Benchmark]
    public int S32M_Ref()
    {
        var s = new S32M
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op32(ref s);
        return sum;
    }

    [Benchmark]
    public int S32M_Copy()
    {
        var s = new S32M
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op32(s);
        return sum;
    }

    [Benchmark]
    public int S64M_Ref()
    {
        var s = new S64M
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
            O5 = "Hello",
            O6 = "Hello",
            O7 = "Hello",
            O8 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op64(ref s);
        return sum;
    }

    [Benchmark]
    public int S64M_Copy()
    {
        var s = new S64M
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
            O5 = "Hello",
            O6 = "Hello",
            O7 = "Hello",
            O8 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op64(s);
        return sum;
    }

    [Benchmark]
    public int S128M_Ref()
    {
        var s = new S128M
        (
            new S64M
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            },
            new S64M
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            }
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op128(ref s);
        return sum;
    }

    [Benchmark]
    public int S128M_Copy()
    {
        var s = new S128M
        (
            new S64M
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            },
            new S64M
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            }
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op128(s);
        return sum;
    }

    [Benchmark]
    public int S256M_Ref()
    {
        var s = new S256M(
            new S128M(
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            ),
            new S128M(
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            )
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op256(ref s);
        return sum;
    }

    [Benchmark]
    public int S256M_Copy()
    {
        var s = new S256M(
            new S128M(
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            ),
            new S128M(
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64M
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            )
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op256(s);
        return sum;
    }

    [Benchmark]
    public int S8_NoOp_Ref()
    {
        var s = new S8
        {
            O1 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op8_NoOp(ref s);
        return sum;
    }

    [Benchmark]
    public int S8_NoOp_Copy()
    {
        var s = new S8
        {
            O1 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op8_NoOp(s);
        return sum;
    }

    [Benchmark]
    public int S16_NoOp_Ref()
    {
        var s = new S16
        {
            O1 = "Hello",
            O2 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
        sum += Operations.Op16_NoOp(ref s);
        return sum;
    }

    [Benchmark]
    public int S16_NoOp_Copy()
    {
        var s = new S16
        {
            O1 = "Hello",
            O2 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op16_NoOp(s);
        return sum;
    }

    [Benchmark]
    public int S32_NoOp_Ref()
    {
        var s = new S32
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op32_NoOp(ref s);
        return sum;
    }

    [Benchmark]
    public int S32_NoOp_Copy()
    {
        var s = new S32
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op32_NoOp(s);
        return sum;
    }

    [Benchmark]
    public int S64_NoOp_Ref()
    {
        var s = new S64
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
            O5 = "Hello",
            O6 = "Hello",
            O7 = "Hello",
            O8 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op64_NoOp(ref s);
        return sum;
    }

    [Benchmark]
    public int S64_NoOp_Copy()
    {
        var s = new S64
        {
            O1 = "Hello",
            O2 = "Hello",
            O3 = "Hello",
            O4 = "Hello",
            O5 = "Hello",
            O6 = "Hello",
            O7 = "Hello",
            O8 = "Hello",
        };
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op64_NoOp(s);
        return sum;
    }

    [Benchmark]
    public int S128_NoOp_Ref()
    {
        var s = new S128
        (
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            },
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            }
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op128_NoOp(ref s);
        return sum;
    }

    [Benchmark]
    public int S128_NoOp_Copy()
    {
        var s = new S128
        (
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            },
            new S64
            {
                O1 = "Hello",
                O2 = "Hello",
                O3 = "Hello",
                O4 = "Hello",
                O5 = "Hello",
                O6 = "Hello",
                O7 = "Hello",
                O8 = "Hello",
            }
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op128_NoOp(s);
        return sum;
    }

    [Benchmark]
    public int S256_NoOp_Ref()
    {
        var s = new S256(
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            ),
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            )
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op256_NoOp(ref s);
        return sum;
    }

    [Benchmark]
    public int S256_NoOp_Copy()
    {
        var s = new S256(
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            ),
            new S128(
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                },
                new S64
                {
                    O1 = "Hello",
                    O2 = "Hello",
                    O3 = "Hello",
                    O4 = "Hello",
                    O5 = "Hello",
                    O6 = "Hello",
                    O7 = "Hello",
                    O8 = "Hello",
                }
            )
        );
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += Operations.Op256_NoOp(s);
        return sum;
    }
}