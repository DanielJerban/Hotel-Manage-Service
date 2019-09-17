using HMS.Service.Core.Interfaces;

namespace HMS.Service.Core
{
    public interface IUnitOfWork
    {

        IEmployeeRepo Employee { get; }
        ICustomerRepo Customer { get; }
        IHotelRepo Hotel { get; }
        IRoomFacilityRepo RoomFacility { get; }
        IRoomImageRepo RoomImage { get; }
        IRoomRepo Room { get; }
        IPersonRepo Person { get; }
        IContactInfoRepo ContactInfo { get; }
        IUserRepo User { get; }
        IPassengerRepo Passenger { get; }
        
        /// <summary>
        /// Save changes on database 
        /// </summary>
        /// <returns>Returns 1 if savechanges is succeed</returns>
        int Complete();
    }
}