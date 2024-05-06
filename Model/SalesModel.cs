using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Add-Migration AddProductTable
//Update-Database
namespace ToDoExampleAndy.Model
{
    public class SalesModel
    {
        [Key]

        public int Guid { get; set; }


        [Required, ForeignKey("Product")]
        public int ProductID { get; set; }

        public virtual ProductModel Product { get; set; }


        [Required]
        public string CustomerName { get; set; }


        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }


        [DataType(DataType.Date)]
        [Required]
        public DateTime TransactionDate { get; set; }

    }
}
