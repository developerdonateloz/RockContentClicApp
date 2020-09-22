using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ClicLikes.Models
{
    [Table("Like")]
    public class Like
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime Creation { get; set; }

        [Column(TypeName = "Varchar(12)")]
        public string UserCode { get; set; }

        public int ArticleId { get; set; }
    }
    public class LikeView
    {
        public int ArticleId { get; set; }
    }
}
