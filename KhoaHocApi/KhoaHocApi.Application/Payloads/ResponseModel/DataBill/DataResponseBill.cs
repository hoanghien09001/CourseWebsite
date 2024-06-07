using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataBill
{
    public class DataResponseBill:DataResponseBase
    {       
        public string UserName { get; set; }
        public string CourseName { get; set; }
        public double Price { get; set; }
        public string TradingCode { get; set; } 
        public string BillStatus { get; set; }
        public DateTime CreateTime { get; set; } 


    }
}
