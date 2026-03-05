using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Excepcion
{
    public class EmailDuplicadoException : Exception
    {
        public EmailDuplicadoException(string message) : base(message){ }
    }

    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException(string message) : base(message) { }
    }

    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException(string message) : base(message) { }
    }
}
