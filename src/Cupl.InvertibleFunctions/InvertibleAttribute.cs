using System;

namespace Cupl.InvertibleFunctions
{
	[AttributeUsage(AttributeTargets.Method)]
    public sealed class InvertibleAttribute : Attribute
    {
        public string InverseMethodName { get; }

		public InvertibleAttribute(string inverseMethod)
		{
			InverseMethodName = inverseMethod;
		}
    }
}
