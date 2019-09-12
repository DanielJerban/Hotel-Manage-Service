using HMS.Model.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Core.DTOs.Reservation
{
    public class CreateReserveDto
    {
        public Guid customerId { get; set; }
        public List<string> roomsId { get; set; }

        [Required (ErrorMessage = "")]
        public string fromDate { get; set; }

        [Required (ErrorMessage = "")]
        public string toDate { get; set; }

        [Required(ErrorMessage = "")]
        public string Status { get; set; }
    }
}
