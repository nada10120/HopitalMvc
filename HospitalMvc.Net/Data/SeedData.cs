using HospitalMvc.Net.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalMvc.Net.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();


            // Exit if data already exists
            if (context.Doctors.Any()) return;

            context.Doctors.AddRange(SampleDataDoctor.GetDoctors());
            context.SaveChanges();
        }
    }
}
