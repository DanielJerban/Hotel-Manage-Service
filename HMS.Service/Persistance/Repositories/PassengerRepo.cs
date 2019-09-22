using System.Data.Entity;
using HMS.Model.Core.DomainModels;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Persistance.Repositories
{
    public class PassengerRepo : Repository<Passenger>, IPassengerRepo
    {
        public PassengerRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<string> CheckingPassengers(Guid CheckingId)
        {
            var passengers = context.Passengers.Include(c => c.Customer).Where(c => c.CheckingId == CheckingId).ToList();
            List<string> passengersName = new List<string>();

            foreach (var item in passengers)
            {
                passengersName.Add(item.Customer.FirstName + " " + item.Customer.LastName);
            }

            return passengersName;
        }
    }
}
