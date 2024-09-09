using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(T data, bool success, string message) : this(data, success)
        {
            Data = data;
            Success = success;
            Message = message;
        }
        public DataResult(T data, bool success):this(success)
        {

        }
        public DataResult(bool success)
        {

        }
        public string Message { get; }

        public T Data { get; }

        public bool Success { get; }

    }
}
