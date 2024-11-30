using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace remlbb
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    public class implementclass : myinterface
    {
        public string Upload(string s)
        {
            Thread.Sleep(5000);
            return "hello" + "--" + s;

        }
    }
}




