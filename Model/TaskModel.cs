using System.ComponentModel.DataAnnotations;
namespace ToDoExampleAndy.Model
{
    public class TaskModel
    {
        [Key]  // Data annotation specifying this property as the primary key in the database.
        public int Id { get; set; } // Property for the task's unique identifier.

        [Required] // Data annotation indicating that this property is mandatory.
        [StringLength(100)] // Restrict the string length to 100 characters.
        public string Description { get; set; } // Property for the task's description.

        [Required] // Data annotation indicating that this property is mandatory.
        public bool Completed { get; set; } // Property to track if the task is completed.

        [DataType(DataType.Date)] // Specifies that only the date portion should be considered, without time.
        [Required] // Data annotation indicating that this property is mandatory.
        public DateTime DueDate { get; set; } // Property for the task's due date.
    }
}
