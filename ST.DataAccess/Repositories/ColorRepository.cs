using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ST.Core;
using ST.Infrastructure.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.DataAccess.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly STDataContext _context;
        private readonly ILogger<ColorRepository> _logger;

        public ColorRepository(STDataContext context, ILogger<ColorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddColorAsync(STColor color)
        {
            var colorCheck = await _context.Colors.AnyAsync(c => c.CName == color.CName);
            if (colorCheck.Equals(true))
            {
                throw new Exception("Color already exists");
            }

            var colorToAdd = new STColor
            {
                CName = color.CName
            };

            try
            {
                await _context.Colors.AddAsync(colorToAdd);
                await _context.SaveChangesAsync();

                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Failed to add color: {ex.Message}", ex.Message);
                throw new Exception("Failed to add color");
            }
        }

        public async Task DeleteColorAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var colorToDelete = await _context.Colors.FindAsync(id);

            if (colorToDelete == null)
            {
                _logger.LogWarning("Color to delete not found in database: {id}", id);
                throw new Exception("Color not found");
            }

            try
            {
                _logger.LogInformation("Starting deletion of color: {id}", id);
                _context.Colors.Remove(colorToDelete);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Finished deletion of color: {id}", id);

                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Deletion of color: {id} failed: {ex.Message}", id, ex.Message);
                throw new Exception("Failed to color customer");
            }
        }

        public async Task EditColorAsync(int id, STColor color)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            if (color == null)
            {
                _logger.LogWarning("Color data invalid when calling update: {id} | {color}", id, color);
                throw new ArgumentOutOfRangeException(nameof(color), "Color data invalid");
            }

            if (id != color.Id)
            {
                _logger.LogWarning("Color id mismatch when calling update: {id}, {color.Id}", id, color.Id);
                throw new Exception("Color Id mismatch");
            }

            var colorToUpdate = await _context.Colors.FindAsync(id);

            if (colorToUpdate == null)
            {
                _logger.LogWarning("Color to update not found in database: {id}", id);
                throw new Exception("Color not found");
            }

            if (colorToUpdate.CName != color.CName)
            {
                var colorNameCheck = await _context.Colors.AnyAsync(c => c.CName == color.CName);
                if (colorNameCheck.Equals(true))
                {
                    _logger.LogInformation("Color name already exists: {color.CName}", color.CName);
                    throw new Exception("Color name already exists");
                }
            }

            colorToUpdate.CName = color.CName;

            try
            {
                _logger.LogInformation("Starting update of color: {id}", id);
                _context.Colors.Update(colorToUpdate);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Finished update of color: {id}", id);
                return;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning("Failed to update color: {id}, {ex.Message}", id, ex.Message);
                throw new Exception("Failed to update color");
            }
        }

        public async Task<IEnumerable<STColor>> GetAllColorsAsync()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<STColor> GetColorByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id out of range when calling update: {id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");
            }

            var color = await _context.Colors.FindAsync(id);

            if (color == null)
            {
                _logger.LogWarning("Color data invalid when calling update: {id} | {customer}", id, color);
                throw new ArgumentOutOfRangeException(nameof(color), "Color data invalid");
            }

            return color;
        }
    }
}
