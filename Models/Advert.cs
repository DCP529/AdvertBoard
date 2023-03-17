using Models.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Advert
    {
        public Guid? Id { get; set; }
        public int Number { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public int Rating { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime ExpirationDate
        {
            get => new(DateOfCreation.Year, DateOfCreation.Month, DateOfCreation.Day + 7);
        }

        public Advert()
        {
            DateOfCreation = DateTime.Now;
        }


        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (!(obj is Advert))
            {
                return false;
            }

            var advert = (Advert)obj;

            return Id == advert.Id
                && ImageName == advert.ImageName
                && DateOfCreation == advert.DateOfCreation
                && ExpirationDate == advert.ExpirationDate
                && UserId == advert.UserId
                && Text == advert.Text
                && Number == advert.Number
                && Rating == advert.Rating;
        }
    }
}
