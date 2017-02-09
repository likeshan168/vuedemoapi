using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lks.webapi.Model
{
    public class FileResponse
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public string Data { get; set; }

        public List<string> Columns { get; set; }
    }
}