using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Data.EFCore;
using AppointmentApi.Model;
using System.Security.Cryptography.X509Certificates;

namespace AppointmentApi.Data.EFCore
{
    public class AppointmentRepository : IAppointmentRepository
    {
        
        AppointmentDbContext _appointmentDbContext;

        public AppointmentRepository(AppointmentDbContext appointmentDbContext)
        {
            _appointmentDbContext = appointmentDbContext;
        }
        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _appointmentDbContext.Appointments.ToListAsync();
        }
        public async Task<Appointment> GetAppointment(int appointmentID)
        {
            return await _appointmentDbContext.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentID);
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserID(int userID)
        {
            return await _appointmentDbContext.Appointments
                .Where(x => x.OrganizerId == userID || x.AttendeeId == userID).OrderBy(y => y.StartTime).ToListAsync();
        }
        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            var result = await _appointmentDbContext.Appointments.AddAsync(appointment);
            await _appointmentDbContext.SaveChangesAsync();
            return result.Entity;
        }

    }
}
