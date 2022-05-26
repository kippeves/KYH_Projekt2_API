using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.Data;

public class Project
{
    public int Id { get; set; }
    [Required]
    public Customer Customer { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public List<TimeRegistration> TimeRegistrations { get; set; } = new();
    public bool IsActive { get; set; } = true;
}