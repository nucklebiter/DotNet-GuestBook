using Microsoft.AspNetCore.Identity;
using MyPortfolioWeb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.DataAccess.DbInitializer
{

    public interface IDbInitializer
    {

        void Initialize();

    }
}
