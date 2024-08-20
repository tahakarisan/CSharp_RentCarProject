using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results
{
    public interface IDataResult<T>
    {
        string Message { get; }
        T Data { get; }
        bool Success { get; }
    }
}
