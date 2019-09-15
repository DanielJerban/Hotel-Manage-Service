using HMS.Service.Core;
using HMS.Service.Core.Interfaces;
using HMS.Service.Persistance.Repositories;
using HMS.Web.Models;

namespace HMS.Service.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepo Employee { get; }
        public ICustomerRepo Customer { get; }
        public IHotelRepo Hotel { get; }
        public IRoomFacilityRepo RoomFacility { get; }
        public IRoomImageRepo RoomImage { get; }
        public IRoomRepo Room { get; }
        public IPersonRepo Person { get; }
        public IContactInfoRepo ContactInfo { get; }
        public IUserRepo User { get; }
        public IReservationRepo Reservation { get; }
        public IPassengerRepo Passenger { get; }
        public IVerbalRentRepo VerbalRent { get; }

        private ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
            Employee = new EmployeeRepo(db);
            Customer = new CustomerRepo(db);
            Hotel = new HotelRepo(db);
            RoomFacility = new RoomFacilityRepo(db);
            RoomImage = new RoomImageRepo(db);
            Room = new RoomRepo(db);
            Person = new PersonRepo(db);
            ContactInfo = new ContactInfoRepo(db);
            User = new UserRepo(db);
            Reservation = new ReservationRepo(db);
            Passenger = new PassengerRepo(db);
            VerbalRent = new VerbalRentRepo(db);
        }

        public int Complete()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}