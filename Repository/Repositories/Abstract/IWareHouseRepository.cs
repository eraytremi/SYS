﻿using Entity.SysModel;
using Infrastructure.DataAcccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IWareHouseRepository:IBaseRepository<WareHouse,int>
    {
    }
}
