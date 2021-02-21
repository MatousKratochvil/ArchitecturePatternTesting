using System;
using System.Runtime.Serialization;

namespace Eshop.Base.Abstraction.Exceptions
{
	[Serializable]
	public class UnsuccessfulUpdateException : Exception
	{
		public string DomainName { get; }

		public object[] Values { get; }
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public UnsuccessfulUpdateException(string domainName, params object[] values) 
			: base($"UnsuccessfulUpdate - {domainName} with values: {{ {string.Join(";", values)} }}")
		{
			DomainName = domainName;
			Values = values;
		}

		protected UnsuccessfulUpdateException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}