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
            FileResponse response = new FileResponse();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    response.Msg = "上传格式不是multipart/form-data";
                    response.Code = (int)HttpStatusCode.UnsupportedMediaType;
                    return response;
                }
                string root = HttpContext.Current.Server.MapPath("/Uploads/");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var f in provider.FileData)
                {
                    string orfilename = f.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(f.LocalFileName);
                    if (fileinfo.Length <= 0)
                    {
                        response.Msg = "请选择上传文件";
                        response.Code = 301;
                        return response;
                    }
                    else
                    {
                        string fileExt = Path.GetExtension(orfilename);
                        fileinfo.CopyTo(Path.Combine(root, orfilename), true);
                        using (ExcelHelper excelHelper = new ExcelHelper(fileinfo.OpenRead(), fileExt))
                        {
                            DataTable dt = excelHelper.ExcelToDataTable(string.Empty, true);

                            if (dt == null)
                            {
                                response.Msg = "文件上传失败";
                                response.Code = 500;
                                fileinfo.Delete();
                                return response;
                            }

                            #region 将上传文件的记录保存到数据库
                            //lks.webapi.Model.File file = new lks.webapi.Model.File()
                            //{
                            //    Id = Guid.NewGuid(),
                            //    FileName = f.LocalFileName,
                            //    FileSize = fileinfo.Length,
                            //    ImportToDb = false,
                            //    //UploadBy = HttpContext.Current.Session["CurrentUser"].ToString(),
                            //    UploadDate = DateTime.Now
                            //};
                            //_bllService.Add(file); 
                            #endregion

                            response.Msg = "文件上传成功";
                            response.Code = 200;
                            response.Data = dt.ToJson();
                            //response.Columns = dt.Columns
                            //数据保存到数据库再删除
                            //fileinfo.Delete();
                        }
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                response.Msg = ex.Message;
                response.Code = 500;
                return response;
            }
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
