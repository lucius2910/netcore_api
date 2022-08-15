namespace Domain.Entities
{
    public class Customer : Company
    {
        public int customer_f { get; set; }

        public Customer()
        {
            this.customer_f = 1;
        }
    }
}