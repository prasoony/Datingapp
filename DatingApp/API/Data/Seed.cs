using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static  class Seed
    {
        
  
        public static async Task SeedUsers(Datacontext context)
        {
            if(await context.User.AnyAsync()) return;

            var UserData= await File.ReadAllTextAsync("Data/UserSeedData.Json");
            var options= new JsonSerializerOptions{PropertyNameCaseInsensitive =true};
            var Users = JsonSerializer.Deserialize<List<AppUser>>(UserData);
            foreach (var user in Users)
            {
                using var hmac = new HMACSHA512();
                user.UserName=user.UserName.ToLower();
                user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt=hmac.Key;
                context.User.Add(user);

            }

            await context.SaveChangesAsync();
        }
    }
}