namespace Homework.WebApi.ToDoApp.DataModels
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; } = false;

        public ToDo ToDoTask { get; set; }
        public int ToDoTaskId { get; set; }

    }
}