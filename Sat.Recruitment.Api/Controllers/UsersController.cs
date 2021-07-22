using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IListUserService _IListUserService ;
        public UsersController(IListUserService listUserService)
        {
            _IListUserService = listUserService;
        }

        [HttpPost]
        
        public IActionResult Create([FromBody]UserDTO userDTO)
        {

            if (userDTO == null)
                return BadRequest();

            
            if (ModelState.IsValid )
            {
                User user = userDTO.MapObjects(userDTO);
                user.Initialize();
                Result result = this._IListUserService.Add(user);

                if (result.IsSuccess)
                {
                    return Ok("User Created");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
         
            }
            
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            return BadRequest(messages);
            
        }

    }

}
