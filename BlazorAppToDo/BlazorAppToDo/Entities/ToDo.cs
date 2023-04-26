namespace BlazorAppToDo.Entities
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
