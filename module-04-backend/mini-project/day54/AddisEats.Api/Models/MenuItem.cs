using System.ComponentModel.DataAnnotations;

namespace AddisEats.Api.Models;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "Price must be greater than zero ETB.")]
    public decimal Price { get; set; }
}
