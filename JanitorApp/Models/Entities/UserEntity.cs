using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JanitorApp.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
internal class UserEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "varchar(320)")]
    public string Email { get; set; } = null!;
    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; } = null!;



    public ICollection<CaseEntity> Cases { get; set; } = new HashSet<CaseEntity>();

}
