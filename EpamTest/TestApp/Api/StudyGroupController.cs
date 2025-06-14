using TestApp.Interfaces;

namespace TestApp.Api
{
    public class StudyGroupController
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupController(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        public async Task<IActionResult> CreateStudyGroup(StudyGroupUnit studyGroupUnit)
        {
            if (string.IsNullOrWhiteSpace(studyGroupUnit.Name) || studyGroupUnit.Name.Length < 5 || studyGroupUnit.Name.Length > 30)
                return new BadRequestResult("Group name must be between 5 and 30 characters.");

            if (!Enum.IsDefined(typeof(Subject), studyGroupUnit.Subject))
                return new BadRequestResult("Invalid subject. Must be Math, Chemistry, or Physics.");

            var existingGroups = await _studyGroupRepository.GetStudyGroups();
            bool alreadyExists = existingGroups.Any(g => g.Subject == studyGroupUnit.Subject);

            if (alreadyExists)
                return new ConflictResult("Study group already exists");

            await _studyGroupRepository.CreateStudyGroup(studyGroupUnit);
            return new OkResult();
        }

        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> SearchStudyGroups(string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return new OkResult();
        }
    }
}
