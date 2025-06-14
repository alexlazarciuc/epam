using Moq;
using TestApp.Api;
using TestApp.Interfaces;

namespace TestApp.Tests.Component
{
    [TestFixture]
    public class StudyGroupControllerTests
    {
        private Mock<IStudyGroupRepository> _mockRepo;
        private StudyGroupController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IStudyGroupRepository>();
            _controller = new StudyGroupController(_mockRepo.Object);
        }

        [Test]
        public async Task CreateStudyGroup_Valid_ReturnsOk() // 1a. Name length valid; 1b. Subject valid; 1. Group is unique
        {
            var group = new StudyGroupUnit(1, "Math Legends", Subject.Math, DateTime.Now, new List<User>());
            _mockRepo.Setup(r => r.GetStudyGroups()).ReturnsAsync(new List<StudyGroupUnit>());

            var result = await _controller.CreateStudyGroup(group);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task CreateStudyGroup_DuplicateSubject_ReturnsConflict() // 1. There can be only one Study Group per subject
        {
            var group = new StudyGroupUnit(1, "Physics Group", Subject.Physics, DateTime.Now, new List<User>());
            _mockRepo.Setup(r => r.GetStudyGroups()).ReturnsAsync(new List<StudyGroupUnit> { group });

            var result = await _controller.CreateStudyGroup(group);
            Assert.IsInstanceOf<ConflictResult>(result);
        }

        [Test]
        public async Task GetStudyGroups_ReturnsExpectedList() // 3. Users can check the list of all existing Study Groups
        {
            var groups = new List<StudyGroupUnit> { new StudyGroupUnit(1, "Physics Pioneers", Subject.Physics, DateTime.Now, new List<User>()) };
            _mockRepo.Setup(r => r.GetStudyGroups()).ReturnsAsync(groups);

            var result = await _controller.GetStudyGroups() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(groups, result.Value);
        }

        [Test]
        public async Task SearchStudyGroups_BySubject_ReturnsFilteredList() // 3a. Users can filter Study Groups by a given Subject
        {
            var groups = new List<StudyGroupUnit>
            {
                new (1, "Math One", Subject.Math, DateTime.Now, new List<User>()),
                new (2, "Chem Group", Subject.Chemistry, DateTime.Now, new List<User>())
            };
            _mockRepo.Setup(r => r.SearchStudyGroups("Math")).ReturnsAsync(groups.FindAll(g => g.Subject == Subject.Math));

            var result = await _controller.SearchStudyGroups("Math") as OkObjectResult;
            Assert.IsNotNull(result);
            var list = result.Value as List<StudyGroupUnit>;
            Assert.That(list, Has.Count.EqualTo(1));
            Assert.AreEqual(Subject.Math, list[0].Subject);
        }

        [Test]
        public async Task JoinStudyGroup_ValidRequest_ReturnsOk() // 2. Users can join Study Groups for different Subjects
        {
            var result = await _controller.JoinStudyGroup(1, 5);
            _mockRepo.Verify(r => r.JoinStudyGroup(1, 5), Times.Once);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task LeaveStudyGroup_ValidRequest_ReturnsOk()  // 4. Users can leave Study Groups they joined
        {
            var result = await _controller.LeaveStudyGroup(1, 5);
            _mockRepo.Verify(r => r.LeaveStudyGroup(1, 5), Times.Once);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task GetStudyGroups_Sorted_By_Newest_First()  // 3b. Users can sort to see most recently created Study Groups or oldest ones
        {
            var now = DateTime.Now;
            var older = now.AddDays(-1);
            var newer = now.AddMinutes(1);

            var groups = new List<StudyGroupUnit>
            {
                new StudyGroupUnit(1, "Old Group", Subject.Physics, older, new List<User>()),
                new StudyGroupUnit(2, "New Group", Subject.Math, newer, new List<User>())
            };

            _mockRepo.Setup(r => r.GetStudyGroups()).ReturnsAsync(groups.OrderByDescending(g => g.CreateDate).ToList());

            var result = await _controller.GetStudyGroups() as OkObjectResult;

            var returnedGroups = result?.Value as List<StudyGroupUnit>;
            Assert.That(returnedGroups, Is.Not.Null);
            Assert.That(returnedGroups[0].Name, Is.EqualTo("New Group"));
        }
    }
}