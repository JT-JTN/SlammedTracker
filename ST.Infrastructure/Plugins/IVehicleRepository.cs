using ST.Core;
using ST.Shared.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Infrastructure.Plugins
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<VehicleListDto>> GetAllVehiclesAsync();
        Task<VehicleDetailsDto> GetVehicleByIdAsync(int id);
        Task AddVehicleAsync(STVehicle vehicle);
        Task EditVehicleAsync(int id, STVehicle vehicle);
        Task DeleteVehicleAsync(int id);
    }
}
