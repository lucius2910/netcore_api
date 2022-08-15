namespace Domain.Entities
{
    public class Destination : Company
    {
        public int destination_f { get; set; }

        public Destination()
        {
            this.destination_f = 1;
        }
    }
}

