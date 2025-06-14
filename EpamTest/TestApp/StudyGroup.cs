namespace TestApp
{
    public class StudyGroupUnit
    {
        public StudyGroupUnit(int studyGroupId, string name, Subject subject, DateTime createDate, List<User> users)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 5 || name.Length > 30)
                throw new ArgumentException("Group name must be between 5 and 30 characters.", nameof(name));

            if (!Enum.IsDefined(typeof(Subject), subject))
                throw new ArgumentException("Invalid subject.", nameof(subject));

            StudyGroupId = studyGroupId;
            Name = name;
            Subject = subject;
            CreateDate = createDate;
            Users = users ?? throw new ArgumentNullException(nameof(users));
        }

        //Some logic will be missing to validate values according to acceptance criteria, but imagine it is existing or do it yourself
        public int StudyGroupId { get; }

        public string Name { get; }

        public Subject Subject { get; }

        public DateTime CreateDate { get; }

        public List<User> Users { get; private set; }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
    }

    public enum Subject
    {
        Math,
        Chemistry,
        Physics
    }

    public class User
    {
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
