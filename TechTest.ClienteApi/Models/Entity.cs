namespace ClienteApi.Models
{
    public abstract class Entity
    {
        protected Entity() => Id = System.Guid.NewGuid();

        public System.Guid Id { get; set; }
    }
}