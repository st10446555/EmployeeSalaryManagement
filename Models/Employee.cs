// Bongani Nkosana Tshabalala | ST10446555
// Programming 2B - ICE Task 3 | Emeris Pretoria Campus | 30 September 2026
using System.ComponentModel.DataAnnotations;

namespace EmployeeSalaryManagement.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Surname { get; set; } = string.Empty;

    [StringLength(150)]
    public string? Address { get; set; }

    public ICollection<Salary>? Salaries { get; set; }
}
