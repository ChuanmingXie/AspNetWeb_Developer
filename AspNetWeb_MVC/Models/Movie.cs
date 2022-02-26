using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AspNetWeb_MVC.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(60,MinimumLength =3)]
        [Display(Name ="电影名称")]
        public string Title { get; set; }

        [Display(Name ="上映时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Display(Name ="流派")]
        public string Genre { get; set; }

        [Range(1,100)]
        [DataType(DataType.Currency)]
        [Display(Name ="价格")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z][a-zA-Z'\s]*$")]
        [StringLength(5)]
        [Display(Name ="限制级别")]
        public string Rating { get; set; }
    }

    public class MovieDBContext : DbContext
    {
        public MovieDBContext():base("name=dbConnection")
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}