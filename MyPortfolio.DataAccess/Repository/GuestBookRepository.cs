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
    public class GuestBookRepository : Repository<GuestBook>, IGuestBookRepository
    {
        private ApplicationDbContext _context;

        public GuestBookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(GuestBook guestBook)
        {
            _context.Update(guestBook);
            //var guestBookFromDb = _context.GuestBooks.FirstOrDefault(x => x.Id == guestBook.Id);
            //if (guestBookFromDb == null)
            //{

            //    guestBookFromDb.FirstName = guestBook.FirstName;
            //    guestBookFromDb.LastName = guestBook.LastName;
            //    guestBookFromDb.Email = guestBook.Email;
            //    guestBookFromDb.AlienType = guestBook.AlienType;
            //    guestBookFromDb.GuestType = guestBook.GuestType;

            //}
        }
    }
}
