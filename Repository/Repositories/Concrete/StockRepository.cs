using DataAccess.Repositories.Abstract;
using Entity.SysModel;
using Infrastructure.DataAcccess;
using Infrastructure.Utilities.Responses;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class StockRepository : BaseRepository<Stock, long, SysContext>, IStockRepository
    {
      
    }
}
