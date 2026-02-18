namespace mission6.Models;
using System.ComponentModel.DataAnnotations;


public class Movie
{
    public int MovieId { get; set; }

    [Required]
    public int MovieCategoryId { get; set; }
    public MovieCategory? MovieCategory { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    // Supports YYYY or YYYY-YYYY (like 2001-2002)
    [Required]
    [StringLength(20)]
    [RegularExpression(@"^\d{4}(\s*-\s*\d{4})?$", ErrorMessage = "Year must be YYYY or YYYY-YYYY.")]
    public string Year { get; set; } = string.Empty;

    [Required]
    public string Director { get; set; } = string.Empty;

    [Required]
    public Rating Rating { get; set; }

    // Optional per prompt
    public bool? Edited { get; set; }

    // Optional per prompt
    public string? LentTo { get; set; }

    // Optional per prompt, max 25 chars
    [StringLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
    public string? Notes { get; set; }
}