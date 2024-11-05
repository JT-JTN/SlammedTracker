using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ST.Core;
using ST.Infrastructure.Plugins;
using ST.Shared.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.DataAccess.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly STDataContext _context;
        private readonly ILogger<VehicleRepository> _logger;

        public VehicleRepository(STDataContext context, ILogger<VehicleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddVehicleAsync(STVehicle vehicle)
        {
            var vinCheck = await _context.Vehicles.AnyAsync(v => v.Vin == vehicle.Vin);
            if (vinCheck.Equals(true))
            {
                throw new Exception("VIN already exists");
            }

            try
            {
                await _context.Vehicles.AddAsync(vehicle);
                await _context.SaveChangesAsync();

                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Failed to add vehicle: {ex.Message}", ex.Message);
                throw new Exception("Failed to add vehicle");
            }
        }

        public async Task DeleteVehicleAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var vehicleToDelete = await _context.Vehicles.FindAsync(id);

            if (vehicleToDelete == null)
            {
                _logger.LogWarning("Vehicle to delete not found in database: {id}", id);
                throw new Exception("Vehicle not found");
            }

            try
            {
                _logger.LogInformation("Starting deletion of vehicle: {id}", id);
                _context.Vehicles.Remove(vehicleToDelete);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Finished deletion of vehicle: {id}", id);

                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Deletion of vehicle: {id} failed, {ex.Message}", id, ex.Message);
                throw new Exception("Failed to delete vehicle");
            }
        }

        public async Task EditVehicleAsync(int id, STVehicle vehicle)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle data invalid when calling update: {id} | {vehicle}", id, vehicle);
                throw new ArgumentOutOfRangeException(nameof(vehicle), "Vehicle data invalid");
            }

            if (id != vehicle.Id)
            {
                _logger.LogWarning("Vehicle id mismatch when calling update: {id}, {vehicle.Id}", id, vehicle.Id);
                throw new Exception("Vehicle Id mismatch");
            }

            var vehicleToUpdate = await _context.Vehicles.FindAsync(id);

            if (vehicleToUpdate == null)
            {
                _logger.LogWarning("Vehicle to update not found in database: {id}", id);
                throw new Exception("Vehicle not found");
            }

            if (vehicleToUpdate.Vin != vehicle.Vin)
            {
                var vinCheck = await _context.Vehicles.AnyAsync(v => v.Vin == vehicle.Vin);
                if (vinCheck.Equals(true))
                {
                    _logger.LogInformation("VIN already exists: {vehicle.Vin}", vehicle.Vin);
                    throw new Exception("VIN already exists");
                }
            }

            vehicleToUpdate.Vin = vehicle.Vin;
            vehicleToUpdate.CustomerId = vehicle.CustomerId;
            vehicleToUpdate.VYear = vehicle.VYear;
            vehicleToUpdate.VMake = vehicle.VMake;
            vehicleToUpdate.VModel = vehicle.VModel;
            vehicleToUpdate.VTrim = vehicle.VTrim;
            vehicleToUpdate.ColorId = vehicle.ColorId;

            try
            {
                _logger.LogInformation("Starting update of vehicle: {id}", id);
                _context.Vehicles.Update(vehicleToUpdate);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Finished update of vehicle: {id}", id);
                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Failed to update vehicle: {id}, {ex.Message}", id, ex.Message);
                throw new Exception("Failed to update vehicle");
            }
        }

        public async Task<IEnumerable<VehicleListDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Customer)
                .Include(v => v.Color)
                .Select(v => new VehicleListDto
                {
                    Id = v.Id,
                    CustomerId = v.CustomerId,
                    Vin = v.Vin,
                    VYear = v.VYear,
                    VMake = v.VMake,
                    VModel = v.VModel,
                    VTrim = v.VTrim,
                    ColorId = v.ColorId,
                    Customer = v.Customer,
                    Color = v.Color
                })
                .ToListAsync();

            return vehicles;
        }

        public async Task<VehicleDetailsDto> GetVehicleByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling details: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .Include(v => v.Color)
                .Where(v => v.Id == id)
                .Select(v => new VehicleDetailsDto
                {
                    Id = v.Id,
                    CustomerId = v.CustomerId,
                    Vin = v.Vin,
                    VYear = v.VYear,
                    VMake = v.VMake,
                    VModel = v.VModel,
                    VTrim = v.VTrim,
                    ColorId = v.ColorId,
                    Customer = v.Customer,
                    Color = v.Color
                })
                .FirstOrDefaultAsync();

            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle data invalid when calling details: {id} | {customer}", id, vehicle);
                throw new ArgumentOutOfRangeException(nameof(vehicle), "Vehicle data invalid");
            }

            return vehicle;
        }
    }
}
