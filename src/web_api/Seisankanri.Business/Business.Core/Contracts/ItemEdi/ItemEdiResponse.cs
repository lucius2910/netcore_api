using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Contracts
{
    public class ItemEdiResponse : BaseEntity
    {
        public string company_cd { get; set; }

        public string item_type { get; set; }

        public string item_cd { get; set; }

        public string item_name1 { get; set; }

        public string? item_name2 { get; set; }

        public string edi_cd { get; set; }

        public DateTime export_date { get; set; }
    }
}
