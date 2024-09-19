using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results
{
    public class SuccesfulDataResult<T> : DataResult<T>
    {
        public SuccesfulDataResult(T data, string message) : base(data, true, message)
        {

        }
        public SuccesfulDataResult(T data) : base(data, true)
        {

        }
        public SuccesfulDataResult(string message) : base(default, true,message)
        {

        }

    }
}
