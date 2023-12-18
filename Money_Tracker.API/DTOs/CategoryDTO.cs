using System.ComponentModel.DataAnnotations;

namespace Money_Tracker.API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Category_Name { get; set; } = string.Empty;
    }

    public class CategoryDataDTO
    {
        [Required]
        public string Category_Name { get; set; } = string.Empty;


    }
}
