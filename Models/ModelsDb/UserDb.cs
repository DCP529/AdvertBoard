using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ModelsDb
{
    [Table(name: "user")]
    public class UserDb
    {
        [Key]
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        [Column("advert")]
        public ICollection<AdvertDb> AdvertCollection { get; set; }
    }
}
