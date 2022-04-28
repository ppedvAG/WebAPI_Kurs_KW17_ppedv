namespace WebAPIKurs.Data
{
    public static class DataSeeder
    {
        public static void SeedTodoDb(ToDoDbContext context)
        {
            //Ist die Datenbank leer?
            if (!context.ToDoItem.Any())
            {
                context.ToDoItem.Add(new Models.ToDoItem() {  Name = "Spazieren gehen", IsClosed=false });
                context.ToDoItem.Add(new Models.ToDoItem() {  Name = "Fussball spielen", IsClosed = true });
                context.SaveChanges();
            }
        }
    }
}
