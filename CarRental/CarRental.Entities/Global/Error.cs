using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Global;
public class Error
{
    public int Code { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Messqge { get; set; } = string.Empty;
}
