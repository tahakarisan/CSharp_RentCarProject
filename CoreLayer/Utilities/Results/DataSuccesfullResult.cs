using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results
{
    public class DataSuccesfullResult<T> : DataResult<T>
    {
        public DataSuccesfullResult(T data, string message) : base(data, true, message)
        {

        }
        public DataSuccesfullResult(T data) : base(data, true)
        {

        }
    }
}
