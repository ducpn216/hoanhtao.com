using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Views
{
    public class PermissionView
    {
        public int PermissionId { get; set; }
        public int FunctionId { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
    }
}
