﻿using Entity.Dtos.StockMovement;
using Entity.Dtos.StockStatus;
using Infrastructure.Utilities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStockMovementService
    {
        Task<ApiResponse<List<GetStockMovement>>> GetAllAsync(int currentUserId);
        Task<ApiResponse<NoData>> AddAsync(PostStockMovement postStockMovement, int currentUserId);
        Task<ApiResponse<NoData>> DeleteAsync(int id, int currentUserId);
        Task<ApiResponse<NoData>> UpdateAsync(UpdateStockMovement updateStockMovement, int currentUserId);
    }
}