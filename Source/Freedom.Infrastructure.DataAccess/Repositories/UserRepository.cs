using System;
using System.Linq;
using Freedom.Domain.Entities;
using Freedom.Infrastructure.DataAccess.Base;
using Freedom.Infrastructure.DataAccess.Factories;
using Freedom.Labs.Components;

namespace Freedom.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IDbContext context)
            : base(context)
        {
        }

        public override void Save(User instance)
        {

            if (string.IsNullOrEmpty(instance.Email))
            {
                throw new ArgumentNullException("Email cant be null");
            }

            if (String.IsNullOrEmpty(instance.Password))
            {
                throw new ArgumentNullException("Password cant be null");
            }
            
            base.Save(instance);
        }

        public void ResetPassword(User user, string newPassword)
        {
            user.Password = Password.CreateHashFrom(newPassword);

            Context.MarkAsModified(user);
        }
    }
}
