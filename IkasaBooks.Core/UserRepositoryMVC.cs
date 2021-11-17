using IkasaBooks.Models;
using IkasaBooks.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IkasaBooks.Core
{
    public class UserRepositoryMVC : IUserRepositoryMVC
    {

        public async Task<bool> AddNewUser(AddUserDTO addUser)
        {
            var userJson = JsonConvert.SerializeObject(addUser);
            var userPayload = new StringContent(userJson, Encoding.UTF8, "application/json");

            var users = new List<UserDTO>(); 
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.PostAsync("users/add-new", userPayload); 
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<UserDTO>>(result);

                    if (users.Count < 1)
                        throw new ArgumentNullException("failed to post");
                }
            }
            return true;
        }


        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = new List<UserDTO>(); 
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/api/v1/");
                var response = await client.GetAsync("users"); 
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<UserDTO>>(result);
                }
            }
            return users;
        }
        public Task<UserDTO> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public Task<UserDTO> GetUserById(string id)
        {
            throw new NotImplementedException();
        }
        public Task<UserDTO> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }


    }
}
