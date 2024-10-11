using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Entity.Parameters
{
    public class QueryParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SearchMessage { get; set; } = "";


        public QueryParameter Next()
        {
            this.PageIndex++;
            return this;
        }

        public QueryParameter Previous()
        {
            this.PageIndex--;
            return this;
        }


        public QueryParameter SetSearchMessage()
        {
            this.PageIndex=0;
            return this;
        }

    }
}
