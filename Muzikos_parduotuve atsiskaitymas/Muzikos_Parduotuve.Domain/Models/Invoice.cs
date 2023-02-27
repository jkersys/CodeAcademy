using System;
using System.Collections.Generic;

namespace Muzikos_Parduotuve.Domain.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? BillingAddress { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingCountry { get; set; }
        public string? BillingPostalCode { get; set; }
        public double Total { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
