// Bongani Nkosana Tshabalala | ST10446555
// Programming 2B - ICE Task 3 | Emeris Pretoria Campus | 30 September 2026
using Microsoft.EntityFrameworkCore;
using EmployeeSalaryManagement.Models;

namespace EmployeeSalaryManagement.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Salaries)
            .WithOne(s => s.Employee)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
