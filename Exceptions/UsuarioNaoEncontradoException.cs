using System;
using System.Runtime.Serialization;

namespace HackTecBanTimeSete.Exceptions
{
    [Serializable]
    internal class UsuarioNaoEncontradoException : Exception
    {
        public UsuarioNaoEncontradoException()
        {
        }

        public UsuarioNaoEncontradoException(string message) : base(message)
        {
        }

        public UsuarioNaoEncontradoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsuarioNaoEncontradoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}