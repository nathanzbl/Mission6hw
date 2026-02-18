namespace mission6.Models;
using System.ComponentModel.DataAnnotations;


public class MovieCategory
{
    public int MovieCategoryId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}