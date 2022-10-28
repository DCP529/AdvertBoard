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
        public string ImagePath { get; set; }
        public int MyProperty { get; set; }
        public int Rating { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
