using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class FieldRequest
    {
        public int StoreId { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public int Price { get; set; }

        public bool IsOpen { get; set; }
    }
}
