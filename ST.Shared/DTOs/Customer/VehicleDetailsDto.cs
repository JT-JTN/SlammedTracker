using ST.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ST.Shared.DTOs.Customer
{
    public class VehicleDetailsDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public required string Vin { get; set; }
        public required int VYear { get; set; }
        public required string VMake { get; set; }
        public string? VModel { get; set; }
        public string? VTrim { get; set; }
        public int ColorId { get; set; }

        [ForeignKey("CustomerId")]
        public STCustomer? Customer { get; set; }

        [ForeignKey("ColorId")]
        public STColor? Color { get; set; }
    }
}
