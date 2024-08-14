using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Common.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(string name) : base($"User \"{name}\" not found.") { }
   
    }
}
