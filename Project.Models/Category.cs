using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Project.Models
{

    // This is a class created in Models folder, which represents the Category table for the database
    public class Category
    {
        // The following properties represent the columns of the Category table
        [Key]  // This data annotation is not mandatory, because Entity Frameworks will automatically set the prop Id as PK (thanks to the name Id).
        public int Id { get; set; }
        [Required] // Data annotation for mandatory data insert in the Name column
        [DisplayName("Category Name")]
        [MaxLength(30)] // Data annotation for max word lenght.       
        public string Name { get; set; }

        [DisplayName("Display Order")] // Data annotation for the property "DisplayOrder" setted in the tag helper and shown on the UI 
        [Range(1, 100, ErrorMessage = "Maximum value for Display Order is 100. The minimum is 1")]  // Data annotation for min-Max int range. We set up also the error message to show with tag helper asp-validation-for=""    
        public int DisplayOrder { get; set; }
    }
}
