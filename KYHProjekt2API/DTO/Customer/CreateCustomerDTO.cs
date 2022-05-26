using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.Customer;

public class CreateCustomerDTO
{
    [Required(ErrorMessage = "Kunden behöver ett namn")]
    public string? Name { get; set; }
}