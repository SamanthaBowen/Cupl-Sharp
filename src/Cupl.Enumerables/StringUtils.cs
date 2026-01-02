using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Cupl.Enumerables
{
	public static class StringUtils
	{
		public static int? IndexOfOrNull(this string str, char value)
		{
			var index = str.IndexOf(value);
			return (0 <= index) ? index : (int?)null;
		}

		public static int? LastIndexOfOrNull(this string str, char value)
		{
			var index = str.LastIndexOf(value);
			return (0 <= index) ? index : (int?)null;
		}

		public static string RemovePrefix(this string str, string prefix)
		{
			return str.StartsWith(prefix) ? str[prefix.Length..] : str;
		}

		public static string? RemovePrefixOrNull(this string str, string prefix)
		{
			return str.StartsWith(prefix) ? str[prefix.Length..] : null;
		}

		public static string RemoveSuffix(this string str, string suffix)
		{
			return str.EndsWith(suffix) ? str[..^suffix.Length] : str;
		}

		public static string? RemoveSuffixOrNull(this string str, string suffix)
		{
			return str.EndsWith(suffix) ? str[..^suffix.Length] : null;
		}
	}
}
