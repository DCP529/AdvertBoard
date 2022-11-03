using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsDb
{
    [Table(name: "advert")]
    public class AdvertDb
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [ForeignKey(nameof(UserId))]
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("text")]
        public string? Text { get; set; }

        [Column("image")]
        public string? ImageName { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("date_of_creation")]
        public DateTime DateOfCreation { get; set; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }
    }
}
