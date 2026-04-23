using System;

namespace Cupl.InvertibleFunctions
{
    public struct SimpleInvertibleFunc<T, TResult> : IInvertibleFunc<T, TResult>
    {
		public Func<T, TResult> Delegate { get; }
		public Func<TResult, T> InverseDelegate { get; }

		public SimpleInvertibleFunc<TResult, T> Inverse => new SimpleInvertibleFunc<TResult, T>(InverseDelegate, Delegate);

		IInvertibleFunc<TResult, T> IInvertibleFunc<T, TResult>.Inverse => Inverse;

		public SimpleInvertibleFunc(Func<T, TResult> delegate_, Func<TResult, T> inverseDelegate)
		{
			Delegate = delegate_;
			InverseDelegate = inverseDelegate;
		}
    }
}
