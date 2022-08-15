using Domain.Abstractions;

namespace Domain.Entities
{
    public class Branch : Company
    {
        public int branch_f { get; set; }

        public Branch()
        {
            this.branch_f = 1;
        }
    }
}