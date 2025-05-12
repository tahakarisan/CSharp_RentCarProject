using CoreLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserRoleDto:IDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserLastName { get; set; }
    }
}
