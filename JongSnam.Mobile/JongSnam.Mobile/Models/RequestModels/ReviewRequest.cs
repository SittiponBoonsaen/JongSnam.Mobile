using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class ReviewRequest
    {
        public int StoreId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public decimal Rating { get; set; }
    }
}
