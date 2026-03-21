using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using Cupl.Watchables;

namespace Cupl.Watchables.Tests;

public class Watchable_Should
{
	private class WatchableMutable<T> : IWatchable<T>
	{
		public event Action<T>? ValueChanged;

		private T value_;
		public T Value
		{
			get => value_!;
			set
			{
				value_ = value;
				ValueChanged?.Invoke(value_);
			}
		}

		public WatchableMutable(T initialValue)
		{
			value_ = initialValue;
		}
	}

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
	public void TestMap()
	{
		var ran = new Mutable<bool>(false);
		var mutable = new WatchableMutable<int>(5);

		var mappedToString = mutable.Map(i => i.ToString());

		mappedToString.ValueChanged +=
			str =>
			{
				Assert.That(str, Is.EqualTo(mutable.Value.ToString()));
				ran.Value = true;
			};
		
		mutable.Value = 42;
		Assert.That(mappedToString.Value, Is.EqualTo((42).ToString()));
		Assert.That(ran.Value);
	}

	[Test]
	public void TestUnwrap()
	{
		var ran = new Mutable<bool>(false);
		var mutable0 = new WatchableMutable<int>(5);
		var mutable1 = new WatchableMutable<int>(42);
		var mutableWatchable = new WatchableMutable<IWatchable<int>>(mutable0);

		var unwrapped = mutableWatchable.Unwrap();

		unwrapped.ValueChanged +=
			i =>
			{
				Assert.That(i, Is.EqualTo(mutableWatchable.Value.Value));
				ran.Value = true;
			};
		
		mutable0.Value = 10;
		Assert.That(unwrapped.Value, Is.EqualTo(10));
		Assert.That(ran.Value);
		ran.Value = false;

		mutable1.Value = 0;
		mutableWatchable.Value = mutable1;
		Assert.That(unwrapped.Value, Is.EqualTo(0));
		Assert.That(ran.Value);
		ran.Value = false;

		mutable0.Value = 42;
		Assert.That(unwrapped.Value, Is.EqualTo(0));
		Assert.That(!ran.Value);
		ran.Value = false;

		mutable1.Value = 16;
		Assert.That(unwrapped.Value, Is.EqualTo(16));
		Assert.That(ran.Value);
		ran.Value = false;
	}

	[Test]
	public void TestWatchManyEnumerable()
	{
		WatchableMutable<int>[] arrayWatchables = Enumerable.Range(0, 8).Select(i => new WatchableMutable<int>(i)).ToArray();
		
		var watchableFirst3 = Watchable.WatchMany(arrayWatchables.Take(3));
		var watchableFirst3Ran = new Mutable<bool>(false);

		var mutableEnumerable = new WatchableMutable<IEnumerable<IWatchable<int>>>(arrayWatchables.Skip(1).Take(4));
		var watchableEnumerable = mutableEnumerable.ToWatchableEnumerable().WatchMany();
		var mutableEnumerableRan = new Mutable<bool>(false);

		watchableFirst3.ValueChanged +=
			s =>
			{
				Assert.That(s, Is.EquivalentTo(arrayWatchables.Take(3).Select(w => w.Value)));
				watchableFirst3Ran.Value = true;
			};
		
		watchableEnumerable.ValueChanged +=
			s =>
			{
				Assert.That(s, Is.EquivalentTo(watchableEnumerable.Value));
				mutableEnumerableRan.Value = true;
			};
		
		arrayWatchables[1].Value = 42;
		Assert.That(watchableFirst3, Is.EquivalentTo([0, 42, 2]));
		Assert.That(watchableFirst3Ran.Value);
		watchableFirst3Ran.Value = false;
		Assert.That(watchableEnumerable, Is.EquivalentTo([42, 2, 3, 4]));
		Assert.That(mutableEnumerableRan.Value);
		mutableEnumerableRan.Value = false;

		arrayWatchables[4].Value = 512;
		Assert.That(watchableFirst3, Is.EquivalentTo([0, 42, 2]));
		Assert.That(!watchableFirst3Ran.Value);
		watchableFirst3Ran.Value = false;
		Assert.That(watchableEnumerable, Is.EquivalentTo([42, 2, 3, 512]));
		Assert.That(mutableEnumerableRan.Value);
		mutableEnumerableRan.Value = false;

		mutableEnumerable.Value = arrayWatchables.Skip(5).Take(3);
		Assert.That(watchableEnumerable, Is.EquivalentTo([5, 6, 7]));
		Assert.That(mutableEnumerableRan.Value);
		mutableEnumerableRan.Value = false;
	}

	[Test]
	public void TestWatchManyTuple()
	{
		WatchableMutable<int> watchable1 = new(0);
		WatchableMutable<string> watchable2 = new("ABC");
		WatchableMutable<double> watchable3 = new(1.5);
		WatchableMutable<string> watchable4 = new("");
		var ran = new Mutable<bool>(false);

		(watchable1, watchable2, watchable3, watchable4).WatchMany().ValueChanged +=
			tuple =>
			{
				Assert.That(tuple.Item1, Is.EqualTo(watchable1.Value));
				Assert.That(tuple.Item2, Is.EqualTo(watchable2.Value));
				Assert.That(tuple.Item3, Is.EqualTo(watchable3.Value));
				Assert.That(tuple.Item4, Is.EqualTo(watchable4.Value));
				ran.Value = true;
			};

		watchable1.Value = 42;
		Assert.That(ran.Value);
		ran.Value = false;

		watchable2.Value = "xyz";
		Assert.That(ran.Value);
		ran.Value = false;

		watchable3.Value = 1.2345;
		Assert.That(ran.Value);
		ran.Value = false;

		watchable4.Value = "something";
		Assert.That(ran.Value);
		ran.Value = false;
	}
}
