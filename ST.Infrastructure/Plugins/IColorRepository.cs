using ST.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Infrastructure.Plugins
{
    public interface IColorRepository
    {
        Task<IEnumerable<STColor>> GetAllColorsAsync();
        Task<STColor> GetColorByIdAsync(int id);
        Task AddColorAsync(STColor color);
        Task EditColorAsync(int id, STColor color);
        Task DeleteColorAsync(int id);
    }
}
