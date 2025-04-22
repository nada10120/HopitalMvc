namespace HospitalMvc.Net.Models
{
    public class SampleDataDoctor
    {
        public static List<Doctor> GetDoctors()
        {
       
            return  new List<Doctor>
            {
            new Doctor {  Name = "Dr. John Smith", Specialization = "Cardiology", Img = "doctor1.jpg" },
            new Doctor { Name = "Dr. Sarah Johnson", Specialization = "Pediatrics", Img = "doctor2.jpg" },
            new Doctor {  Name = "Dr. Emily Davis", Specialization = "Dermatology", Img = "doctor4.jpg" },
             new Doctor { Name = "Dr. Michael Lee", Specialization = "Orthopedics", Img = "doctor3.jpg" },
            new Doctor { Name = "Dr. William Clark", Specialization = "Neurology", Img = "doctor5.jpg" },
            };
        }
    }
}
