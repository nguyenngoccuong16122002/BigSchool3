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
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập")]
        [FutureDate]
        public string Date { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập")]
        [ValidTime]
        public string Time { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public byte Category { get; set; }
        public IEnumerable<Category> Categoies { get; set; }
        public DateTime GetDateTime()
        {
            string dateInString = string.Format("{0} {1}", Date, Time);

            return DateTime.ParseExact(dateInString, "d/M/yyyy hh:mm", CultureInfo.InvariantCulture);
        }
    }
}
