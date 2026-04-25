using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Cupl.Watchables;
using Cupl.Watchables.Collections;

namespace Cupl.Watchables.Tests;

public class WatchableArray_Should
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
		var array = new WatchableArray<int>(Enumerable.Repeat(0, 10));

		array.ValueChanged +=
			e =>
			{
				Assert.That(e, Is.EquivalentTo(array));
				ran.Value = true;
			};
		
		array[5] = 42;
		Assert.That(array[5], Is.EqualTo(42));
		Assert.That(ran.Value);
		ran.Value = false;

		array.CopyFrom(6, new int[] { 6, 7, 8 });
		Assert.That(array[5], Is.EqualTo(42));
		Assert.That(array[6], Is.EqualTo(6));
		Assert.That(array[7], Is.EqualTo(7));
		Assert.That(array[8], Is.EqualTo(8));
		Assert.That(array[9], Is.EqualTo(0));
		Assert.That(ran.Value);
		ran.Value = false;
	}

	[Test]
	public void TestCollectionChangedChanged()
	{
		var ran = new Mutable<bool>(false);
		var expectedStartIndex = new Mutable<int>(-1);
		var expectedOldItems = new Mutable<IEnumerable<int>>([]);
		var expectedNewItems = new Mutable<IEnumerable<int>>([]);
		var array = new WatchableArray<int>(Enumerable.Repeat(0, 10));

		array.CollectionChanged +=
			(o, e) =>
			{
				Assert.That(e.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
				Assert.That(e.OldStartingIndex, Is.EqualTo(expectedStartIndex.Value));
				Assert.That(e.NewStartingIndex, Is.EqualTo(expectedStartIndex.Value));
				Assert.That(e.OldItems, Is.EquivalentTo(expectedOldItems.Value));
				Assert.That(e.NewItems, Is.EquivalentTo(expectedNewItems.Value));
				ran.Value = true;
			};
		
		expectedStartIndex.Value = 5;
		expectedOldItems.Value = [0];
		expectedNewItems.Value = [42];
		array[5] = 42;
		Assert.That(ran.Value);
		ran.Value = false;

		expectedStartIndex.Value = 4;
		expectedOldItems.Value = [0, 42, 0];
		expectedNewItems.Value = [4, 5, 6];
		array.CopyFrom(4, new int[] { 4, 5, 6 });
		Assert.That(ran.Value);
		ran.Value = false;
	}
}
