using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : Controller
    {
        public IRepository _repository { get; }

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");

            }
        }

        [HttpGet("{ProfessorId}")]
        public async Task<IActionResult> Get(int professorId)
        {
            try
            {
                var result = await _repository.GetProfessorAsyncById(professorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Professor model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/professor", model);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }

            return BadRequest();
        }

        [HttpPut("{ProfessorId}")]
        public async Task<IActionResult> Put(int professorId, Professor model)
        {
            try
            {
                var prof = await _repository.GetProfessorAsyncById(professorId, false);

                if(prof == null) return NotFound();

                model.Id = professorId;
                _repository.Update(model);

                if (await _repository.SaveChangesAsync()){

                    prof = await _repository.GetProfessorAsyncById(professorId, false);
                    return Created($"/api/professor/{model.Id}", prof);
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
            return BadRequest();
        }

        [HttpDelete("{ProfessorId}")]
        public async Task<IActionResult> Delete(int professorId)
        {
            try
            {
                var prof = await _repository.GetProfessorAsyncById(professorId, false);

                if(prof == null) return NotFound();

                 _repository.Delete(prof);

                if (await _repository.SaveChangesAsync()){
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
            return BadRequest();
        }
    }
}