using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Tools.Exception
{
    public class ValidateFailException : System.Exception
    {
        public ValidateFailException(string message) : base(message)
        {

        }
    }
}
