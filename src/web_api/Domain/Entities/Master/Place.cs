namespace Domain.Entities
{
    public class Place : Company
    {
        public int place_f { get; set; }

        public Place()
        {
            this.place_f = 1;
        }
    }
}