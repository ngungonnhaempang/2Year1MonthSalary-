using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{
    public class IEPick
    {
        //船期编号
        public string sono { get; set; }
        //柜号
        public string macno { get; set; }

        //發貨單號
        public string outno { get; set; }

        //包号
        public string pattern { get; set; }
        //发货单行项目
        public string pkno { get; set; }
        //銷售訂單號碼
        public string Ordno { get; set; }
        //销售订单项号
        public string Pos { get; set; }
        //拣配数量(KG)
        public decimal ryard2 { get; set; }
        //拣配数量单位(KG)
        public string unit { get; set; }


        //數量(工廠)淨/每包
        public decimal ryard { get; set; }
        //數量(工廠)毛重/每包
        public decimal gwquan { get; set; }

        //物料編號
        public string fbcno { get; set; }

        //物料說明(SAP/MES提供)
        public string cunicode { get; set; }

        public string matdesc { get; set; }

        public string outdate { get; set; }


        public decimal PackNum { get; set; }
        public string PackType { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料批次
        /// </summary>
        public string Chkno { get; set; }

        public decimal Volume { get; set; }

        public string VolUnit { get; set; }

        public string Fpu { get; set; }

        /// <summary>
        /// 栈板重量
        /// </summary>
        public decimal Pallet { get; set; }
    }

    public class IEItem
    {

    }
}
