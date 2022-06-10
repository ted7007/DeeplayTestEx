using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class CompanyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UniqueInfo> UniqueInfos { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new Post[]
                {
                new Post { Id=1, Name="Director", Workers = new List<Worker>(), NameUniqueInformation = "Company Name"},
                new Post { Id=2, Name="Worker", Workers = new List<Worker>(), NameUniqueInformation = "Company Name"},
                new Post { Id=3, Name="Intern", Workers = new List<Worker>()}
                });

            modelBuilder.Entity<Company>().HasData(
                new Company[]
                {
                    new Company { Id=1, Name="Deeplay" },
                    new Company { Id=2, Name="GameForest"}
                });
            modelBuilder.Entity<Worker>().HasData(
                new Worker[]
                {
                    new Worker { Name = "Dmitrii Chichuk", Id=1, Birthday=DateTime.MinValue.AddDays(1), CompanyId=1, PostId=1, Sex=Sex.Male, UIId=1},
                    new Worker { Name = "Second Boy", Id=2, Birthday=DateTime.MinValue.AddYears(1), CompanyId=2, PostId=1, Sex=Sex.Male, UIId=2},
                });

            modelBuilder.Entity<Worker>().HasOne(w => w.UI)
                                         .WithOne(w => w.Worker)
                                         .HasForeignKey<UniqueInfo>(i => i.WorkerId);
            modelBuilder.Entity<UniqueInfo>().HasData(
                new UniqueInfo[]
                {
                    new UniqueInfo { Id = 1, WorkerId=1, Text="Deeplay"},
                    new UniqueInfo { Id = 2, WorkerId=2, Text="GameForest"}
                });
        }
    }
}
