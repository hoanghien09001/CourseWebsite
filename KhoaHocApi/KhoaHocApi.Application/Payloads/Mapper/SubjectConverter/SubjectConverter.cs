using KhoaHocApi.Application.Payloads.ResponseModel.DataSubject;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.SubjectConverter
{
    public class SubjectConverter
    {
        public DataResponseSubject EntityDTO(Subject subject)
        {
            return new DataResponseSubject
            {
                SubjectId = subject.Id,
                SubjectName = subject.SubjectName,
                Symbol = subject.Symbol,
                IsActive = subject.IsActive,
            };
        }
    }
}
