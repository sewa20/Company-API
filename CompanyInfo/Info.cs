using System.ComponentModel.DataAnnotations;

namespace Practice1.CompanyInfo
{
    public class Info
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Address { get; set; }

    }
}
