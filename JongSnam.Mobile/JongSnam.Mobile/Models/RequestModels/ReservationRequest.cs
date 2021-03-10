using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class ReservationRequest
    {
        public int StoreId { get; set; }

        public int UserId { get; set; }

        public int FieldId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }
    }
}
