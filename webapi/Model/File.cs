using System;
namespace lks.webapi.Model
{
    //File
    public class File
    {
        /// <summary>
        /// 主键
        /// </summary>		
        public Guid Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>		
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小，单位kb
        /// </summary>		
        public long FileSize { get; set; }
        /// <summary>
        /// 上传日期
        /// </summary>		
        public DateTime UploadDate { get; set; }
        /// <summary>
        /// 上传的用户名
        /// </summary>		
        public string UploadBy { get; set; }
        /// <summary>
        /// ImportToDb
        /// </summary>		
        public bool ImportToDb { get; set; }
        /// <summary>
        /// Remark
        /// </summary>		
        public string Remark { get; set; }

    }
}