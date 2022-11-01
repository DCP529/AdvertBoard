using Models.ModelsDb;

namespace Models
{
    public class User
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Advert> AdvertCollection { get; set; }
    }
}