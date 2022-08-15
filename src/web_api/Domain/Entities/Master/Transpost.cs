namespace Domain.Entities
{
    public class Transpost : Company
    {
        public int transpost_f { get; set; }

        public Transpost()
        {
            this.transpost_f = 1;
        }
    }
}