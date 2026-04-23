using System;

namespace Cupl.InvertibleFunctions
{
    public interface IInvertibleFunc<T, TResult>
    {
        public Func<T, TResult> Delegate { get; }
        public IInvertibleFunc<TResult, T> Inverse { get; }
    }
}
