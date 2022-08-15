using Domain.Abstractions;

namespace Domain.Entities
{
    public class Maker : Company
    {
        public int maker_f { get; set; }

        public Maker()
        {
            this.maker_f = 1;
        }
    }
}