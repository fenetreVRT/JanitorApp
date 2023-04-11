namespace JanitorApp.Models.Entities
{
    internal class CaseEntity
    {

        public int Id { get; set; }
        public DateTime FirstCreated { get; set; } = DateTime.Now;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public int StatusId { get; set; } = 1;
        public StatusEntity Status { get; set; } = null!;

    }
}
