using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.Customer;

public class CreateCustomerDTO
{
    [Required(ErrorMessage = "Du glömde skicka in ett namn")]
    public string? Name { get; set; }
}