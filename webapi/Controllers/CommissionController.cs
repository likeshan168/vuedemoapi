using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using lks.webapi.BLL;
using lks.webapi.Extension;
using lks.webapi.Model;
using lks.webapi.Utility;

namespace webapi.Controllers
{
    [RoutePrefix("api/commission")]
    public class CommissionController : ApiController
    {
        private readonly FileService _bllService = new FileService();

        [Route("upload"), HttpPost]
        public async Task<FileResponse> Upload()
        {
            return await _bllService.GetUploadFileData(Request);
        }

        public FileResponse SaveDataToDb(IList<Commission> commissions)
        {
            FileResponse response = new FileResponse();
            response.Msg = "test";
            response.Code = 500;
            return response;
        }
        
        [HttpGet]
        //[Authorize]
        public FileResponse Test()
        {
            FileResponse response = new FileResponse();
            response.Msg = "test";
            response.Code = 500;
            return response;
        }
    }
}
