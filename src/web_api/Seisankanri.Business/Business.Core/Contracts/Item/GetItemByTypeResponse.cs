

namespace Business.Core.Contracts
{
    public class GetItemByTypeResponse
    {
        public Guid id { get; set; }

        public string code { get; set; }

        public string short_name_kana { get; set; }

        public string name1 { get; set; }

        public string name2 { get; set; }

        public int std_unitprice { get; set; }
    }
}
