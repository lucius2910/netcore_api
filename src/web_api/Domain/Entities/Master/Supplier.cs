namespace Domain.Entities
{
    public class Supplier : Company
    {
        public int supplier_f { get; set; }

        public Supplier()
        {
            this.supplier_f = 1;
        }
    }
}