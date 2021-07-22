using Sat.Recruitment.Api.Data;
using System.Collections.Generic;

namespace Sat.Recruitment.Api
{
    public interface IListUserService
    {
        List<User> Users { get; set; }
        public bool IsDuplicated(User newUser);

        public Result Add(User newUser);
    }
        
}