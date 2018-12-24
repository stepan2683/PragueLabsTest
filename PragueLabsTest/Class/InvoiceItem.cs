using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PragueLabsTest.Class
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        [DisplayName("Items name")]
        public string Name { get; set; }
        public int Count { get; set; }

        [DisplayName("Price for one item")]
        public int PriceForOneItem { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}