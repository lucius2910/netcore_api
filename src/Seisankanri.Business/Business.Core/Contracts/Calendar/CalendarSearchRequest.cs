using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Contracts
{
    public class CalendarSearchRequest
    {
        public string company_cd { get; set; }
        public DateTime? date_from { get; set; }
        public DateTime? date_to { get; set; }
    }
}
