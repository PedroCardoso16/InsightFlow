using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Repositories;

public class DemandRepository : IDemandRepository
{
    private readonly AppDbContext _context;

    public DemandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Demand>> GetAllAsync()
    {
        return await _context.Demands
            .AsNoTracking()
            .Include(demand => demand.Category)
            .Include(demand => demand.CreatedByUser)
            .Include(demand => demand.AssignedToUser)
            .OrderByDescending(demand => demand.CreatedAt)
            .ToListAsync();
    }

    public async Task<Demand?> GetByIdAsync(Guid id)
    {
        return await _context.Demands
            .Include(demand => demand.Category)
            .Include(demand => demand.CreatedByUser)
            .Include(demand => demand.AssignedToUser)
            .FirstOrDefaultAsync(demand => demand.Id == id);
    }

    public async Task<Demand> CreateAsync(Demand demand)
    {
        await _context.Demands.AddAsync(demand);
        await _context.SaveChangesAsync();

        return demand;
    }

    public async Task<Demand?> UpdateAsync(Demand demand)
    {
        _context.Demands.Update(demand);
        await _context.SaveChangesAsync();

        return demand;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var demand = await _context.Demands
            .FirstOrDefaultAsync(demand => demand.Id == id);

        if (demand is null)
            return false;

        _context.Demands.Remove(demand);
        await _context.SaveChangesAsync();

        return true;
    }
}