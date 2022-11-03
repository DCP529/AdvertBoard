using Models.ModelsDb;

namespace Models
{
    public class User
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Advert> AdvertCollection { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
            {
                return false;
            }
            if (!(obj is User))
            {
                return false;
            }

            User objUser = (User)obj;

            return Id == objUser.Id
            && Name == objUser.Name
            && IsAdmin == objUser.IsAdmin
            && AdvertCollection == objUser.AdvertCollection;
        }
    }
}