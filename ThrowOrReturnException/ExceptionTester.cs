using LanguageExt.Common;

namespace ThrowOrReturnException
{
	public class ExceptionTester
	{
		public int ThrowException()
		{
			throw new ThrownException();
		}

		public Result<int> ReturnWrapped()
		{
			return new(new ThrownException());
		}
	}
}