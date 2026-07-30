using MongoDB.Driver;
using TaskManager.Dtos;
using TaskManager.InterFace;
using TaskManager.Utils;
using TodoList.Config;
using TodoList.Model;

namespace TaskManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMongoCollection<UserModel> _user;
        public AuthService(DatabaseSettings _settings)
        {
            var client  = new MongoClient(_settings.CollectionString);
            var database = client.GetDatabase(_settings.DataBaseName);
            _user = database.GetCollection<UserModel>(_settings.UserCollectionName);
        }

        public async Task<string> Login(LoginDto dto)
        {
            string token;
            var existUser = await _user.Find(x=>x.Email == dto.Email).FirstOrDefaultAsync();
            if (existUser == null) {
                throw new Exception("User Not Found please register");
            }
            bool cheakPassword = BCrypt.Net.BCrypt.Verify(dto.Password, existUser.PasswordHash);
            if (!cheakPassword)
            {
                throw new Exception("Wrong password");
            }

            else
            {
                token = TokenGenerator.AccessToken(existUser);

            }
            return token;
        }

        public async Task Register(RegisterDto dto)
        {
            var existUser = await _user.Find(x => x.Email == dto.Email).FirstOrDefaultAsync();
            if (existUser != null)
            {
                throw new Exception("user Exists");
            }
            var user = new UserModel { 
                Name=dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UserRole = dto.UserRole,
            };
            await _user.InsertOneAsync(user);

        }
    }
}
