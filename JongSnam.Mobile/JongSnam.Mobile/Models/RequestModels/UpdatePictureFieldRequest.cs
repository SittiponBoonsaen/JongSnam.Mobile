using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class UpdatePictureFieldRequest : ImageFieldRequest
    {
        public int Id { get; set; }
    }
}
