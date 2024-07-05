using DataAccess.Repositories.Abstract;
using Entity;
using Infrastructure.DataAcccess;
using Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class UserRepository:BaseRepository<User,long,SysContext>,IUserRepository
    {
    }
}
