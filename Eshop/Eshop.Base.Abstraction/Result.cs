using System;
using System.Threading.Tasks;

namespace Eshop.Base.Abstraction
{
	/// <summary>
	/// Represent result of operation without throwing exception.
	/// </summary>
	/// <typeparam name="T">Type of successful result</typeparam>
	public sealed record Result<T>
	{
		private Result(T value, Exception exception)
		{
			Value = value;
			Exception = exception;

			IsSuccess = value != null;
			IsFailure = exception != null;
		}

		/// <summary>
		/// Value of successful operation.
		/// </summary>
		public T Value { get; }

		/// <summary>
		/// Exception represent failed operation.
		/// </summary>
		public Exception Exception { get; }

		/// <summary>
		/// Check for success.
		/// </summary>
		public bool IsSuccess;


		/// <summary>
		/// Check for failure.
		/// </summary>
		public bool IsFailure;

		/// <summary>
		/// Creates successful operation result
		/// </summary>
		/// <param name="value">Value of result</param>
		/// <returns>Successful operation wrapped in Result</returns>
		public static Result<T> Success(T value) => new(value, null);
		
		/// <summary>
		/// Creates successful operation result
		/// </summary>
		/// <param name="value">Value of result</param>
		/// <returns>Successful operation wrapped in Result</returns>
		public static Task<Result<T>> SuccessTask(T value) => Task.FromResult(Success(value));
		
		/// <summary>
		/// Creates unsuccessful operation result
		/// </summary>
		/// <param name="exception">Exception of result</param>
		/// <returns>Unsuccessful operation wrapped in Result</returns>
		public static Result<T> Failed(Exception exception) => new(default, exception);
		
		/// <summary>
		/// Creates unsuccessful operation result wrapped in Task.FromResult
		/// </summary>
		/// <param name="exception">Exception of result</param>
		/// <returns>Unsuccessful operation wrapped in Result</returns>
		public static Task<Result<T>> FailedTask(Exception exception) => Task.FromResult(Failed(exception));

		/// <summary>
		/// Call operation and return result wrapped in Result ( little bit confusing :) ).
		/// </summary>
		/// <param name="operation">Operation to call</param>
		/// <returns>Result of operation wrapped in Result</returns>
		public static Result<T> TryOperation(Func<T> operation)
		{
			try
			{
				return Success(operation());
			}
			catch (Exception e)
			{
				return Failed(e);
			}
			
		}
	}
}