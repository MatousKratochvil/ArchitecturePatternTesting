using System;
using System.Runtime.Serialization;

namespace ThrowOrReturnException
{
	[Serializable]
	public class ThrownException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public ThrownException()
		{
		}

		public ThrownException(string message) : base(message)
		{
		}

		public ThrownException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ThrownException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}