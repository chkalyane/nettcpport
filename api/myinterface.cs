using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace remlbb
{
    [ServiceContract]
    public interface myinterface
    {
        [OperationContract]

        string Upload(string s);

    }
  
}
