using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using lks.webapi.ActionFilters;
using lks.webapi.BLL;
using lks.webapi.Model;

namespace webapi.Controllers
{
    [RoutePrefix("api/commission")]
    public class CommissionController : ApiController
    {
        private readonly FileService _bllService = new FileService();
        private readonly CommissionService _comService = new CommissionService();

        [Route("upload"), HttpPost]
        public async Task<FileResponse> Upload()
        {
            return await _bllService.GetUploadFileData(Request);
        }
        [Route("SaveData"), HttpPost]
        public FileResponse SaveDataToDb(FileDataRequest fileData)
        {
            FileResponse response = new FileResponse();

            try
            {
                _comService.AddAndUpdate(fileData.Commissions, fileData.ColumnCount);
                response.Msg = "保存成功";
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.Msg = ex.Message;
                response.Code = 500;
            }

            return response;
        }
        [HttpPost, DeflateCompression]
        public dynamic GetCommissions(PagePara para)
        {
            int total = 0;
            return new
            {
                Commissions = _comService.QueryList(para.Columns, para.Index, para.Size, para.WhereStr, para.OrderField, out total),
                Total = total
            };
        }
        [Route("DeleteCommission"), HttpPost]
        public dynamic Delete(IList<Commission> commission)
        {
            return _comService.BatchDelete(commission);
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
