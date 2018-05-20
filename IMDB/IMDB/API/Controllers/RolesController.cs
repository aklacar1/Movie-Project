using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMDB.DataLayer.Entities;
using IMDB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IMDB.DataLayer.Models.BindingModels;

namespace IMDB.API.Controllers
{
    /// <summary>
    /// This controller is used only for Role data changes, everything else regarding roles is done in UserController
    /// </summary>
    [Produces("application/json")]
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        private readonly MovieDBContext _context;
        private readonly RoleManager<Roles> roleManager;

        public RolesController(MovieDBContext context, RoleManager<Roles> roleManager)
        {
            _context = context;
            this.roleManager = roleManager;
        }

        // GET: api/Roles
        [HttpGet]
        [Authorize(Roles = "SU")]
        public IEnumerable<Roles> GetRoles()
        {
            return _context.Roles;
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> GetRoles([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> PutRoles([FromRoute] string id, [FromBody] Roles roles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roles.Id)
            {
                return BadRequest();
            }

            _context.Entry(roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        [Authorize(Roles = "SU")]
        //[AllowAnonymous]
        public async Task<IActionResult> PostRoles([FromBody] insertRoleModel roles)
        {
            Roles role = new Roles() { Name = roles.Name, NormalizedName = roles.NormalizedName };
            IdentityResult result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            

            return Ok("New Role created");
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SU")]
        public async Task<IActionResult> DeleteRoles([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roles = await _context.Roles.SingleOrDefaultAsync(m => m.Id == id);
            if (roles == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();

            return Ok(roles);
        }

        private bool RolesExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}