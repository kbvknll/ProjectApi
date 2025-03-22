using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.DTO;
using ProjectApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // Получение всех проектов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.TaskEmployees)
                        .ThenInclude(te => te.Employee) 
                .ToListAsync();

            var projectDtos = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Cost = p.Cost,
                Tasks = p.Tasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Employees = t.TaskEmployees.Select(te => new EmployeeDto
                    {
                        Id = te.Employee.Id,
                        FirstName = te.Employee.FirstName,
                        LastName = te.Employee.LastName,
                        UserId = te.Employee.UserId
                    }).ToList()
                }).ToList(),
                Employees = p.Tasks
                    .SelectMany(t => t.TaskEmployees.Select(te => te.Employee))
                    .Distinct()
                    .Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        UserId = e.UserId
                    }).ToList()
            }).ToList();

            return projectDtos;
        }

        // Получение проекта по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.TaskEmployees)
                        .ThenInclude(te => te.Employee) 
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return NotFound();

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Cost = project.Cost,
                Tasks = project.Tasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Employees = t.TaskEmployees.Select(te => new EmployeeDto
                    {
                        Id = te.Employee.Id,
                        FirstName = te.Employee.FirstName,
                        LastName = te.Employee.LastName,
                        UserId = te.Employee.UserId
                    }).ToList()
                }).ToList(),
                Employees = project.Tasks
                    .SelectMany(t => t.TaskEmployees.Select(te => te.Employee))
                    .Distinct()
                    .Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        UserId = e.UserId
                    }).ToList()
            };

            return projectDto;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Id) return BadRequest();
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}