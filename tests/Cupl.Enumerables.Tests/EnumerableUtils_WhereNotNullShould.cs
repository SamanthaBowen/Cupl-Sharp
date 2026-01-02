using System.Linq;
using Cupl.Enumerables;

namespace Cupl.Enumerables.Tests
{
	public class EnumerableUtils_WhereNotNullShould
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void WhereNotNullRefType_InputNoNulls_ReturnSame()
		{
			var strings = Enumerable.Range(0, 10).SelectMany(x => new string?[] { x.ToString(), x.ToString() });
			var result = strings.WhereNotNull();

			Assert.That(result, Is.EquivalentTo(strings));
		}

		[Test]
		public void WhereNotNullRefType_InputEmpty_ReturnEmpty()
		{
			var result = Enumerable.Empty<string?>().WhereNotNull();

			Assert.That(result, Is.EquivalentTo(Enumerable.Empty<string>()));
		}

		[Test]
		public void WhereNotNullValType_InputNoNulls_ReturnSame()
		{
			var ints = Enumerable.Range(0, 10).SelectMany(x => new int?[] { x, x });
			var result = ints.WhereNotNull();

			Assert.That(result, Is.EquivalentTo(ints));
		}

		[Test]
		public void WhereNotNullValType_InputEmpty_ReturnEmpty()
		{
			var result = Enumerable.Empty<int?>().WhereNotNull();

			Assert.That(result, Is.EquivalentTo(Enumerable.Empty<int>()));
		}
	}
}
