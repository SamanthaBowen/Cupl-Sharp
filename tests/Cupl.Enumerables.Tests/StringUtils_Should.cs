using System.Linq;
using Cupl.Enumerables;

namespace Cupl.Enumerables.Tests
{
	public class StringUtils_Should
	{
		private static IEnumerable<string> GetTestCases()
		{
			yield return String.Empty;
			yield return "abcdefghijklmnopqrstuvwxyz";
			yield return "abc";
			yield return "xyz";
			yield return String.Concat(Enumerable.Repeat("abc", 4));
		}

		[SetUp]
		public void Setup()
		{
		}

		[TestCaseSource(nameof(GetTestCases))]
		public void IndexOfOrNull_EquivalentToNegative1(string str)
		{
			var value = 'b';
			var result = str.IndexOfOrNull(value);

			Assert.That(result, Is.Not.EqualTo(-1));
			Assert.That(result ?? -1, Is.EqualTo(str.IndexOf(value)));
		}

		[TestCaseSource(nameof(GetTestCases))]
		public void LastIndexOfOrNull_EquivalentToNegative1(string str)
		{
			var value = 'b';
			var result = str.LastIndexOfOrNull(value);

			Assert.That(result, Is.Not.EqualTo(-1));
			Assert.That(result ?? -1, Is.EqualTo(str.LastIndexOf(value)));
		}
	}
}
