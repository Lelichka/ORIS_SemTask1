using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemTask1.Attributes
{
    public class HttpPost : Attribute,HttpCustomMethod
    {
        public string MethodURI { get; set; }

        public HttpPost(string methodURI = "")
        {
            MethodURI = methodURI;
        }
    }
}