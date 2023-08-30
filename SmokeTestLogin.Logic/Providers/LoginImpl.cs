﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SmokeTestLogin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmokeTestLogin.Data.Utils;
using SmokeTestLogin.Logic.Interfaces;
using SmokeTestLogin.Logic.Models;

namespace SmokeTestLogin.Logic.Providers
{
    public class LoginImpl : ILoginService
    {
        private readonly IHttpContextAccessor _http;
        private readonly ILogger<LoginImpl> _logger;
        private readonly MainContext _context;

        public LoginImpl(IHttpContextAccessor http, ILogger<LoginImpl> logger, MainContext context)
        {
            _http = http;
            _logger = logger;
            _context = context;
        }

        public async Task<bool> LoginAsync(UserModel model)
        {
            try
            {
                var userLog = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
                if (userLog is null) return false;
                if (!await SecretHasher.VerifyAsync(model.Password, userLog.Password)) return false;
                await _http.HttpContext!.AuthenticateAsync();
                var claimUserName = new Claim(ClaimTypes.NameIdentifier, userLog.UserName);
                var claimFullName = new Claim(ClaimTypes.Name, userLog.Name);
                var claimId = new Claim(ClaimTypes.Sid, userLog.Id.ToString());
                var identity = new ClaimsIdentity(new[] { claimUserName, claimFullName, claimId },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var user = new ClaimsPrincipal(identity);
                await _http.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Some thing wrong {}", ex.Message);
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _http.HttpContext!.SignOutAsync();
        }
    }
}