using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        public IRepository _repository { get; }

        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllAlunosAsync(true);
                return Ok(result);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }

        [HttpGet("{AlunoId}")]
        public async Task<IActionResult> GetByAlunoId(int alunoId)
        {
            try
            {
                var result = await _repository.GetAlunoAsyncById(alunoId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");

            }
        }

        [HttpGet("ByProfessor/{ProfessorId}")]
        public async Task<IActionResult> GetAlunosByProfessorId(int professorId)
        {
            try
            {
                var result = await _repository.GetAlunosAsyncByProfessorId(professorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Aluno model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/aluno", model);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
            return BadRequest();
        }

        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> Put(int alunoId, Aluno model)
        {
            try
            {
                var aluno = await _repository.GetAlunoAsyncById(alunoId, false);

                if (aluno == null) return NotFound();

                model.Id = alunoId;
                _repository.Update(model);
                if (await _repository.SaveChangesAsync())
                {
                    aluno = await _repository.GetAlunoAsyncById(alunoId, true);
                    return Created($"/api/aluno/{model.Id}", aluno);
                }

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }
            return BadRequest();
        }

        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> Delete(int alunoId)
        {
            try
            {
                var aluno = await _repository.GetAlunoAsyncById(alunoId, false);

                if (aluno == null) return NotFound();

                _repository.Delete(aluno);
                if (await _repository.SaveChangesAsync())
                {
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