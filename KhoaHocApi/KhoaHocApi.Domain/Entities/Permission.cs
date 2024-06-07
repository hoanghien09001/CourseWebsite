﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Permission : BaseEntities
    {
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public long RoleId {  get; set; }
        public virtual Role? Role { get; set;}
    }
}
