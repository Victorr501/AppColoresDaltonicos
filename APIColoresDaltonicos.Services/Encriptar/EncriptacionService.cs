using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace APIColoresDaltonicos.Services.Encriptar
{
    public class EncriptacionService : IEncriptacionService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
