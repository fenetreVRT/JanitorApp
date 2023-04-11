

namespace JanitorApp.Models;

internal class CaseRegistration
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Guid UserId { get; set; }
}
