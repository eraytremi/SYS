using DataAccess.Repositories.Abstract;
using Entity;
using Entity.Dtos.StockMovement;
using Entity.SysModel;
using Infrastructure.DataAcccess;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class StockMovementRepository : BaseRepository<StockMovement, long, SysContext>, IStockMovementRepository
    {
        public async Task<List<StockMovement>> ApprovedStatus()
        {
            return await GetAllAsync(p=>p.StatusType==StatusType.Onaylanan);
        }

        public async Task<List<StockMovement>> RejectedStatus()
        {
            return await GetAllAsync(p=>p.StatusType == StatusType.Reddedilmiş);
        }

        public async Task<List<StockMovement>> WaitingStatus()
        {
            return await GetAllAsync(p => p.StatusType == StatusType.Bekleyen);
        }
    }
}
