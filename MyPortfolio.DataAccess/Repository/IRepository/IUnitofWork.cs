using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {

        IGuestTypeRepository GuestType { get; }
        IAlienTypeRepository AlienType { get; }
        IGuestBookRepository GuestBook { get; }
        IApplicationUserRepository ApplicationUser { get; }

        void Save();

    }
}
