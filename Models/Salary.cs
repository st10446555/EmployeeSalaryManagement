// Bongani Nkosana Tshabalala | ST10446555
// Programming 2B - ICE Task 3 | Emeris Pretoria Campus | 30 September 2026
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSalaryManagement.Models;

public class Salary
{
    public int SalaryId { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    [DataType(DataType.Date)]
    public DateTime? SalaryMonth { get; set; }

    public bool Employed { get; set; }

    [Range(0, 10000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? HourlyRate { get; set; }

    [Range(0, 1000)]
    public int? HoursWorked { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? TotalSalary { get; set; }
}
