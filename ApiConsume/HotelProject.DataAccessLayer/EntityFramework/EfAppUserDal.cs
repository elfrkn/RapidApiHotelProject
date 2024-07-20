using HotelProject.DataAccessLayer.Abstract;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.DataAccessLayer.Repositories;
using HotelProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DataAccessLayer.EntityFramework
{
    public class EfAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        public EfAppUserDal(Context context) : base(context)
        {

        }

        public int AppUserCount()
        {
            throw new NotImplementedException();
        }

        public List<AppUser> UserListWithWorkLocation()
        {
            throw new NotImplementedException();
        }

        public List<AppUser> UsersListWithWorkLocations()
        {
            throw new NotImplementedException();
        }
    }
}
