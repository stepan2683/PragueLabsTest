using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static PragueLabsTest.Class.Enums;

namespace PragueLabsTest.Class
{
    public class Invoice
    {
        public int Id { get; set; }

        [DisplayName("Paid Status")]
        public PaidStatus Status { get; set; }

        [DisplayName("Invoice Name")]
        public string Name { get; set; }
        public long Sum
        {
            get
            {
                return Items != null && Items.Count > 0 ? Items.Sum(a => a.PriceForOneItem * a.Count) : 0;
            }
        }
        public virtual List<InvoiceItem> Items { get; set; }
    }

    public class InvoiceRequest
    {
        public int Id { get; set; }
        public PaidStatus Status { get; set; }
        public string Name { get; set; }
    }

}