using KhoaHocApi.Application.Payloads.ResponseModel.DataSubject;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.SubjectConverter
{
    public class SubjectDetailsConverter
    {
        private readonly IBaseRepository<Subject> _subjectRepository;
        public SubjectDetailsConverter(IBaseRepository<Subject> subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public  DataResponseSubjectDetails EntityDTO(SubjectDetail subjectDetails)
        {
            var subjectDetail= _subjectRepository.GetByIdAsync(subjectDetails.SubjectId);
            return new DataResponseSubjectDetails
            {
                SubjectDetailId = subjectDetails.Id,
                SubjectDetailName = subjectDetails.SubjectDetailName,
                LinkVideo = subjectDetails.LinkVideo,
                IsActive = subjectDetails.IsActive,
                IsFinished = subjectDetails.IsFinished,
                SubjectName = subjectDetail.Result.SubjectName
        };
        }
    }
}
