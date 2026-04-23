using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Cupl.InvertibleFunctions
{
    public static class InvertibleFunc
    {
		[Invertible(nameof(InverseInvoke))]
        public static TResult Invoke<T, TResult>(this IInvertibleFunc<T, TResult> function, T arg)
		{
			return function.Delegate(arg);
		}

		[Invertible(nameof(Invoke))]
		public static T InverseInvoke<T, TResult>(this IInvertibleFunc<T, TResult> function, TResult inverseArg)
		{
			return function.Inverse.Invoke(inverseArg);
		}

		private static MethodInfo MakeGenericMethodIfGeneric(this MethodInfo method, params Type[] typeArguments)
		{
			if (method.IsGenericMethodDefinition)
				return method.MakeGenericMethod(typeArguments);
			else if (typeArguments.Length != 0)
				throw new ArgumentException("Non-generic methods cannot have generic arguments.");
			else
				return method;
		}

		private static MethodInfo GetInverseMethod(MethodInfo method, bool hasTarget, string inverseMethodName)
		{
			if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
				return GetInverseMethod(method.GetGenericMethodDefinition(), hasTarget, inverseMethodName).MakeGenericMethod(method.GetGenericArguments());
			else
			{
				BindingFlags bindingFlags =
					(method.IsStatic ? BindingFlags.Static : BindingFlags.Instance) |
					(method.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic) |
					BindingFlags.FlattenHierarchy;

				var inverseMethodParameterTypes =
					method.IsStatic && hasTarget
						? new Type[] { method.GetParameters()[0].ParameterType, method.ReturnType }
						: new Type[] { method.ReturnType };

				var methodGenericArguments = method.GetGenericArguments();
				var inverseMethod =
					method.DeclaringType.GetMethods(bindingFlags)
						.Where(m => m.Name == inverseMethodName)
						.Where(m => m.GetGenericArguments().Length == methodGenericArguments.Length)
						.Where(m => m.MakeGenericMethodIfGeneric(methodGenericArguments).GetParameters().Select(p => p.ParameterType).SequenceEqual(inverseMethodParameterTypes))
						.SingleOrDefault();

				if (inverseMethod == null)
					throw new InvalidOperationException($"Could not find inverse method {inverseMethodName} for {method}.");
				else if (inverseMethod.GetCustomAttribute<InvertibleAttribute>() is InvertibleAttribute inverseMethodAttr)
				{
					if (inverseMethodAttr?.InverseMethodName != method.Name)
						throw new InvalidOperationException($"Inverse method names in [Invertible] attributes must match each other's names.");
				}
				else
					throw new InvalidOperationException($"Inverse method must also have a [Invertible] attribute.");

				return inverseMethod;
			}
		}

		public static bool TryGetInvertible<T, TResult>(this Func<T, TResult> function, [NotNullWhen(true)] out IInvertibleFunc<T, TResult>? invertible)
		{
			var method = function.Method;

			if (method.GetCustomAttribute<InvertibleAttribute>() is InvertibleAttribute attr)
			{
				var inverseMethod = GetInverseMethod(method, function.Target != null, attr.InverseMethodName);

				Delegate inverseDelegate =
					(function.Target != null)
						? inverseMethod.CreateDelegate(typeof(Func<TResult, T>), function.Target)
						: inverseMethod.CreateDelegate(typeof(Func<TResult, T>));
				
				invertible = new SimpleInvertibleFunc<T, TResult>(function, (Func<TResult, T>)inverseDelegate);
				return true;
			}
			else
			{
				invertible = null;
				return false;
			}
		}
    }
}
