using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using LanguageExt;

namespace ThrowOrReturnException
{
	class Program
	{
		static void Main(string[] args)
		{
			BenchmarkDotNet.Running.BenchmarkRunner.Run<ThrowOrReturn>();
		}
	}

	[MemoryDiagnoser]
	[RyuJitX64Job]
	[MarkdownExporter]
	public class ThrowOrReturn
	{
		public ExceptionTester _tester;
		public Consumer _consumer;
		
		[GlobalSetup]
		public void GlobalSetup()
		{
			_tester = new ExceptionTester();
			_consumer = new Consumer();
		}
		
		[Benchmark]
		public void Exception_Is_Thrown()
		{
			try
			{
				var i = _tester.ThrowException();
				_consumer.Consume(i);
			}
			catch (Exception e)
			{
				// Do nothing
				_consumer.Consume(e);
			}
		}

		[Benchmark]
		public void Exception_Is_Returned_In_WrappedClass()
		{
			_consumer.Consume(_tester.ReturnWrapped().Match(
				number => number,
				exception =>
				{
					_consumer.Consume(exception);
					return 0;
				}
			));
		}

		[Benchmark]
		public void Exception_Is_Catched_In_LanguageExtTryClass()
		{
			var result = new Try<int>(() => _tester.ThrowException());
			
			_consumer.Consume(result.Match(
				number => number,
				exception =>
				{
					_consumer.Consume(exception);
					return 0;
				}
			));
		}
		
	}
}