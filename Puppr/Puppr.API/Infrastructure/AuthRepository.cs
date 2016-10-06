using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Puppr.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Puppr.API.Infrastructure
{
    public class AuthRepository : IDisposable
    {
        private PupprDataContext _db;
        private UserManager<Owner> _userManager;

        public AuthRepository()
        {
            _db = new PupprDataContext();
            _userManager = new UserManager<Owner>(
                new UserStore<Owner>(
                    _db   
                )
            );
        }

        // Register User
        public async Task<IdentityResult> RegisterUser(RegistrationModel userModel)
        {
            Owner owner = new Owner
            {
                UserName = userModel.Username,
                Biography = userModel.Biography
            };

            var result = await _userManager.CreateAsync(owner, userModel.Password);

            return result;
        }

        // Find User
        public async Task<Owner> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _db.Dispose();
            _userManager.Dispose();
        }
    }
}