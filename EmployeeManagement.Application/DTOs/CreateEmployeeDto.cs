using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs;

public class CreateEmployeeDto
{
    [Required]
    [MaxLength(20)]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Designation { get; set; } = string.Empty;

    [Required]
    public int DepartmentId { get; set; }
    public string? ProfileImage { get; set; }
}

