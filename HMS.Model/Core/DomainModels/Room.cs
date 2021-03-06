﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Model.Core.DomainModels.Base;

namespace HMS.Model.Core.DomainModels
{
    public class Room : ObjectModel
    {
        [DisplayName("شماره اتاق")]
        public string RoomNumber { get; set; }

        [DisplayName("امتیاز"), Range(0, 5)]
        public int? Rate { get; set; }

        [DisplayName("توضیحات بیشتر")]
        public string Description { get; set; }


        public Guid? FacilityId { get; set; }
        public RoomFacility Facility { get; set; }

        public Guid HotelId { get; set; }
        public HotelData Hotel { get; set; }

        public List<RoomImage> Images { get; set; }

        public List<Reserve_Room> Room_Reserve_Rooms { get; set; }
        public List<Checking> Checkings { get; set; }
        public ICollection<RoomPrice> RoomPrices { get; set; }
    }
}
