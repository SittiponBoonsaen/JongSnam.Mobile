using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class UpdateFieldRequest : FieldRequest
    {
        public UpdateDiscountRequest UpdateDiscountRequest { get; set; }

        public ICollection<UpdatePictureFieldRequest> UpdatePictureFieldRequest { get; set; }
    }
}
