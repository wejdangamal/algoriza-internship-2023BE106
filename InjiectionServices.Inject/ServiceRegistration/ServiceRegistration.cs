using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vzeeta.Core.Repository;
using Vzeeta.Core.Service;
using Vzeeta.Repository;
using Vzeeta.Services.Interfaces.IAdmin;
using Vzeeta.Services.Interfaces.IDoctor;
using Vzeeta.Services.Interfaces.IDoctorInterfaces;
using Vzeeta.Services.Interfaces.IPatient;
using Vzeeta.Services.Services.AccountService;
using Vzeeta.Services.Services.AdminServices;
using Vzeeta.Services.Services.DoctorsServices;
using Vzeeta.Services.Services.PatientsServices;

namespace InjiectionServices.Inject.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(ICustomService<>), typeof(CustomService<>));
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAdminSetting, AdminSetting>();
            services.AddScoped<IAdminDashBoard, AdminDashBoard>();
            services.AddScoped<IAdminDoctorBoard, AdminDoctorBoard>();
            services.AddScoped<IAdminPatientsBoard, AdminPatientsBoard>();
            services.AddScoped<IDoctorRequests, DoctorRequests>();
            services.AddScoped<ICancelBooking, CancelBooking>();
            services.AddScoped<IPatientSearchBookings, PatientSearchBookings>();
            services.AddScoped<IPatientAllBookings, PatientAllBookings>();
            services.AddScoped<IDoctorRepository, DoctorAppointmentsService>();
            services.AddScoped<ITimeSlotSettings, TimeSlotSettings>();
            services.AddScoped<IAppointmentSettings, AppointmentSettings>();
            services.AddScoped<ISendEmailService, SendEmailService>();
        }
    }
}
