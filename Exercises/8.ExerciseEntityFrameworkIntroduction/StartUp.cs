using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext dbContext = new SoftUniContext();
            string result = RemoveTown(dbContext);
            Console.WriteLine(result);
        }

        //Problem 03
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToArray();

            foreach (var e in employees) 
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        //Problem 04
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,                    
                    e.Salary
                })
                .ToArray();

            foreach (var e in employees)
            {
                sb
                    .AppendLine($"{e.FirstName} - {e.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        //Problem 05
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        { 
            StringBuilder sb = new StringBuilder();
            var employeesRnD = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToArray();                

            foreach (var e in employeesRnD)
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:f2}");
            }
            return sb.ToString().TrimEnd();

        }

        //Problem 06
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            Employee? employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");
            employee!.Address = newAddress;

            context.SaveChanges();

            string[] employeeAddresses = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address!.AddressText)
                .ToArray();

            return (String.Join(Environment.NewLine, employeeAddresses));
        }

        //Problem 07
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employeesWithProjects = context.Employees
                //.Where(e => e.EmployeesProjects
                    //.Any(ep => 
                        //ep.Project.StartDate.Year >= 2001 &&
                        //ep.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new 
                { 
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager!.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                        .Where(ep => ep.Project.StartDate.Year >= 2001 &&
                                     ep.Project.StartDate.Year <= 2003)
                        .Select(ep => new 
                        { 
                            ProjectName = ep.Project.Name,
                            StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                            EndDate = ep.Project.EndDate.HasValue ?
                                ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture): "not finished"
                        })
                        .ToArray()

                })
                .ToArray();

            foreach ( var e in employeesWithProjects )
            {
                sb
                    .AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                foreach (var p in e.Projects)
                {
                    sb
                        .AppendLine($"--{p.ProjectName} - {p.StartDate} - {p.EndDate}");
                }
            }
            return sb.ToString().TrimEnd();
        }
        //Problem 08
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addressesInfo = context
                    .Addresses
                    .Select(x => new
                    {
                        x.AddressText,
                        x.Town!.Name,
                        x.Employees.Count
                    })
                    .OrderByDescending(x => x.Count)
                    .ThenBy(x => x.Name)
                    .ThenBy(x => x.AddressText)
                    .Take(10);

            var result = new StringBuilder();

            foreach (var address in addressesInfo)
            {
                result.AppendLine($"{address.AddressText}, {address.Name} - {address.Count} employees");
            }

            return result.ToString().Trim();
        }
        //Problem 09
        public static string GetEmployee147(SoftUniContext context)
        {
            var employeeInfo = context
                .Employees
                .Where(x => x.EmployeeId == 147)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    Projects = x.EmployeesProjects
                      .Select(p => new { p.Project.Name })
                      .OrderBy(p => p.Name)
                      .ToList()
                })
                .First();

            var result = new StringBuilder();

            result.AppendLine($"{employeeInfo.FirstName} {employeeInfo.LastName} - {employeeInfo.JobTitle}");

            foreach (var project in employeeInfo.Projects)
            {
                result.AppendLine(project.Name);
            }

            return result.ToString().Trim();

        }
        //Problem 10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var deparmentsInfo = context
              .Departments
              .Where(x => x.Employees.Count() > 5)
              .OrderBy(x => x.Employees.Count())
              .ThenBy(x => x.Name)
              .Select(x => new
              {
                  DepartmentName = x.Name,
                  ManagerFirstName = x.Manager.FirstName,
                  ManagerLastName = x.Manager.LastName,
                  Employees = x
                    .Employees
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList()
              })
              .ToList();

            var result = new StringBuilder();

            foreach (var d in deparmentsInfo)
            {
                result.AppendLine($"{d.DepartmentName} - {d.ManagerFirstName} {d.ManagerLastName}");

                foreach (var e in d.Employees)
                {
                    result.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return result.ToString().Trim();
        }
        //Problem 11
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projectsInfo = context
                .Projects
                .OrderByDescending(u => u.StartDate).Take(10)
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    x.StartDate
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var project in projectsInfo)
            {
                result.AppendLine(project.Name);
                result.AppendLine(project.Description);
                result.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return result.ToString().Trim();
        }
        //Problem 12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            decimal raising = 1.12M;

            var employeesInfo = context
                .Employees
                .Where(x => x.Department.Name == "Engineering" ||
                x.Department.Name == "Tool Design" ||
                x.Department.Name == "Marketing" ||
                x.Department.Name == "Information Services").ToList();

            foreach (var employee in employeesInfo)
            {
                employee.Salary *= raising;
            }

            context.SaveChanges();

            var employees = context
                .Employees
                .Where(x => x.Department.Name == "Engineering" ||
                x.Department.Name == "Tool Design" ||
                x.Department.Name == "Marketing" ||
                x.Department.Name == "Information Services")
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
            }

            return result.ToString().Trim();
        }

        //Problem 13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employeesInfo = context
                .Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            var result = new StringBuilder();

            foreach (var e in employeesInfo)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
            }

            return result.ToString().Trim();
        }

        //Problem 14
        public static string DeleteProjectById(SoftUniContext context)
        {
            var deletingIdFromEmployeeProjects = context
                .EmployeesProjects
                .Where(x => x.ProjectId == 2)
                .ToList();

            context.EmployeesProjects.RemoveRange(deletingIdFromEmployeeProjects);

            var deletingIdFromProjects = context
                .Projects
                .Where(x => x.ProjectId == 2)
                .FirstOrDefault();

            context.Projects.RemoveRange(deletingIdFromProjects);

            var taking10Projects = context
                .Projects
                .Take(10)
                .Select(x => new
                {
                    x.Name
                }).ToList();

            var result = new StringBuilder();

            foreach (var p in taking10Projects)
            {
                result.AppendLine(p.Name);
            }

            return result.ToString().Trim();
        }

        //Problem 15
        public static string RemoveTown(SoftUniContext context)
        {
            var townName = "Seattle";

            var employeeInfo = context
                .Employees
                .Where(x => x.Address.Town.Name == townName)
                .ToList();

            foreach (var e in employeeInfo)
            {
                e.AddressId = null;
            }

            var addressesCount = context
                .Addresses
                .Where(x => x.Town.Name == townName)
                .Count();

            var addressesRemove = context
                .Addresses
                .Where(x => x.Town.Name == townName)
                .ToList();

            context.Addresses.RemoveRange(addressesRemove);

            var townRemove = context
               .Towns
               .Where(x => x.Name == townName)
               .ToList();

            context.Towns.RemoveRange(townRemove);

            context.SaveChanges();

            return $"{addressesCount} addresses in {townName} were deleted";
        }



    }
}