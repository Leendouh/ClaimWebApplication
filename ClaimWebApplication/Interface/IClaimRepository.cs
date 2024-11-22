using ClaimWebApplication.Models;

namespace ClaimWebApplication.Interface
{
    public interface IClaimRepository
    {
        Task<List<Claim>> GetAllClaimsAsync();
        Task<Claim> GetClaimByIdAsync(int id);
        Task AddClaimAsync(Claim claim);
        Task UpdateClaimAsync(Claim claim);
        Task<List<Claim>> GetPendingClaimsAsync();
        Task<List<Claim>> GetApprovedClaimsAsync();


        // CRUD's statements/commands
        bool Add(Claim claim);
        bool Delete(Claim claim);
        bool Save();
    }
}
