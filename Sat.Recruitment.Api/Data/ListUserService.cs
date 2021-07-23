using Sat.Recruitment.Api.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Api
{
    public class ListUserService : IListUserService
    {
        public List<User> Users { get; set; }
        
        public ListUserService() {
            Users=new List<User>();
            var reader = ReadUsersFromFile();
            
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                Users.Add(user);
            }
            reader.Close();
        }

        public Result Add(User newUser)
        {
            Result result = new Result();
            if (!this.IsDuplicated(newUser))
            {
                this.Users.Add(newUser);
                result.IsSuccess = true;
                result.Errors = "";
            }
            else
            {
                result.IsSuccess = false;
                result.Errors = "The user is duplicated";

            }
            return result;
        }
        private bool IsDuplicated(User newUser)
        {
            bool resp = false;
            
            if (this.Users.Any(u => (u.Email == newUser.Email || u.Phone == newUser.Phone) || (u.Name == newUser.Name && u.Address == newUser.Address) ))
            {
                resp = true;
            }
            return resp;
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}