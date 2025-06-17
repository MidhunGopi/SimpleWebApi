using Microsoft.AspNetCore.Mvc;
using Business;
using DataAccess;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto dto)
        {
            // Example: Save the file and get its path or content
            string? photoPath = null;
            if (dto.ProfilePhoto != null)
            {
                var uploads = Path.Combine("uploads", dto.ProfilePhoto.FileName);
                using (var stream = new FileStream(uploads, FileMode.Create))
                {
                    await dto.ProfilePhoto.CopyToAsync(stream);
                }
                photoPath = uploads;
            }

            var employee = new Employee
            {
                EmpName = dto.EmpName,
                Gender = dto.Gender,
                ProfilePhoto = photoPath // Save the path or base64 string as needed
            };

            var created = await _service.CreateAsync(employee);
            return Ok(dto);
            // return CreatedAtAction(nameof(GetById), new { id = created.EmpId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            var updated = await _service.UpdateAsync(id, employee);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
