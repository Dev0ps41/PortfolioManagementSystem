using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioManagementSystem.Data;
using PortfolioManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Enforce authentication for all endpoints
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioDbContext _context;

        public PortfolioController(PortfolioDbContext context)
        {
            _context = context;
        }

        // Get all investments for the authenticated user
        [HttpGet("GetPortfolio")]
        public async Task<ActionResult<IEnumerable<Investment>>> GetPortfolio()
        {
            var userId = User.Identity.Name; // Extract the authenticated user's ID
            if (userId == null) return Unauthorized();

            return await _context.Investments.Where(i => i.UserId == userId).ToListAsync();
        }

        // Add a new investment
        [HttpPost("AddInvestment")]
        public async Task<ActionResult> AddInvestment(Investment investment)
        {
            var userId = User.Identity.Name;
            if (userId == null) return Unauthorized();

            investment.UserId = userId; // Assign the investment to the authenticated user
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Investment added successfully." });
        }

        // Delete an investment
        [HttpDelete("DeleteInvestment/{id}")]
        public async Task<ActionResult> DeleteInvestment(int id)
        {
            var userId = User.Identity.Name;
            if (userId == null) return Unauthorized();

            var investment = await _context.Investments.FindAsync(id);
            if (investment == null) return NotFound();
            if (investment.UserId != userId) return Forbid(); // Prevent deletion of another user's investment

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Investment deleted successfully." });
        }
    }
}
