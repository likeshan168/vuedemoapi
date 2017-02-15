using System;
namespace lks.webapi.Model
{
    //UserInfo
    public class UserInfo
    {
        /// <summary>
        /// Id
        /// </summary>		
        public Guid Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>		
        public string Name { get; set; }
        /// <summary>
        /// Password
        /// </summary>		
        public string Password { get; set; }
        /// <summary>
        /// Email
        /// </summary>		
        public string Email { get; set; }
        /// <summary>
        /// PhoneNumber
        /// </summary>		
        public string PhoneNumber { get; set; }
        /// <summary>
        /// RoleId
        /// </summary>		
        public int RoleId { get; set; }
        /// <summary>
        /// Remark
        /// </summary>		
        public string Remark { get; set; }

    }
}