using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_Razor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required] // Data annotation for mandatory data insert in the Name column
        [DisplayName("Category Name")]
        [MaxLength(30)] // Data annotation for max word lenght.       
        public string Name { get; set; }

        [DisplayName("Display Order")] // Data annotation for the property "DisplayOrder" setted in the tag helper asp-for="" and shown on the UI 
        [Range(1, 100, ErrorMessage = "Maximum value for Display Order is 100. The minimum is 1")]
        public int DisplayOrder { get; set; }
    }
}
