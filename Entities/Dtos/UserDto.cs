using CoreLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserDto:IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public bool Status { get; set; }

    }
}
