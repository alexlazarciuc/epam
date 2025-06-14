using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Interfaces
{
    public interface IStudyGroupRepository
    {
        Task CreateStudyGroup(StudyGroupUnit studyGroupUnit);

        Task<List<StudyGroupUnit>> GetStudyGroups();

        Task<List<StudyGroupUnit>> SearchStudyGroups(string subject);

        Task JoinStudyGroup(int studyGroupId, int userId);

        Task LeaveStudyGroup(int studyGroupId, int userId);
    }
}
