using System;
using System.Collections.Generic;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Checking;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace HMS.Service.Persistance.Repositories
{
    public class CheckingRepo : Repository<Checking>, ICheckingRepo
    {
        public CheckingRepo(ApplicationDbContext context) : base(context)
        {
        }

        public List<CheckingDto> GetCheckings()
        {
            var checkings = context.Checkings.Include(c => c.Customer).Include(c => c.Room).ToList();
            List<CheckingDto> checkingDtos = new List<CheckingDto>();

            foreach (var item in checkings)
            {
                PersianCalendar pc = new PersianCalendar();
                string FromDate = pc.GetDayOfMonth(item.FromDate).ToString() + "/"
                                + pc.GetMonth(item.FromDate).ToString() + "/"
                                + pc.GetYear(item.FromDate).ToString();

                string ToDate = pc.GetDayOfMonth(item.ToDate).ToString() + "/"
                              + pc.GetMonth(item.ToDate).ToString() + "/"
                              + pc.GetYear(item.ToDate).ToString();

                checkingDtos.Add(new CheckingDto()
                {
                    Id = item.Id,           
                    FromDate = FromDate,
                    ToDate = ToDate,
                    CustomerName = item.Customer.FirstName + " " + item.Customer.LastName,
                    Number = item.Number,
                    RoomNumber = item.Room.RoomNumber
                });
            }

            return checkingDtos;
        }
    }
}