using Entity.Dtos.StockMovement;
using Entity.SysModel;
using Infrastructure.DataAcccess;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IStockMovementRepository:IBaseRepository<StockMovement,long>
    {
       Task<List<StockMovement>> RejectedStatus();
        Task<List<StockMovement>> ApprovedStatus();
        Task<List<StockMovement>> WaitingStatus();


    }
}
