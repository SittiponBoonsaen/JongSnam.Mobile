using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class AddFieldRequest
    {
        public FieldRequest FieldRequest { get; set; }
        public DiscountRequest DiscountRequest { get; set; }

        public ICollection<ImageFieldRequest> PictureFieldRequest { get; set; }
    }
}
