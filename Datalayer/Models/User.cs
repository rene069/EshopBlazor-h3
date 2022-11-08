using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public int RoleId { get; set; }
        public bool IsSoftDeleted { get; set; }

        public Role Role { get; set; }
        public List<UserProdukt>? UserProdukts { get; set; }

    }
}
