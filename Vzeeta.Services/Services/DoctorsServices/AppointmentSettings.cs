using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Services.Interfaces.IDoctorInterfaces;

namespace Vzeeta.Services.Services.DoctorsServices
{
    public class AppointmentSettings : IAppointmentSettings
    {
        private readonly IRepository<Appointments, int> appointmentRespository;

        public AppointmentSettings(IRepository<Appointments, int> appointmentRespository)
        {
            this.appointmentRespository = appointmentRespository;
        }
        public async Task<bool> Add(Appointments appointments)
        {
            if (appointments != null)
            {
                var res = await appointmentRespository.Add(appointments);
                return res;
            }
            return false;
        }

        public async Task<Core.Model.Appointments> GetById(int id)
        {
            var res = await appointmentRespository.GetById(id);
            return res;
        }
    }
}