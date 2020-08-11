using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public int OrganizerId { get; set; }
        [Required]
        public int AttendeeId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [DefaultValue("false")]
        public bool AllDay { get; set; }
        public string Decsription { get; set; }
    }
}
