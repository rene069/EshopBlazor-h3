using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        List<User> Users { get; set; }
    }
}
