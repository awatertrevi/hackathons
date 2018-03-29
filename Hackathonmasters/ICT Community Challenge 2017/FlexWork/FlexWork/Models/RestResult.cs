//
// RestResult.cs
//
// Trevi Awater (t.awater@resoftware.nl)
// 11/18/2016
//

using System;

namespace FlexWork.Models
{
	public class RestResult<T>
	{
		public RestResult(T result, bool status, string message = null)
		{
			Result = result;
			Success = status;
			Message = message;
		}

		public T Result { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
	}
}
