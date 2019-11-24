using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }


        //Alunos
        public async Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
                query = query.Include(
                    aluno => aluno.Professor
                );
            
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Aluno[]> GetAlunosAsyncByProfessorId(int professorId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
                query = query.Include(
                    aluno => aluno.Professor
                );
            
            query = query.AsNoTracking().Where(aluno => aluno.ProfessorId == professorId).OrderBy(aluno => aluno.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunoAsyncById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor)
                query = query.Include(
                    aluno => aluno.Professor
                );
            
            query = query.AsNoTracking().Where(aluno => aluno.Id == alunoId).OrderBy(aluno => aluno.Id);

            return await query.FirstOrDefaultAsync();
        }

        //Professores
        public async Task<Professor[]> GetAllProfessoresAsync(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAluno)
                query.Include(
                    professor => professor.Alunos
                );

            query = query.AsNoTracking().OrderBy(professor => professor.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetProfessorAsyncById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAluno)
                query.Include(
                    professor => professor.Alunos
                );

            query = query.AsNoTracking().Where(professor => professor.Id == professorId).OrderBy(professor => professor.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}