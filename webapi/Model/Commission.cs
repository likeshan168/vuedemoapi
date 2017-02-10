using System;
namespace lks.webapi.Model
{
    //Commission
    public class Commission
    {
        /// <summary>
        /// 工作号
        /// </summary>		
        public string 工作号 { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>		
        public string 业务员 { get; set; }
        /// <summary>
        /// 委托人简称
        /// </summary>		
        public string 委托人简称 { get; set; }
        /// <summary>
        /// 利润
        /// </summary>		
        public decimal 利润 { get; set; }
        /// <summary>
        /// 应收折合
        /// </summary>		
        public decimal 应收折合 { get; set; }
        /// <summary>
        /// 未收折合
        /// </summary>		
        public decimal 未收折合 { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>		
        public DateTime 收款日期 { get; set; }
        /// <summary>
        /// 超期日期
        /// </summary>		
        public DateTime 超期日期 { get; set; }
        /// <summary>
        /// 月数
        /// </summary>		
        public int 月数 { get; set; }
        /// <summary>
        /// 超期回款资金成本
        /// </summary>		
        public decimal 超期回款资金成本 { get; set; }
        /// <summary>
        /// 金额列
        /// </summary>		
        public decimal 金额列 { get; set; }
        /// <summary>
        /// 工作单日期
        /// </summary>		
        public DateTime 工作单日期 { get; set; }
        /// <summary>
        /// KB
        /// </summary>		
        public decimal KB { get; set; }

    }
}