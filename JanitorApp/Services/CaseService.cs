
using JanitorApp.Context;
using JanitorApp.Models;
using JanitorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JanitorApp.Services;

internal class CaseService
{
    private readonly DataContext _context = new();
    public async Task<int> SaveAsync(CaseRegistration caseRegistration)
    {
        var caseEntity = new CaseEntity
        {
            Title = caseRegistration.Title,
            Description = caseRegistration.Description,
            UserId = caseRegistration.UserId,
        };

        await _context.AddAsync(caseEntity);
        await _context.SaveChangesAsync();

        return caseEntity.Id;
    }

    public async Task<IEnumerable<CaseEntity>> GetAllAsync()
    {
        return await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Status)
            .OrderByDescending(x => x.FirstCreated)
            .ToListAsync();
    }

    public async Task<CaseEntity?> GetAsync(int id)
    {
        return await _context.Cases
            .Include(x => x.User)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id);


    }

    public async Task<CaseEntity> UpdateAsync(int id, int statusId)
    {
        var caseEntity = await GetAsync(id);
        if (caseEntity == null)
            return null!;


        if (!await _context.Statuses.AnyAsync(x => x.Id == statusId))
            return null!;

        caseEntity.StatusId = statusId;
        _context.Update(caseEntity);
        await _context.SaveChangesAsync();
        return caseEntity;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Cases.AnyAsync(x => x.Id == id);
    }

}
