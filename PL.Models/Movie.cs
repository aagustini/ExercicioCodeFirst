using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        // [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }


        [StringLength(60, MinimumLength = 3)]
        public string Director { get; set; }

        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Gross { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        //public string Title { get; set; }
        //public string Director { get; set; }
        //public DateTime ReleaseDate { get; set; }
        //public decimal Gross { get; set; }
        //public double Rating { get; set; }

        [Display(Name = "Upload image")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Image link")]
        [DataType(DataType.ImageUrl)]
        public String ImageUrl { get; set; }


        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<ActorMovie> Characters { get; set; }
    }
}
