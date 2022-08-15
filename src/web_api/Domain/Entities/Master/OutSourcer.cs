using Domain.Abstractions;

namespace Domain.Entities
{
    public class OutSourcer : Company
    {
        public int outsourcer_f { get; set; }

        public OutSourcer()
        {
            this.outsourcer_f = 1;
        }
    }
}