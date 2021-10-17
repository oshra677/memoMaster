using Common1;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoAllAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }



        [HttpPost("LoginUser")]
        public string Post(CUserAll user)
        {
            return userService.AddUser(user);
        }
      
        [HttpGet("EnterUser")]

        public string Get(string id)
        {
            
            return userService.Enter_User(id);

        }  

    }


}
