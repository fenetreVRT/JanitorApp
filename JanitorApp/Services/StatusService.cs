using JanitorApp.Context;
using JanitorApp.Models.Entities;

namespace JanitorApp.Services;

internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task InitializeAsync()
    {
        if (_context.Statuses.Any())
            return;

        var statuses = new List<StatusEntity>()
            {
                new StatusEntity { Id = 1, Name = "Ej påbörjad"},
                new StatusEntity { Id = 2, Name = "Påbörjad" },
                new StatusEntity { Id = 3, Name = "Avslutad" },
            };

        await _context.AddRangeAsync(statuses);
        await _context.SaveChangesAsync();
    }

}
