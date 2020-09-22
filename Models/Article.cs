using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ClicLikes.Models
{
    [Table("Article")]
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime Creation { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Content { get; set; }
    }

    public class ArticleView
    {
        public int Id { get; set; }
        public DateTime Creation { get; set; }
        public string Content { get; set; }
        public int NumberLikes { get; set; }
    }
}
