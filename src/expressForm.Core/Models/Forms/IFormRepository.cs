using System.Collections.Generic;
using System.Threading.Tasks;

namespace expressForm.Core.Models.Forms
{
    public interface IFormRepository
    {
        Form Add(Form form);
        Task<Form> DeleteAsync(int id);
        Task<Form> FindAsync(int id);
        Task<IEnumerable<Form>> GetAllAsync();
        Task SaveChangesAsync();
        Form Update(Form form);
    }
}
