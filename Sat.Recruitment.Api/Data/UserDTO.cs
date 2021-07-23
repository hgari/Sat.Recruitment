using Sat.Recruitment.Api.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{
    public class UserDTO
    {
        [Required(AllowEmptyStrings = false)]
        
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        public User MapObjects(UserDTO stud)
        {
            
            
            User user= new User()
            {
                Name = stud.Name,
                Email = StringFunctions.NormalizeEmail(stud.Email),
                Address = stud.Address,
                Phone = stud.Phone,
                UserType = stud.UserType,
                Money = stud.Money

            };
            user.Initialize();
            return user;
        }
    }
}
