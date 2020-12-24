using icden.NetCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icden.NetCore.API.Controllers {
    [Route( "[controller]" )]
    [ApiController]
    public class HomeController : ControllerBase {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<string> Index() {

            bool isAdminExist = await this._roleManager.RoleExistsAsync( RoleConstants.ADMINISTRATOR );
            if ( !isAdminExist ) {
                var role = new ApplicationRole();
                role.Name = RoleConstants.ADMINISTRATOR;
                var resultCreateRole = await this._roleManager.CreateAsync( role );
                if ( resultCreateRole.Succeeded ) {
                    ApplicationUser user = await this._userManager.FindByNameAsync( "admin" );
                    if ( user == null ) {
                        user = new ApplicationUser {
                            UserName = "admin",
                            FirstName = "Dogucan",
                            LastName = "Kip",
                            Email = "futurecom.icden@gmail.com"
                        };
                        var resultCreateUser = await this._userManager.CreateAsync( user, "Futurecomicden_159.753" );
                        if ( resultCreateUser.Succeeded ) {
                            var resultAddRole = await _userManager.AddToRoleAsync( user, RoleConstants.ADMINISTRATOR );
                        }
                        else {
                            return "Service is running (The user can not be created)";
                        }
                    }
                }
            }
            return "Service is running";
        }
    }
}
