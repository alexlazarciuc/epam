
namespace TestApp.Tests.Unit
{
    [TestFixture]
    public class StudyGroupUnitTests
    {
        [Test]
        public void Assert_Valid_Group_Name() // 1a. Name must be 5–30 characters
        {
            var group = new StudyGroupUnit(1, "Math Squad", Subject.Math, DateTime.Now, new List<User>());
            Assert.AreEqual("Math Squad", group.Name);
        }

        [Test]
        public void Assert_Short_Group_Name_Throws_Exception() // 1a. Name must be 5–30 characters
        {
            Assert.Throws<ArgumentException>(() =>
                new StudyGroupUnit(2, "abc", Subject.Chemistry, DateTime.Now, new List<User>()));
        }

        [Test]
        public void Assert_Long_Group_Name_Throws_Exception() // 1a. Name must be 5–30 characters
        {
            var longName = new string('x', 40);
            Assert.Throws<ArgumentException>(() =>
                new StudyGroupUnit(3, longName, Subject.Physics, DateTime.Now, new List<User>()));
        }

        [Test]
        public void Assert_Invalid_Subject_Throws_Exception() // 1b. Only valid subjects: Math, Chemistry, Physics
        {
            Assert.Throws<ArgumentException>(() =>
                new StudyGroupUnit(4, "Random Group", (Subject)999, DateTime.Now, new List<User>()));
        }

        [Test]
        public void Assert_Add_User_Will_Increase_UserCount() // 2. Users can join Study Groups for different Subjects
        {
            var group = new StudyGroupUnit(5, "Physics Crew", Subject.Physics, DateTime.Now, new List<User>());
            var user = new User(1, "Michael");
            group.AddUser(user);
            Assert.AreEqual(1, group.Users.Count);
        }

        [Test]
        public void Assert_Remove_User_Will_Decrease_User_Count() // 4. Users can leave Study Groups they joined
        {
            var user = new User(2, "Alex");
            var group = new StudyGroupUnit(6, "Chem Buddies", Subject.Chemistry, DateTime.Now, new List<User> { user });
            group.RemoveUser(user);
            Assert.AreEqual(0, group.Users.Count);
        }

        [Test]
        public void Assert_Remove_User_That_Is_Not_In_Group_Does_Not_Fail() // 4. Users can leave Study Groups they joined (edge case)
        {
            var user = new User(3, "Mark");
            var group = new StudyGroupUnit(61, "Extra Chemistry", Subject.Chemistry, DateTime.Now, new List<User>());
            Assert.DoesNotThrow(() => group.RemoveUser(user));
            Assert.AreEqual(0, group.Users.Count);
        }

        [Test]
        public void Assert_Create_Date_Is_Set_Correctly() // 1c. We want to record when Study Groups were created
        {
            var now = DateTime.Now;
            var group = new StudyGroupUnit(7, "Science Stars", Subject.Math, now, new List<User>());
            Assert.That(group.CreateDate, Is.EqualTo(now).Within(TimeSpan.FromSeconds(1)));
        }
    }
}
