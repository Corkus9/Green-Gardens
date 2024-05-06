using System.ComponentModel.DataAnnotations;
//Add-Migration AddProductTable
//Update-Database
namespace ToDoExampleAndy.Model
{
    public class ProductModel
    {
        [Key] 
        public int Guid { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
