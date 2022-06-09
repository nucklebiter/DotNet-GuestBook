using MyPortfolio.DataAccess.Repository.IRepository;
using MyPortfolioWeb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {

        private ApplicationDbContext _context;

        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            GuestType = new GuestTypeRepository(_context);
            AlienType = new AlienTypeRepository(_context);
            GuestBook = new GuestBookRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
        }

        public IGuestTypeRepository GuestType { get; private set; }
        public IAlienTypeRepository AlienType { get; private set; }
        public IGuestBookRepository GuestBook { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
