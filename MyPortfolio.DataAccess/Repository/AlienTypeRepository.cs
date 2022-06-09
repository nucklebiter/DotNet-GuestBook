using MyPortfolio.DataAccess.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolioWeb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.DataAccess.Repository
{
    public class AlienTypeRepository : Repository<AlienType>, IAlienTypeRepository
    {
        private ApplicationDbContext _context;

        public AlienTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(AlienType alienType)
        {
            _context.Update(alienType);
        }
    }
}
