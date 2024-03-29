﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using BimApp.API.Data;
using BimApp.API.Dtos;
using BimApp.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BimApp.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserForRegisterDto userForRegistrationDto)
        {
            // validate user request

            userForRegistrationDto.Username = userForRegistrationDto.Username.ToLower();

            if (await _repo.UserExists(userForRegistrationDto.Username))
            {
                return BadRequest("User-name already exists");
            }

            var userToCreate = new AppUser
            {
                UserName = userForRegistrationDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegistrationDto.Password);

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDecryptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDecryptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}
