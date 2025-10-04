namespace DataAccessLayer.Entites.Identity
{
    public class BaseEntity <T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
