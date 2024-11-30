using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace remlbb
{
   public class secondClass : MySecondInterface
    {
        public string SendString(string s)
        {
            Thread.Sleep(5000);
            return "Second service" + "--" + s;

        }
    }
}
