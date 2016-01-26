using System;

namespace WebApiSelfHost.Model.User
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
