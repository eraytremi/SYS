using DataAccess.Repositories.Abstract;
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
    public class DemandRepository:BaseRepository<Demand,int,SysContext>,IDemandRepository
    {
    }
}
