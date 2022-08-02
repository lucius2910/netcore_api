﻿using Framework.Core.Entities;
using Seisankanri.Model.Core;

namespace Seisankanri.Model.Entities
{
    public class Calendar : BaseEntity
    {
        public DateTime? calendar_date { get; set; }

        public string company_cd { get; set; }

        public CalendarStatus open_status { get; set; }

        public virtual Company? company { get; set; }

        public Calendar()
        {
            id = Guid.NewGuid();
        }
    }
}
