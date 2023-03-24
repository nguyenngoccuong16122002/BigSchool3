using BigSchool3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BigSchool3.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        public byte Category { get; set; }
        public IEnumerable<Category> Categoies { get; set; }
        public DateTime GetDateTime()
        {
            string dateInString = string.Format("{0} {1}", Date, Time);

            return DateTime.ParseExact(dateInString, "d/M/yyyy hh:mm", CultureInfo.InvariantCulture);
        }
        public string Heading { get; set; }
        public string Action { get { return (Id != 0) ? "Update" : "Create"; }  }
    }
}
