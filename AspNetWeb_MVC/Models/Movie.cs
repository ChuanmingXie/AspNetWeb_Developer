using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AspNetWeb_MVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name ="电影名称")]
        public string Title { get; set; }

        [Display(Name ="上映时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name ="流派")]
        public string Genre { get; set; }

        [Display(Name ="价格")]
        public decimal Price { get; set; }

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