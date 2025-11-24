using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Exceptions
{
    internal class NoImageException : Exception
    {
        private const string DefaultMessage = "No se ha asignado una imagen.";
        public NoImageException(string message = DefaultMessage) : base(message) { }
        public NoImageException(Exception inner, string message = DefaultMessage) : base(message, inner) { }
    }
}
