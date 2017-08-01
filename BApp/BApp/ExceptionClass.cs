using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BApp
{
   public class ExceptionClass:Exception
    {
       public ExceptionClass(string s):base("Error: " + s + " does not contain letters")
        {

        }
    }
}
