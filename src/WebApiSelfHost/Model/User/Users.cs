using System;

namespace WebApiSelfHost.Model.User
{
    public class Users : IRepository<AppUsersModel>
    {
        public void Create(Guid id, AppUsersModel appUsersModel)
        {
            throw new NotImplementedException();
        }
    }
}
