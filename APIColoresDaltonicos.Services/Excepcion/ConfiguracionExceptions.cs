using System;
using System.Collections.Generic;
using System.Text;

namespace APIColoresDaltonicos.Services.Excepcion
{
    public class ConfiguracionNoEncontradaException : Exception
    {
        public ConfiguracionNoEncontradaException(string message) : base(message) { }
    }

    public class  TipoDaltonismoInvalidoException : Exception
    {
        public TipoDaltonismoInvalidoException(string message) : base(message) { }
    }

    public class UsuarioSinConfiguracionException : Exception
    {
        public UsuarioSinConfiguracionException(string message) : base(message) { }
    }
}
