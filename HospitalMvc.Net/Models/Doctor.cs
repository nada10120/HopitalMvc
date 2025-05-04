using System.ComponentModel.DataAnnotations;

namespace HospitalMvc.Net.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, ErrorMessage = "Specialization cannot exceed 100 characters")]
        public string Specialization { get; set; }

        public string Img { get; set; }=nameof(Doctor) + ".jpg";

        public ICollection<Appointments> Appointments { get; set; }
    }
}
