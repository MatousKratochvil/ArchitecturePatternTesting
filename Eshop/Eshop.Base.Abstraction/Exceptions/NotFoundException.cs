﻿using System;
using System.Runtime.Serialization;

namespace Eshop.Base.Abstraction.Exceptions
{
	[Serializable]
	public class NotFoundException : Exception
	{
		public string DomainName { get; }
		public object Id { get; }

		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public NotFoundException()
		{
		}

		public NotFoundException(string domainName, object id) : base($"NotFound - {domainName}: {id}")
		{
			DomainName = domainName;
			Id = id;
		}

		protected NotFoundException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}