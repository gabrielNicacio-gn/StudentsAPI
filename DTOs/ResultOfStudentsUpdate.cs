namespace StudentsApi.DTOs
{
    public record ResultOfStudentsUpdate
    {
        public ReturnStudents? Return { get; private set; }
        public string? ErrorMessage { get; private set; }

        public ResultOfStudentsUpdate(ReturnStudents returnStudents)
        {
            Return = returnStudents;
        }
        public ResultOfStudentsUpdate(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

    }
}
