namespace Cupl.InvertibleFunctions.Tests;

using System;
using Cupl.InvertibleFunctions;

public static class SimpleMathStatic
{
    [Invertible(nameof(Sub1))]
    public static int Add1(int i)
    {
        return i + 1;
    }

    [Invertible(nameof(Add1))]
    public static int Sub1(int i)
    {
        return i - 1;
    }

    public static int Sign(int i)
    {
        return Math.Sign(i);
    }
}

public class SimpleMathInstance
{
    [Invertible(nameof(Sub1))]
    public int Add1(int i)
    {
        return i + 1;
    }

    [Invertible(nameof(Add1))]
    public int Sub1(int i)
    {
        return i - 1;
    }

    public int Sign(int i)
    {
        return Math.Sign(i);
    }
}

public class InvertibleFunc_Should
{
    [SetUp]
    public void Setup()
    {
    }

    private void AssertNormal<T, TResult>(IInvertibleFunc<T, TResult> function, T arg, TResult expectedResult)
    {
        Assert.That(function.Invoke(arg), Is.EqualTo(expectedResult));
    }

    private void AssertInverse<T, TResult>(IInvertibleFunc<T, TResult> function, T arg)
    {
        Assert.That(function.Inverse.Invoke(function.Invoke(arg)), Is.EqualTo(arg));
    }

    [Test]
    public void TestInvertibleAttribute()
    {
        Assert.That(InvertibleFunc.TryGetInvertible(SimpleMathStatic.Add1, out IInvertibleFunc<int, int>? add1));
        Assert.That(InvertibleFunc.TryGetInvertible(SimpleMathStatic.Sub1, out IInvertibleFunc<int, int>? sub1));
        Assert.That(!InvertibleFunc.TryGetInvertible(SimpleMathStatic.Sign, out IInvertibleFunc<int, int>? _));

        AssertNormal(add1!, 42, 43);
        AssertInverse(add1!, 42);
        AssertNormal(sub1!, 42, 41);
        AssertInverse(sub1!, 42);

        var simpleMathInstance = new SimpleMathInstance();

        Assert.That(InvertibleFunc.TryGetInvertible(simpleMathInstance.Add1, out IInvertibleFunc<int, int>? add1Instance));
        Assert.That(InvertibleFunc.TryGetInvertible(simpleMathInstance.Sub1, out IInvertibleFunc<int, int>? sub1Instance));
        Assert.That(!InvertibleFunc.TryGetInvertible(simpleMathInstance.Sign, out IInvertibleFunc<int, int>? _));

        AssertNormal(add1Instance!, 42, 43);
        AssertInverse(add1Instance!, 42);
        AssertNormal(sub1Instance!, 42, 41);
        AssertInverse(sub1Instance!, 42);
    }

    [Test]
    public void TestInvertibleInvoke()
    {
        Assert.That(InvertibleFunc.TryGetInvertible(SimpleMathStatic.Add1, out IInvertibleFunc<int, int>? add1));
        Assert.That(InvertibleFunc.TryGetInvertible(add1!.Invoke, out IInvertibleFunc<int, int>? add1Invoke));
        Assert.That(InvertibleFunc.TryGetInvertible(add1!.InverseInvoke, out IInvertibleFunc<int, int>? add1InverseInvoke));

        AssertNormal(add1Invoke!, 42, 43);
        AssertInverse(add1Invoke!, 42);
        AssertNormal(add1InverseInvoke!, 42, 41);
        AssertInverse(add1InverseInvoke!, 42);
    }
}
