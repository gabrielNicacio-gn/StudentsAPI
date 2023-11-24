namespace StudentsApi.Entitys
{
    public class Student
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Registration { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }

        public Student(string name,string registration,string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Registration = registration;
            Email = email;
            IsActive = true;
        }
        public void Inactivate()
        {
            IsActive = false;
        }
        public void UpdateStudent(string name, string resgistration, string email)
        {
            Name = name;
            Registration = resgistration;
            Email = email;
        }

    }
}
