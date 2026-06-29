using InsightFlow.Application.DTOs;
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

    public async Task<List<Demand>> GetAllAsync(DemandFilterDto? filter = null)
    {
        var query = _context.Demands
            .AsNoTracking()
            .Include(demand => demand.Category)
            .Include(demand => demand.CreatedByUser)
            .Include(demand => demand.AssignedToUser)
            .AsQueryable();

        if (filter is not null)
        {
            if (filter.Status.HasValue)
                query = query.Where(demand => demand.Status == filter.Status.Value);

            if (filter.Priority.HasValue)
                query = query.Where(demand => demand.Priority == filter.Priority.Value);

            if (filter.CategoryId.HasValue)
                query = query.Where(demand => demand.CategoryId == filter.CategoryId.Value);

            if (filter.CreatedByUserId.HasValue)
                query = query.Where(demand => demand.CreatedByUserId == filter.CreatedByUserId.Value);

            if (filter.AssignedToUserId.HasValue)
                query = query.Where(demand => demand.AssignedToUserId == filter.AssignedToUserId.Value);

            if (filter.StartDate.HasValue)
                query = query.Where(demand => demand.CreatedAt >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(demand => demand.CreatedAt <= filter.EndDate.Value);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.ToLower();

                query = query.Where(demand =>
                    demand.Title.ToLower().Contains(search) ||
                    demand.Description.ToLower().Contains(search) ||
                    (demand.Notes != null && demand.Notes.ToLower().Contains(search))
                );
            }
        }

        return await query
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