using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>()
                        .HasData(
                            new List<Professor>{
                                new Professor{
                                    Id = 1,
                                    Nome = "Adelson"
                                },
                                new Professor{
                                    Id = 2,
                                    Nome = "Rose"
                                },
                                new Professor{
                                    Id = 3,
                                    Nome = "Cristiano"
                                },
                            }
                        );

            modelBuilder.Entity<Aluno>()
            .HasData(
                new List<Aluno>{
                    new Aluno{
                        Id = 1,
                        Nome = "Bill",
                        Sobrenome = "Klinton",
                        DataNasc = "29/01/1997",
                        ProfessorId = 1
                    },
                    new Aluno{
                        Id = 2,
                        Nome = "João",
                        Sobrenome = "Almeida",
                        DataNasc = "20/01/1990",
                        ProfessorId = 2
                    },
                    new Aluno{
                        Id = 3,
                        Nome = "Pedro",
                        Sobrenome = "Augusto",
                        DataNasc = "12/04/1997",
                        ProfessorId = 3
                    },
                }
            );
        }
    }
}