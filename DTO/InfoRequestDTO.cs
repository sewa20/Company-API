using System.ComponentModel.DataAnnotations;

namespace Practice1.DTO
{
    public class InfoRequestDTO
    {
        [Required]
        public string? FullName { get; set; } = default!;
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}
