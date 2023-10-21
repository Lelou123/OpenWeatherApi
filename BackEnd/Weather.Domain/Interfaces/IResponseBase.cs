using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Interfaces
{
    public interface IResponseBase<T>
    {
        T? Data { get; set; }
        bool IsSuccess { get; set; }
        string? Message { get; set; }
        Exception? Exception { get; set; }
    }
}
