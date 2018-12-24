namespace PragueLabsTest.Models
{
    using PragueLabsTest.Class;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {

        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<Invoice> DbInvoice { get; set; }
        public virtual DbSet<InvoiceItem> DbInvoiceItem { get; set; }
    }

}