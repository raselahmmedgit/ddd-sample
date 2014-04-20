using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domain
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public bool IsDelete { get; set; }

        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        //ActionLink
        public string ActionLink { get; set; }
    }
}
