using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager_App.Models
{
    public class SupplierInvoice
    {
        public int InvoiceId { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string PaymentTerms { get; set; }
        public string Status { get; set; }
        public string SupplierName { get; set; } // For display purposes
    }
}
