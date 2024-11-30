using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace remlbb
{
    [ServiceContract]
    public interface MySecondInterface
    {
        [OperationContract]
        string SendString(string s);
    }
}
