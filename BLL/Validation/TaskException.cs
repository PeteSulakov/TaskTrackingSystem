using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace BLL.Validation
{
	[Serializable]
	public class TaskException : Exception
	{
		public HttpStatusCode StatusCode { get; }
		public TaskException()
		{ }

		public TaskException(string message, HttpStatusCode statusCode) : base(message)
		{
			StatusCode = statusCode;
		}

		protected TaskException(SerializationInfo info, StreamingContext context)
			: base(info, context) 
		{ }

		public TaskException(string message, Exception innerException) : base (message, innerException) 
		{ }

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
