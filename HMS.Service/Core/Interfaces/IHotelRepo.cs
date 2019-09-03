using System;
using System.Collections.Generic;
using HMS.Model.Core.DomainModels;
using HMS.Model.Core.DTOs.Hotel;

namespace HMS.Service.Core.Interfaces
{
    public interface IHotelRepo : IRepository<HotelData>
    {
        bool Remove_Hotel(Guid? Id);

        List<GetHotelsDto> Read_Hotel();

        void Create_Hotel(CreateHotelDto model);

        void Update_Hotel(CreateHotelDto model, Guid Id);
    }
}