using System.ComponentModel.DataAnnotations;

namespace APIDemoApp.Models
{
    public class OrderUpdateModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "The Id should be greater than zero")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Ordered By is required")]
        [MaxLength(20, ErrorMessage = "The Ordered By should be less than 20 characters")]
        [MinLength(3, ErrorMessage = "The Ordered By should have at least 3 characters")]
        public string NewName { get; set; }
    }
}
