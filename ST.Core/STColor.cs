using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ST.Core
{
    public class STColor
    {
        public int Id { get; set; }
        public string CName { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<STVehicle>? Vehicles { get; set; }
    }
}
