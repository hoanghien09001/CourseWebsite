using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Enumerates
{
    public class ConstantEnum
    {
        public enum UserStatusEnum
        {
            UnActived = 0,
            Actived = 1,
        }
        public enum LevelEnum
        {
            Easy = 1,
            Medium = 2,
            Hard = 3,
        }
        public enum HomeworkEnum
        {
            Unfinished = 0,
            Finished = 1,
        }
    }
}
