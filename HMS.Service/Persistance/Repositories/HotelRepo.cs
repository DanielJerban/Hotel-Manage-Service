using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Hotel;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class HotelRepo : Repository<HotelData>, IHotelRepo
    {
        public HotelRepo(ApplicationDbContext context) : base(context)
        {
        }

        public bool Remove_Hotel(Guid? Id)
        {
            if (Id == null)
            {
                return false;
            }

            var hotel = context.Hotels.SingleOrDefault(c => c.Id == Id);
            if (hotel == null)
            {
                return false;
            }

            context.Hotels.Remove(hotel);

            return true;
        }

        public List<GetHotelsDto> Read_Hotel()
        {
            var hotels = context.Hotels.ToList();
            var hotelDtos = new List<GetHotelsDto>();

            foreach (var hotel in hotels)
            {
                int roomsCount = 0;


                List<Room> rooms = context.Rooms.Where(c => c.HotelId == hotel.Id).ToList();

                if (rooms != null)
                {
                    roomsCount = rooms.Count;
                }


                hotelDtos.Add(new GetHotelsDto()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Rate = hotel.Rate,
                    Description = hotel.Description,
                    RoomsCount = roomsCount
                });
            }

            return hotelDtos;
        }

        public void Create_Hotel(CreateHotelDto model)
        {
            var hotel = new HotelData()
            {
                Name = model.Name,
                Rate = model.Rate,
                Description = model.Description
            };

            context.Hotels.Add(hotel);
        }

        public void Update_Hotel(CreateHotelDto model, Guid Id)
        {
            var hotel = context.Hotels.Find(Id);

            hotel.Name = model.Name;
            hotel.Rate = model.Rate;
            hotel.Description = model.Description;

            context.Entry(hotel).State = EntityState.Modified;
        }
    }
}