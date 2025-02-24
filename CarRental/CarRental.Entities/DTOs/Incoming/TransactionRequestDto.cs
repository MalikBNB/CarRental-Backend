using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Incoming;
public class TransactionRequestDto
{
    public Guid VehicleReturnId { get; set; }
    public virtual VehicleReturn? VehicleReturn { get; set; }
    public string PaymentDetails { get; set; } = string.Empty;
    public decimal PaidAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal RestAmount { get; set; }
    public decimal RefundedAmount { get; set; }
}
