using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.DataAccess.Repository.IRepository
{
    public interface IAlienTypeRepository : IRepository<AlienType>
    {

        void Update(AlienType alienType);
        

    }
}
