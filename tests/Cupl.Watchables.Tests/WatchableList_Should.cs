using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Cupl.Watchables;
using Cupl.Watchables.Collections;

namespace Cupl.Watchables.Tests;

public class WatchableList_Should
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
		var list = new WatchableList<int>();

		list.ValueChanged +=
			e =>
			{
				Assert.That(e, Is.EquivalentTo(list));
				ran.Value = true;
			};
		
		list.Add(42);
		Assert.That(ran.Value);
		ran.Value = false;

		list.Add(6);
		Assert.That(ran.Value);
		ran.Value = false;

		list.Add(5);
		Assert.That(ran.Value);
		ran.Value = false;

		Assert.That(list.Count, Is.EqualTo(3));
		Assert.That(list[0], Is.EqualTo(42));
		Assert.That(list[1], Is.EqualTo(6));
		Assert.That(list[2], Is.EqualTo(5));

		list[1] = 0;
		Assert.That(list.Count, Is.EqualTo(3));
		Assert.That(list[1], Is.EqualTo(0));
		Assert.That(ran.Value);
		ran.Value = false;

		list.RemoveAt(1);
		Assert.That(list.Count, Is.EqualTo(2));
		Assert.That(list[1], Is.EqualTo(5));
		Assert.That(ran.Value);
		ran.Value = false;

		list.AddRange([10, 20, 30]);
		Assert.That(list.Count, Is.EqualTo(5));
		Assert.That(list[2], Is.EqualTo(10));
		Assert.That(list[3], Is.EqualTo(20));
		Assert.That(list[4], Is.EqualTo(30));
		Assert.That(ran.Value);
		ran.Value = false;

		list.RemoveRange(0, 2);
		Assert.That(list.Count, Is.EqualTo(3));
		Assert.That(list[0], Is.EqualTo(10));
		Assert.That(list[1], Is.EqualTo(20));
		Assert.That(list[2], Is.EqualTo(30));
		Assert.That(ran.Value);
		ran.Value = false;

		list.Clear();
		Assert.That(list.Count, Is.EqualTo(0));
		Assert.That(ran.Value);
		ran.Value = false;
	}

	[Test]
	public void TestCollectionChangedChanged()
	{
		var ran = new Mutable<bool>(false);
		var expectedAction = new Mutable<NotifyCollectionChangedAction>(NotifyCollectionChangedAction.Reset);
		var expectedOldStartIndex = new Mutable<int?>(null);
		var expectedOldItems = new Mutable<IEnumerable<int>?>([]);
		var expectedNewStartIndex = new Mutable<int?>(null);
		var expectedNewItems = new Mutable<IEnumerable<int>?>([]);
		var list = new WatchableList<int>();

		list.CollectionChanged +=
			(o, e) =>
			{
				Assert.That(e.Action, Is.EqualTo(expectedAction.Value));

				if (expectedOldStartIndex.Value.HasValue)
					Assert.That(e.OldStartingIndex, Is.EqualTo(expectedOldStartIndex.Value.Value));

				if (expectedOldItems.Value is not null)
					Assert.That(e.OldItems, Is.EquivalentTo(expectedOldItems.Value));
				else
					Assert.That(e.OldItems, Is.EqualTo(expectedOldItems.Value));

				if (expectedNewStartIndex.Value.HasValue)
					Assert.That(e.NewStartingIndex, Is.EqualTo(expectedNewStartIndex.Value.Value));

				if (expectedNewItems.Value is not null)
					Assert.That(e.NewItems, Is.EquivalentTo(expectedNewItems.Value));
				else
					Assert.That(e.NewItems, Is.EqualTo(expectedNewItems.Value));

				ran.Value = true;
			};
		
		expectedAction.Value = NotifyCollectionChangedAction.Add;
		expectedOldStartIndex.Value = null;
		expectedOldItems.Value = null;
		expectedNewStartIndex.Value = 0;
		expectedNewItems.Value = [42];
		list.Add(42);
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Add;
		expectedOldStartIndex.Value = null;
		expectedOldItems.Value = null;
		expectedNewStartIndex.Value = 1;
		expectedNewItems.Value = [6];
		list.Add(6);
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Add;
		expectedOldStartIndex.Value = null;
		expectedOldItems.Value = null;
		expectedNewStartIndex.Value = 2;
		expectedNewItems.Value = [5];
		list.Add(5);
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Replace;
		expectedOldStartIndex.Value = 1;
		expectedOldItems.Value = [6];
		expectedNewStartIndex.Value = 1;
		expectedNewItems.Value = [0];
		list[1] = 0;
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Remove;
		expectedOldStartIndex.Value = 1;
		expectedOldItems.Value = [0];
		expectedNewStartIndex.Value = null;
		expectedNewItems.Value = null;
		list.RemoveAt(1);
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Add;
		expectedOldStartIndex.Value = null;
		expectedOldItems.Value = null;
		expectedNewStartIndex.Value = 2;
		expectedNewItems.Value = [10, 20, 30];
		list.AddRange([10, 20, 30]);
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Remove;
		expectedOldStartIndex.Value = 0;
		expectedOldItems.Value = [42, 5];
		expectedNewStartIndex.Value = null;
		expectedNewItems.Value = null;
		list.RemoveRange(0, 2);
		Assert.That(ran.Value);
		ran.Value = false;

		expectedAction.Value = NotifyCollectionChangedAction.Remove;
		expectedOldStartIndex.Value = 0;
		expectedOldItems.Value = [10, 20, 30];
		expectedNewStartIndex.Value = null;
		expectedNewItems.Value = null;
		list.Clear();
		Assert.That(ran.Value);
		ran.Value = false;
	}
}
