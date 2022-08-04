using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using my_expense_api.Components.Handlers;
using my_expense_api.Dtos.Request;
using my_expense_api.Dtos.Response;
using my_expense_api.Models;

namespace my_expense_api.Data
{
   public class AuthRepository : IAuthRepository
   {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        protected readonly IHandler _handler;
      
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public AuthRepository(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, IHandler handler)
        {
            _handler = handler;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user ==null) {
            response.Success = false;
            response.Message = "Invalid credentials";
            } else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) 
            {
            response.Success = false;
            response.Message = "Invalid credentials";
            } else 
            {
                response.Message = "Login success";
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<UserDTO>> Me()
        {
            ServiceResponse<UserDTO> serviceResponse = new ServiceResponse<UserDTO>();
            User dbUser = await _context.Users.FirstOrDefaultAsync(c=> c.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<UserDTO>(dbUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();

            if(await UserExists(user.Email)) {
            response.Success = false;
            response.Message = "Email already exists";
            return response;
            }

            _handler.Utility.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            await _context.Users.AddAsync(user);
            await _context.Categories.AddAsync(new Category{Name= "School", Limit=3200, User=user});
            await _context.Categories.AddAsync(new Category{Name= "General", Limit=3200, User=user});
            await _context.Categories.AddAsync(new Category{Name= "Work", Limit=3200, User=user});
            await _context.Categories.AddAsync(new Category{Name= "Hobby", Limit=3200, User=user});
            await _context.Categories.AddAsync(new Category{Name= "Emergency", Limit=3200, User=user});
            await _context.SaveChangesAsync();
            
            response.Data = user.Id;

            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            if(await _context.Users.AnyAsync(x=> x.Email.ToLower() == email.ToLower())) {
            return true;
            }
            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) 
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for(int i=0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != passwordHash[i]) {
                return false;
                }
            }
            return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
   }
}