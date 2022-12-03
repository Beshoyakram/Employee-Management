using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAPP.Models;
using MVCAPP.ViewModels;
using System.Security.Claims;

namespace MVCAPP.Controllers
{
    /*[Authorize(Roles = "Admin,User")] //One of them accepted*/
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        #region Roles
        [HttpGet]
        [Authorize(Policy = "CreateRolePolicy")]
        public IActionResult CreateRole()
        {return View();}
        [HttpPost]
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task <IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole() { Name = model.RoleName };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;

            return View(roles); 
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(string ID)
        {
            var role = await _roleManager.FindByIdAsync(ID);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {ID} can't be found.";
                return View("Notfound");
            }
            else
            {
                var model = new EditRoleViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                foreach (var user in await _userManager.Users.ToListAsync())
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        model.Users.Add(user.UserName);
                    }
                }
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {model.RoleId} can't be found.";
                return View("Notfound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string RoleId) 
        {
            ViewBag.RoleId = RoleId; // I will need it in post request

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {RoleId} can't be found.";
                return View("NotFound");
            }
            else 
            {
                var model = new List<UserRoleViewModel>();
                var users =await _userManager.Users.ToListAsync();
                foreach (var user in users)
                {
                    var userRole = new UserRoleViewModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                    };

                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRole.IsSelected = true;
                    }
                    else
                    {
                        userRole.IsSelected = false;
                    }
                    model.Add(userRole);
                }
                return View(model);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model,string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {RoleId} can't be found.";
                return View("NotFound");
            }
            else
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await _userManager.FindByIdAsync(model[i].UserId);
                    IdentityResult result = null;
                    if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                    {
                        result = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    { continue; }

                    //validate result.
                    if (result.Succeeded)
                    {
                        continue;
                    }
                }

                return RedirectToAction("EditRole", new { ID =RoleId});

            }

        }

        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string RoleId) 
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {RoleId} can't be found.";
                return View("NotFound");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    return RedirectToAction("ListRoles");
                }
            }
        }

        #endregion

        #region Users
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);

                var model = new EditUserViewModel()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    City = user.City,
                    Roles = roles,
                    Claims = claims.Select(n => n.Type + " : " + n.Value).ToList()
                };
                
                return View(model);
            }    
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {model.UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                user.Email=model.Email;
                user.UserName=model.UserName;
                user.City=model.City;
                
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        
                    }
                    return View(model);
                }
                
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    return RedirectToAction("ListUsers");
                }

            }

        }
        

        [HttpGet]
        public async Task<IActionResult> ManageUserRole(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                var model =new List<UserRolesViewModel>();
                var roles = _roleManager.Roles.ToList();
                foreach (var role in roles)
                {
                    UserRolesViewModel UserRoles = new UserRolesViewModel()
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                    var result = await _userManager.IsInRoleAsync(user, role.Name);
                    UserRoles.IsSelected = result;

                    model.Add(UserRoles);
                }
                ViewBag.UserId = UserId;
                return View(model);
            }
               
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRole(List<UserRolesViewModel> model,string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                foreach (var role in model)
                {
                    if (role.IsSelected == true && !await _userManager.IsInRoleAsync(user, role.RoleName))
                    {
                        var result = await _userManager.AddToRoleAsync(user, role.RoleName);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("EditUser", new { UserId = UserId });
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return RedirectToAction("EditUser", new { UserId = UserId });
                        }
                    }
                    else if (role.IsSelected == false && await _userManager.IsInRoleAsync(user, role.RoleName))
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("EditUser", new { UserId = UserId });
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return RedirectToAction("EditUser", new { UserId = UserId });
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                return RedirectToAction("EditUser",new {UserId = UserId});
            }

        }

        [HttpGet]
        public async Task<IActionResult> ManageUserCliam(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                var ExistingUserClaims = await _userManager.GetClaimsAsync(user);
                var model = new UserClaimViewModel()
                {
                    UserId = user.Id,

                };
                foreach (var claim in ClaimStore.AllClaims)
                {
                    UserClaim userClaim = new UserClaim()
                    {
                        ClaimType = claim.Type,
                    };
                    if (ExistingUserClaims.Any(n=>n.Type == userClaim.ClaimType && n.Value== "true"))
                    {
                        userClaim.IsSelected = true;
                    }
                    else { userClaim.IsSelected = false; }

                    model.Claims.Add(userClaim);
                }

                return View(model);
            }

        }
        [HttpPost]
        public async Task<IActionResult> ManageUserCliam(UserClaimViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id : {model.UserId} is not fonud";
                return View("NotFound");
            }
            else
            {
                var ExistingClaims = await _userManager.GetClaimsAsync(user);
                var result = await _userManager.RemoveClaimsAsync(user,ExistingClaims);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Can't remove user existing claims");
                    return View(model);
                }
                else
                {
                    result = await _userManager.AddClaimsAsync(user, model.Claims
                        .Select(n => new Claim(n.ClaimType, n.IsSelected ? "true" : "false")));
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Can't add selected claims to users");
                        return View(model);
                    }
                    return RedirectToAction("EditUser", new { UserId = model.UserId });

                }
            }

        }



        #endregion
    }
}
