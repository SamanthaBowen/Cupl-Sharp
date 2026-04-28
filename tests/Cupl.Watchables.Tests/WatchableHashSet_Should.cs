using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Cupl.Watchables;
using Cupl.Watchables.Collections;

namespace Cupl.Watchables.Tests;

public class WatchableHashSet_Should
{
	private class Mutable<T>
	{
		public T Value { get; set; }

		public Mutable(T value)
		{
			Value = value;
		}
	}

	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void TestValueChanged()
	{
		var ran = new Mutable<bool>(false);
		var set = new WatchableHashSet<int>();

		set.ValueChanged +=
			e =>
			{
				Assert.That(e, Is.EquivalentTo(set));
				ran.Value = true;
			};

		Assert.That(set, Is.EquivalentTo(new int[0]));

		set.Add(42);
		Assert.That(set, Is.EquivalentTo([42]));
		Assert.That(ran.Value);
		ran.Value = false;

		set.Add(0);
		Assert.That(set, Is.EquivalentTo([0, 42]));
		Assert.That(ran.Value);
		ran.Value = false;

		set.Add(128);
		Assert.That(set, Is.EquivalentTo([0, 42, 128]));
		Assert.That(ran.Value);
		ran.Value = false;

		set.UnionWith([0, 1, 2]);
		Assert.That(set, Is.EquivalentTo([0, 1, 2, 42, 128]));
		Assert.That(ran.Value);
		ran.Value = false;

		set.SymmetricExceptWith([3, 4, 128]);
		Assert.That(set, Is.EquivalentTo([0, 1, 2, 3, 4, 42]));
		Assert.That(ran.Value);
		ran.Value = false;

		set.ExceptWith([2, 4, 42]);
		Assert.That(set, Is.EquivalentTo([0, 1, 3]));
		Assert.That(ran.Value);
		ran.Value = false;

		set.Clear();
		Assert.That(set, Is.EquivalentTo(new int[0]));
		Assert.That(ran.Value);
		ran.Value = false;
	}
}
