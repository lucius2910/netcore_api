using Framework.Core.Collections;

namespace Business.Core.Contracts
{
    public class ClassificationSearchRequest : PagedRequest
    {
        public string code { get; set; }
        public string name { get; set; }
        public int flag { get; set; }


    }
}
