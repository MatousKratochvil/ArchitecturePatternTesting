using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace MultipleIterationsToSortOrNotToSort
{
	class Program
	{
		static void Main(string[] args)
		{
			BenchmarkDotNet.Running.BenchmarkRunner.Run<ToSortOrNotToSort>();
		}
	}

	[MemoryDiagnoser]
	[RyuJitX64Job]
	[MarkdownExporter]
	public class ToSortOrNotToSort
	{
		public Consumer _consumer;
		public EnumerableTester _tester;

		[GlobalSetup]
		public void GlobalSetup()
		{
			_consumer = new Consumer();
			_tester = new EnumerableTester();
		}

		[Benchmark]
		public void Multiple_Iteration_FindingItem_ShuffledArray_LINQ()
		{
			var unsortedList = _tester.UnsortedList();

			var firstSearch = unsortedList.FirstOrDefault(x => x.Id == _tester.SearchedGuidShuffled);
			_consumer.Consume(firstSearch);
			
			var secondSearch = unsortedList.FirstOrDefault(x => x.Id == _tester.SearchedGuidShuffled);
			_consumer.Consume(secondSearch);
		}

		[Benchmark]
		public void Multiple_Iteration_FindingItem_SortedArray_LINQ()
		{
			var sortedList = _tester.SortedList();
			
			var firstSearch = sortedList.FirstOrDefault(x => x.Id == _tester.SearchedGuidSorted);
			_consumer.Consume(firstSearch);
			
			var secondSearch = sortedList.FirstOrDefault(x => x.Id == _tester.SearchedGuidSorted);
			_consumer.Consume(secondSearch);
		}
		
		[Benchmark]
		public void Multiple_Iteration_FindingItem_ShuffledArray_For()
		{
			var unsortedList = _tester.UnsortedList();

			Item firstSearch = null;
			for (int i = 0; i < unsortedList.Count; i++)
			{
				if (unsortedList[i].Id == _tester.SearchedGuidShuffled)
				{
					firstSearch = unsortedList[i];
					break;
				}
			}
			_consumer.Consume(firstSearch);
			
			Item secondSearch = null;
			for (int i = 0; i < unsortedList.Count; i++)
			{
				if (unsortedList[i].Id == _tester.SearchedGuidShuffled)
				{
					secondSearch = unsortedList[i];
					break;
				}
			}
			_consumer.Consume(secondSearch);
		}

		[Benchmark]
		public void Multiple_Iteration_FindingItem_SortedArray_For()
		{
			var sortedList = _tester.SortedList();
			
			Item firstSearch = null;
			for (int i = 0; i < sortedList.Count; i++)
			{
				if (sortedList[i].Id == _tester.SearchedGuidSorted)
				{
					firstSearch = sortedList[i];
					break;
				}
			}
			_consumer.Consume(firstSearch);
			
			Item secondSearch = null;
			for (int i = 0; i < sortedList.Count; i++)
			{
				if (sortedList[i].Id == _tester.SearchedGuidSorted)
				{
					secondSearch = sortedList[i];
					break;
				}
			}
			_consumer.Consume(secondSearch);
		}
	}

	public class EnumerableTester
	{
		private List<Item> _shuffledlist = new();
		private List<Item> _sortedList = new();
		
		public Guid SearchedGuidSorted = Guid.Empty;
		public Guid SearchedGuidShuffled = Guid.Empty;

		public EnumerableTester()
		{
			const int count = 1000;

			var list = new List<Item>();
			for (int i = 0; i < count; i++)
			{
				list.Add(new Item
				{
					Count = i,
					Id = Guid.NewGuid(),
					Name = i.ToString()
				});
			}

			_shuffledlist = list.Select(x => (Item) x.Clone()).ToList();
			_sortedList = list.Select(x => (Item) x.Clone()).ToList();
			
			_shuffledlist.Shuffle();
			_sortedList.Sort((a,b) => a.Count.CompareTo(b.Count));

			SearchedGuidShuffled = _shuffledlist[count / 2].Id;
			SearchedGuidSorted = _sortedList[count / 2].Id;
		}
		
		public List<Item> SortedList() => _sortedList;

		public List<Item> UnsortedList() => _shuffledlist;
	}

	public class Item : ICloneable
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Count { get; set; }
		
		public object Clone()
		{
			return new Item()
			{
				Id = Id,
				Name = Name,
				Count = Count
			};
		}
	}

	public static class ListExtensions
	{
		private static Random rng = new();

		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}