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
    public class GuestTypeRepository : Repository<GuestType>, IGuestTypeRepository
    {
        private ApplicationDbContext _context;

        public GuestTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(GuestType guestType)
        {
            _context.Update(guestType);
        }
    }
}
