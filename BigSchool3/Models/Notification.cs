using System;

namespace BigSchool3.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalPlace { get; set; }

        public NotificationType Type { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }





    }
}