using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results
{
    public class DataErrorResult<T> : DataResult<T>
    {
        public DataErrorResult(T data, string message) : base(data, false, message)
        {

        }
        public DataErrorResult(T data) : base(data, false)
        {

        }
    }
}
