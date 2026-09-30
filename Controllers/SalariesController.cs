// Bongani Nkosana Tshabalala | ST10446555
// Programming 2B - ICE Task 3 | Emeris Pretoria Campus | 30 September 2026
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeSalaryManagement.Data;
using EmployeeSalaryManagement.Models;

namespace EmployeeSalaryManagement.Controllers;

public class SalariesController : Controller
{
    private readonly ApplicationDbContext _context;

    public SalariesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Salaries
    public async Task<IActionResult> Index()
    {
        var salaries = await _context.Salaries
            .Include(s => s.Employee)
            .ToListAsync();
        return View(salaries);
    }

    // GET: Salaries/Create
    public IActionResult Create()
    {
        ViewBag.Employees = new SelectList(_context.Employees, "EmployeeId", "Name");
        return View();
    }

    // POST: Salaries/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EmployeeId,SalaryMonth,Employed,HourlyRate,HoursWorked")] Salary salary)
    {
        if (ModelState.IsValid)
        {
            if (salary.HourlyRate.HasValue && salary.HoursWorked.HasValue)
            {
                salary.TotalSalary = salary.HourlyRate.Value * salary.HoursWorked.Value;
            }
            else
            {
                salary.TotalSalary = null;
            }

            _context.Add(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Employees = new SelectList(_context.Employees, "EmployeeId", "Name", salary.EmployeeId);
        return View(salary);
    }

    // GET: Salaries/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salary = await _context.Salaries.FindAsync(id);
        if (salary == null)
        {
            return NotFound();
        }
        ViewBag.Employees = new SelectList(_context.Employees, "EmployeeId", "Name", salary.EmployeeId);
        return View(salary);
    }

    // POST: Salaries/Edit/5
    // Source: Microsoft (2024) Create, Read, Update, and Delete (CRUD) with EF Core. Microsoft Learn, https://learn.microsoft.com/aspnet/core/data/ef-mvc/crud (Accessed: 30 Sep 2026).
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SalaryId,EmployeeId,SalaryMonth,Employed,HourlyRate,HoursWorked")] Salary salary)
    {
        if (id != salary.SalaryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (salary.HourlyRate.HasValue && salary.HoursWorked.HasValue)
            {
                salary.TotalSalary = salary.HourlyRate.Value * salary.HoursWorked.Value;
            }
            else
            {
                salary.TotalSalary = null;
            }

            try
            {
                _context.Update(salary);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(salary.SalaryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Employees = new SelectList(_context.Employees, "EmployeeId", "Name", salary.EmployeeId);
        return View(salary);
    }

    // GET: Salaries/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salary = await _context.Salaries
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(s => s.SalaryId == id);

        if (salary == null)
        {
            return NotFound();
        }

        return View(salary);
    }

    // POST: Salaries/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var salary = await _context.Salaries.FindAsync(id);
        if (salary != null)
        {
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool SalaryExists(int id)
    {
        return _context.Salaries.Any(e => e.SalaryId == id);
    }
}
