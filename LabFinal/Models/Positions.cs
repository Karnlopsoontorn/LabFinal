using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LabFinal.Models
{
    public class Positions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10)]
        public string position_Id { get; set; }
        public string position_Name { get; set; }
        public float baseSalary { get; set; }
        public float salaryIncreaseRate { get; set; }


    }
}
