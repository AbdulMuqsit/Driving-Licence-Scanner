using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingLicenceScanner.Entities.Infrastructure;

namespace DrivingLicenceScanner.Entities
{
    public class LegalAge : ObjectBase
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Age { get; set; }
    }
}
