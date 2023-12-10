using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;

namespace Vzeeta.Data.Data
{
    public static class SeedingData
    {
        public static async Task Seed(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceBuilder = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceBuilder.ServiceProvider.GetService<ApplicationDBContext>();

                if (!context.Specializations.Any())
                {
                    var specializations = new List<Specialization>
                        {
                             new Specialization {   specializeType = "General Practitioner" , Description = "Provides primary healthcare and referrals."},
                             new Specialization {   specializeType = "Dentist", Description = "Specializes in oral health and dental care."},
                             new Specialization {   specializeType = "Orthodontist",  Description ="Focuses on the alignment of teeth and jaws."},
                             new Specialization {   specializeType = "Cardiologist", Description = "Specializes in heart-related issues."},
                             new Specialization {   specializeType = "Dermatologist", Description = "Focuses on skin, hair, and nail health."},
                             new Specialization {   specializeType = "Gynecologist", Description = "Specializes in women's reproductive health."},
                             new Specialization {   specializeType = "Pediatrician", Description = "Focuses on the health of infants, children, and adolescents."},
                             new Specialization {   specializeType = "Psychiatrist", Description = "Specializes in mental health and psychological disorders."},
                             new Specialization {   specializeType = "Neurologist", Description = "Treats disorders of the nervous system."},
                             new Specialization {   specializeType = "Gastroenterologist", Description = "Specializes in digestive system disorders."},
                             new Specialization {   specializeType = "Ophthalmologist",  Description = "Deals with eye care and vision problems."},
                             new Specialization {   specializeType = "Orthopedic Surgeon", Description = "Deals with disorders of the musculoskeletal system."},
                             new Specialization {   specializeType = "Urologist", Description = "Specializes in urinary tract and male reproductive system issues."},
                             new Specialization {   specializeType = "ENT Specialist", Description = "Focuses on ear, nose, and throat issues."},
                             new Specialization {   specializeType = "Obstetrician", Description = "Specializes in pregnancy and childbirth."},
                             new Specialization {   specializeType = "Endocrinologist", Description = "Focuses on hormone-related disorders."},
                             new Specialization {   specializeType = "Rheumatologist", Description = "Treats autoimmune and inflammatory disorders."},

                             new Specialization {   specializeType = "Physiotherapist", Description =" Helps with physical rehabilitation and movement."},
                             new Specialization {   specializeType = "Nutritionist", Description = " Provides guidance on nutrition and dietary plans."},
                             new Specialization {   specializeType = "Oncologist", Description = " Specializes in the diagnosis and treatment of cancer."},
                             new Specialization {   specializeType = "Nephrologist", Description = " Deals with kidney-related conditions and diseases."},
                             new Specialization {   specializeType = "Infectious Disease Specialist", Description = " Diagnoses and treats infectious diseases."},
                             new Specialization {   specializeType = "Hematologist", Description = " Specializes in disorders of the blood and blood-forming tissues."},
                             new Specialization {   specializeType = "Podiatrist", Description = " Focuses on foot and ankle health."},
                             new Specialization {   specializeType = "Sports Medicine Physician", Description = " Treats sports-related injuries and promotes physical fitness."},
                             new Specialization {   specializeType = "Geriatrician", Description = " Specializes in the health and well-being of elderly individuals."},
                             new Specialization {   specializeType = "Sleep Medicine Specialist", Description = " Addresses sleep disorders and disturbances."},
                             new Specialization {   specializeType = "Pain Management Specialist", Description = " Focuses on the management of chronic pain conditions."},
                             new Specialization {   specializeType = "Radiologist", Description = " Interprets medical imaging, such as X-rays and MRIs."},
                             new Specialization {   specializeType = "Interventional Cardiologist", Description = " Performs minimally invasive heart procedures."},
                             new Specialization {   specializeType = "Plastic Surgeon", Description = " Specializes in cosmetic and reconstructive surgery."},
                             new Specialization {   specializeType = "Cardiothoracic Surgeon", Description = " Performs surgeries on the heart, lungs, and other thoracic organs."},
                             new Specialization {   specializeType = "Endodontist", Description = " Specializes in root canal treatment and dental pulp issues."},
                             new Specialization {   specializeType = "Genetic Counselor", Description = " Provides guidance on genetic testing and inherited conditions."},
                             new Specialization {   specializeType = "Palliative Care Specialist", Description = " Focuses on improving the quality of life for patients with serious illnesses."},
                             new Specialization {   specializeType = "Forensic Pathologist", Description = " Investigates and determines the cause of death in legal cases."},
                             new Specialization {   specializeType = "Hospitalist", Description = " Manages the care of patients within a hospital setting."},
                             new Specialization {   specializeType = "Telemedicine Practitioner", Description = " Offers medical consultations and advice remotely."}
                        };

                    context.Specializations.AddRange(specializations);
                    context.SaveChanges();
                }
                if (!context.Users.Any(user => user.Role == UserRole.Admin))
                {
                    var userManager = serviceBuilder.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var admin = new ApplicationUser
                    {
                        Email = "wejdanadmain451@gmail.com",
                        firstName = "wejdan",
                        lastName = "admin",
                        gender = Gender.FEMALE,
                        DateOfBirth = new DateTime(2000, 12, 12),
                        UserName = "wejdan_admin",
                        PhoneNumber = "012345678910",
                        Role = UserRole.Admin,
                    };
                    var result = await userManager.CreateAsync(admin, "Vzeeta_Admin12");
                    if (result.Succeeded)
                    {
                        var addToRole = await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
                    }
                    else
                    {
                        throw new Exception("Failed to seed the Admin.");
                    }
                }
            }
        }
    }
}
