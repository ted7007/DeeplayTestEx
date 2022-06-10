using Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CompanyRepository
    {
        private CompanyContext db;

        public CompanyRepository(CompanyContext context)
        {
            this.db = context;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return db.Companies
                     .Include(c => c.Workers)
                        .ThenInclude(w => w.Post)
                    .ToList();
        }

        public ObservableCollection<Company> GetAllCompaniesO()
        {
            return new ObservableCollection<Company>(db.Companies.Include(c => c.Workers)
                                                                 .ThenInclude(w => w.Post));
        }

        public BindingList<Worker> GetAllWorkers()
        {
             db.Workers
                             .Include(w => w.Post)
                             .Include(w => w.UI)
                             .Include(w => w.Company)
                             .Load();
            return db.Workers.Local.ToBindingList();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return db.Posts
                     .Include(w => w.Workers)
                     .ToList();
        }

        public Post GetPost (Func<Post, bool> predicate)
        {
            return db.Posts.Include(p => p.Workers).ToList().FirstOrDefault(predicate);
        }

        public Company GetCompany(Func<Company, bool> predicate)
        {
            return db.Companies
                     .Include(c => c.Workers)
                        .ThenInclude(w => w.Post)
                    .ToList().FirstOrDefault(predicate);
        }

        public Company AddCompany(Company post)
        {
            var result = db.Companies.Add(post).Entity;
            db.SaveChanges();
            return result;
        }

        public Worker AddWorker(Worker worker)
        {
            var result = db.Workers.Add(worker).Entity;
            db.SaveChanges();
            return result;
        }

        public UniqueInfo AddUniqueInfo(UniqueInfo info)
        {
            var result = db.UniqueInfos.Add(info);
            db.SaveChanges();
            return result.Entity;
        }

        public void UpdateCompany(Company post)
        {
            db.Companies.Update(post);
            db.SaveChanges();
        }

        public bool AnyCompanies(Func<Company, bool> func)
        {
            return db.Companies.Any(func);
        }

        public Company FindCompany(int? id)
        {
            return db.Companies.Include(c => c.Workers)
                               .ToList()
                               .Find(c => c.Id == id);
        }

        public Worker FindWorker(int? id)
        {
            return db.Workers.Include(w => w.Post)
                             .Include(w => w.UI)
                             .Include(w => w.Company)
                             .ToList()
                             .Find(w => w.Id == id);
        }

        public Post FirstPost(Func<Post, bool> predicate)
        {
            return db.Posts.Include(w => w.Workers).First(predicate);
                           
        }


        public void DeleteCompany(int id)
        {
            db.Companies.Remove(FindCompany(id));
            db.SaveChanges();
        }

        public void LoadWorkers()
        {
            db.Workers.Include(w => w.Post)
                             .Include(w => w.UI)
                             .Include(w => w.Company)
                             .Include(w => w.UI)
                             .Load();
        }

        public void UpdateWorker(Worker worker)
        {
            db.Workers.Update(worker);
            db.SaveChanges();

        }

        public void RemoveWorker(Worker worker)
        {
            db.Workers.Remove(worker);
            db.SaveChanges();
        }

        public void RemoveUI(UniqueInfo UI)
        {
            db.UniqueInfos.Remove(UI);
            db.SaveChanges();
        }
    }
}
