namespace StudentsApi.DTOs
{
    public record ResultOfCreatingStudents
    {
        public Guid? Id { get; private set; }
        public string? ErrorMessage { get; private set; }

        public ResultOfCreatingStudents(Guid id)
        {
            Id = id;
        }
        public ResultOfCreatingStudents(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
