using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Encriptar
{
    public interface IEncriptacionService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
