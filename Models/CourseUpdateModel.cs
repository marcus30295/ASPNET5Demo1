using System.ComponentModel.DataAnnotations;

namespace ASPNET5Demo1.Models
{
    public class CourseUpdateModel
    {
        [Required]
        [StringLength(30,ErrorMessage = "Title 太長")]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }
    }
}