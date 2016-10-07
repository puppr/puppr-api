using Microsoft.AspNet.Identity;
using Puppr.API.Infrastructure;
using Puppr.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Puppr.API.Controllers
{
    public class RegisterController : ApiController
    {
        private AuthRepository _authRepository = new AuthRepository();

        // POST: api/register
        public async Task<IHttpActionResult> Register(RegistrationModel registration)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authRepository.RegisterUser(registration);

            var errorResult = GetErrorResult(result);

            if(errorResult != null)
            {
                return errorResult;
            }
            var owner = await _authRepository.FindUser(registration.Username, registration.Password);
            return Ok(owner.Id);
        }


        protected override void Dispose(bool disposing)
        {
            _authRepository.Dispose();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
