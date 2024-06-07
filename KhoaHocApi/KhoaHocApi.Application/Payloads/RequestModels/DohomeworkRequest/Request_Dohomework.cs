using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.DohomeworkRequest
{
    public class Request_Dohomework
    {
        public long PracticeId { get; set; }
        public string ActualOutput { get; set; }
        public long RegisterStudyId { get; set; }

    }
}
