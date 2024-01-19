using Fitness.Entities;
using Fitness.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Fitness.DataAccess
{
    public class AuthRepository : IAuthRepository
    {
        private readonly FitnessDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(FitnessDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "Kullanıcı Bulunamadı";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Hatalı Kullanıcı Adı Ya Da Şifre";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }


        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            ServiceResponse<int> response = new ServiceResponse<int>();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
        public async Task<ServiceResponse<string>> ForgotPassword(string username)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "Kullanıcı Bulunamadı";
                return response;
            }

            // Yeni rastgele şifre oluştur
            string newPassword = GenerateRandomPassword();

            // Şifreyi kullanıcıya atayarak veritabanında güncelle
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Veritabanını güncelle
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // E-posta gönder
            SendPasswordResetEmail(user, newPassword);

            response.Data = "Şifre sıfırlama e-postası gönderildi.";
            return response;
        }
        // Diğer metotlar...

        private string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            return password.ToString();
        }

        private void SendPasswordResetEmail(User user, string newPassword)
        {
            // E-posta servisi kullanarak e-posta gönderme işlemi
            // Bu örnek sadece bir SMTP servisi kullanımını göstermektedir.

            // SMTP ayarları
            string smtpHost = "your-smtp-host.com";
            int smtpPort = 587;
            string smtpUsername = "your-email@example.com";
            string smtpPassword = "your-email-password";

            // E-posta mesajı
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = "Şifre Sıfırlama",
                Body = $"Merhaba {user.UserName},\n\nYeni şifreniz: {newPassword}\n\nBu şifre ile giriş yapabilir ve ardından şifrenizi değiştirebilirsiniz.",
                IsBodyHtml = false
            };

            mailMessage.To.Add(user.Email);

            // SMTP client oluştur
            SmtpClient smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            // E-postayı gönder
            smtpClient.Send(mailMessage);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
