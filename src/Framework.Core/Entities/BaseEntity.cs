﻿using Framework.Core.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Framework.Core.Entities
{
    public abstract class BaseEntity : IAudit, ISoftDelete, IEntity<Guid>
    {
        [Key]
        public Guid id { get; set; }
        public DateTime created_date { get; set; }
        public Guid created_by { get; set; }
        public DateTime? updated_date { get; set; }
        public Guid? updated_by { get; set; }
        public bool del_flg { get; set; }
    }
}
