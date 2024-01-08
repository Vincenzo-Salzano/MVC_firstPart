using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }
        [Required]
		[DisplayName("Author")]
		public string Author { get; set; }
        [Required]
        [DisplayName("List Price")]
        [Range(1, 1000, ErrorMessage = "Maximum value for List Price is 1000. The minimum is 1")]
        public double ListPrice { get; set; }

        [Required]
        [DisplayName("List Price for 50+")]
        [Range(1, 1000, ErrorMessage = "Maximum value for List Price is 1000. The minimum is 1")]
        public double ListPrice50 { get; set; }

        [Required]
        [DisplayName("List Price for 100+")]
        [Range(1, 1000, ErrorMessage = "Maximum value for List Price is 1000. The minimum is 1")]
        public double ListPrice100 { get; set; }

        // Foreign Key for relationship Category - Product
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        // Image url property. After inserting this property, remember to add it to the DB context and then migrate and update the DB
        public string ImageUrl { get; set; }
    }
}
