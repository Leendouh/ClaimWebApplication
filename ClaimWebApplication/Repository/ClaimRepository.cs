using ClaimWebApplication.Data;
using ClaimWebApplication.Interface;
using ClaimWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimWebApplication.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Claim claim)
        {
            // calling this add/generating all the sql
            _context.Add(claim);

            // save - sending the sql to the database to create entity
            return Save();
        }

        public async Task AddClaimAsync(Claim claim)
        {
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
        }

        public bool Delete(Claim claim)
        {
            _context.Remove(claim);
            return Save();
        }

        public async Task<List<Claim>> GetAllClaimsAsync()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<List<Claim>> GetApprovedClaimsAsync()
        {
            return await _context.Claims
                .Where(c => c.Status == "Approved")
                .ToListAsync();
        }

        public async Task<Claim> GetClaimByIdAsync(int id)
        {
            return await _context.Claims
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task<List<Claim>> GetPendingClaimsAsync()
        {
            return await _context.Claims
                .Where(c => c.Status == "Pending")
                .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task UpdateClaimAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }
    }
}
