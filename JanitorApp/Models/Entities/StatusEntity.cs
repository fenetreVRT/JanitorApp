using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanitorApp.Models.Entities
{
    internal class StatusEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();


    }
}
