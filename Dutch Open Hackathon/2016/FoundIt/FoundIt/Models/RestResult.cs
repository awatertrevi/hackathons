//
// RestPair.cs
//
// Trevi Awater (awatertrevi@gmail.com)
// 12/10/2016
//
using System;
namespace FoundIt.Models
{
    public class RestResult<T>
    {
        public RestResult(T result, bool status)
        {
            Result = result;
            Success = status;
        }

        public T Result { get; set; }
        public bool Success { get; set; }
    }
}
