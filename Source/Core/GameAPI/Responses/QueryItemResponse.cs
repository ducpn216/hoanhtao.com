using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.API.GameApiResponses
{
    public class QueryItemResponse
    {
        public int dwEnumID { get; set; }
        public int dwInstID { get; set; }
        public int dwCount { get; set; }
        public int dwHolder { get; set; }
        public int dwSlot { get; set; }
        public int dwLevel { get; set; }
        public int dwStrongLevel { get; set; }

        List<PropertyTypeResponse> setProp { get; set; }
    }

    public class PropertyTypeResponse
    {
        public int dwPropertyType { get; set; }
        public int dwValue { get; set; }
    }
}
