using System.Linq;
using Cupl.Enumerables;

namespace Cupl.Enumerables.Tests
{
	public class CuplEnumerable_OrNullAltToOrDefaultShould
	{
		private static IEnumerable<IEnumerable<int>> GetNonnullValueTypeDataTestCases()
		{
			var fullData = Enumerable.Range(0, 10).SelectMany(x => new int[] { x, x });

			yield return Enumerable.Empty<int>();
			yield return fullData;
			yield return fullData.Where(x => x % 2 == 0);
			yield return fullData.Where(x => x % 2 == 1);
		}

		public CuplEnumerable_OrNullAltToOrDefaultShould()
		{
		}

		[TestCaseSource(nameof(GetNonnullValueTypeDataTestCases))]
		public void LastOrNull_NoPredicate_EquivalentToOrDefault(IEnumerable<int> data)
		{
			var result = data.LastOrNull();

			Assert.That(result, Is.EqualTo(data.Cast<int?>().LastOrDefault()));
		}

		[TestCaseSource(nameof(GetNonnullValueTypeDataTestCases))]
		public void LastOrNull_Predicate_EquivalentToOrDefault(IEnumerable<int> data)
		{
			Func<int, bool> predicate = x => x % 2 == 0;
			var result = data.LastOrNull(predicate);

			Assert.That(result, Is.EqualTo(data.Cast<int?>().LastOrDefault(x => predicate(x!.Value))));
		}

		[TestCaseSource(nameof(GetNonnullValueTypeDataTestCases))]
		public void FirstOrNull_NoPredicate_EquivalentToOrDefault(IEnumerable<int> data)
		{
			var result = data.FirstOrNull();

			Assert.That(result, Is.EqualTo(data.Cast<int?>().FirstOrDefault()));
		}

		[TestCaseSource(nameof(GetNonnullValueTypeDataTestCases))]
		public void FirstOrNull_Predicate_EquivalentToOrDefault(IEnumerable<int> data)
		{
			Func<int, bool> predicate = x => x % 2 == 0;
			var result = data.FirstOrNull(predicate);

			Assert.That(result, Is.EqualTo(data.Cast<int?>().FirstOrDefault(x => predicate(x!.Value))));
		}
	}
}
