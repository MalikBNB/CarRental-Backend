using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Global;
public class PagedResult<T> : Result<List<T>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int ResultCount { get; set; }
}
