using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Readers
{
    class FileMapper
    {
        public virtual string MapFile(string fileName)
        {
            return System.Web.HttpContext.Current.Server.MapPath(fileName);
        }
    }
}
