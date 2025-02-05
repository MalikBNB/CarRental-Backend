using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Global;
public class Result<T> where T : class
{
    public T Content { get; set; } = null!;
    public Error Error { get; set; } = null!;
    public bool Success => Error == null;
    public DateTime ResponseDate { get; set; } = DateTime.Now;

}
