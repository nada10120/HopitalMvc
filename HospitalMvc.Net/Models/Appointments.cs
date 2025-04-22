using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalMvc.Net.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}
