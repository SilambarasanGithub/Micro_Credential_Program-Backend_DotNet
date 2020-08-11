using AppointmentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Data.EFCore
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<Appointment> GetAppointment(int appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsByUserID(int userID);
        Task<Appointment> AddAppointment(Appointment appointment);
    }
}
