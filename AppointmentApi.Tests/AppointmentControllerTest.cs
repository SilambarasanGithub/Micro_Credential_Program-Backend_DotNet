using AppointmentApi.Data.EFCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using AppointmentApi.Model;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;

namespace AppointmentApi.Tests
{
    class AppointmentControllerTest
    {
        Mock<IAppointmentRepository> _mockAppointmentRepository = new Mock<IAppointmentRepository>();
        List<Appointment> appointments;
        private IAppointmentRepository MockAppointmentRepository;


        [SetUp]
        public void Setup()
        {
            appointments = new List<Appointment>{
                new Appointment { Id = 1, OrganizerId = 1, AttendeeId = 2, Subject = "Weekly Meeting", Location = "Web-ex", StartTime = Convert.ToDateTime("2020-07-30 09:00:00.0000000"), EndTime = Convert.ToDateTime("2020-07-30 09:30:00.0000000"), AllDay = false, Decsription = "" },
                new Appointment { Id = 2, OrganizerId = 3, AttendeeId = 4, Subject = "Montly Meeting", Location = "Web-ex", StartTime = Convert.ToDateTime("2020-08-10 11:00:00.0000000"), EndTime = Convert.ToDateTime("2020-08-10 12:00:00.0000000"), AllDay = false, Decsription = "" } 
            };

            // Arrange Mock to Get All Appointments
            _mockAppointmentRepository.Setup(m => m.GetAppointments())
                .Returns(Task.FromResult<IEnumerable<Appointment>>(appointments));

            // Arrange Mock to Get Appointment By ID
            _mockAppointmentRepository.Setup(m => m.GetAppointment(It.IsAny<int>()))
                .Returns((int i) => Task.FromResult(appointments.Where(x => x.Id == i).SingleOrDefault()));

            // Arrange Mock to Get Appointment By userID
            _mockAppointmentRepository.Setup(m => m.GetAppointmentsByUserID(It.IsAny<int>()))
                .Returns((int i) => Task.FromResult<IEnumerable<Appointment>>(appointments.Where(x => x.OrganizerId == i || x.AttendeeId == i).OrderBy(y => y.StartTime)));

            // Arrange Mock to Insert Appointment
            _mockAppointmentRepository.Setup(m => m.AddAppointment(It.IsAny<Appointment>()))
                .Returns(
                (Appointment target) =>
                {
                    appointments.Add(target);
                    return Task.FromResult(target);
                });

            // Complete setup of Mock AppointmentRepository
            MockAppointmentRepository = _mockAppointmentRepository.Object;
        }

        [Test]
        public void Test1_CanGetAllAppointments()
        {
            // Act
            var allAppointments = MockAppointmentRepository.GetAppointments();

            // Assert
            Assert.IsNotNull(allAppointments);
            Assert.AreEqual(2, allAppointments.Result.Count());
        }

        [Test]
        public void Test2_CanGetAllAppointmentByID()
        {
            // Act
            Appointment appointment = MockAppointmentRepository.GetAppointment(2).Result;

            // Assert
            Assert.IsNotNull(appointment);
            Assert.IsInstanceOf(typeof(Appointment), appointment);
            Assert.AreEqual("Montly Meeting", appointment.Subject);
        }

        [Test]
        public void Test3_CanAddNewAppointment()
        {
            // Arrange
            Appointment newAppointment = new Appointment { Id = 1, OrganizerId = 1, AttendeeId = 2, Subject = "Production Support", Location = "Web-ex", StartTime = Convert.ToDateTime("2020-08-14 09:00:00.0000000"), EndTime = Convert.ToDateTime("2020-08-14 23:59:59.0000000"), AllDay = true, Decsription = "" };

            //Act
            Appointment appointment = MockAppointmentRepository.AddAppointment(newAppointment).Result;

            // Assert
            Assert.AreEqual(3, MockAppointmentRepository.GetAppointments().Result.Count());
            Assert.IsNotNull(appointment);
            Assert.IsInstanceOf(typeof(Appointment), appointment);
            Assert.AreEqual("Production Support", appointment.Subject);
        }

        [Test]
        public void Test4_CanGetAppointmentsByUserId()
        {
            // Act
            var userAppointments = MockAppointmentRepository.GetAppointmentsByUserID(2);

            // Assert
            Assert.IsNotNull(userAppointments);
            Assert.AreEqual(1, userAppointments.Result.Count());
        }

    }
}
