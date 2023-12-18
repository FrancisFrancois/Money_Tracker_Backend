using System.ComponentModel.DataAnnotations;

namespace Money_Tracker.API.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public int User_Id { get; set; }
        public int Home_Id { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date_Expense { get; set; }
    }

    public class ExpenseDataDTO
    {
        [Required]
        public int Category_Id { get; set; }

        [Required]
        public int User_Id { get; set; }

        [Required]
        public int Home_Id { get; set; }

        [Required]
        public double Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_Expense { get; set; }
    }
}
