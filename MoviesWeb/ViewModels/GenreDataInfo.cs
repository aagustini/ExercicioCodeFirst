using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesWeb.ViewModels
{
    public class GenreDataInfo
    {
        [Display(Name = "Genre")]
        public String GenreName { get; set; }

        [Display(Name = "Count")]
        public int GenreCount { get; set; }

        [Display(Name = "Total Gross")]
        [DataType(DataType.Currency)]
        public decimal TotalGross { get; set; }

        [Display(Name = "Average Rating")]
        [DisplayFormat(DataFormatString = "{0:.0}")]
        public double AverageRating { get; set; }

    }
}