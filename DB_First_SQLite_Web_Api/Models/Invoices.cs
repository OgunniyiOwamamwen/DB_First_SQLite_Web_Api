using System;
using System.Collections.Generic;

namespace DB_First_SQLite_Web_Api.Models
{
    public partial class Invoices
    {
        public Invoices()
        {
            InvoiceItems = new HashSet<InvoiceItems>();
        }

        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        public string InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public string Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
    }
}
