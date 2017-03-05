using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Model;

namespace BLL
{
    /// <summary>
    /// 库存管理.销售出库
    /// </summary>
    public class SaleOut
    {

        /// <summary>
        /// 销售出库:新增单据
        /// </summary>
        /// <param name="rdRecord"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Save(RdRecord rdRecord, out string errMsg)
        {
            BLL.Service.RdRecord tRdRecord = new BLL.Service.RdRecord();
            //主表转换
            EntityConvert.ConvertClass<RdRecord, BLL.Service.RdRecord>(rdRecord, tRdRecord);

            //循环遍历子表
            BLL.Service.RdRecords tRdRecords;
            //初始化目标数组
            tRdRecord.List = new BLL.Service.RdRecords[rdRecord.List.Count];
            int i = 0;
            foreach (RdRecords rdRecords in rdRecord.List)
            {
                tRdRecords = new BLL.Service.RdRecords();
                EntityConvert.ConvertClass<RdRecords, BLL.Service.RdRecords>(rdRecords, tRdRecords);
                tRdRecords.SerialList = rdRecords.SerialList.ToArray();//标签序列号集合
                tRdRecord.List[i++] = tRdRecords;
            }

            return Common.Instance.Service.ST_SaleOut_Save(Common.Instance.User, tRdRecord, out errMsg);
        }
    }
}
