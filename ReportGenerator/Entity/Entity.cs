namespace ReportGenerator.Entity
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Entity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Entity()
        {

        }
    }
}
