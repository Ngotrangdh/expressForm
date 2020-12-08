using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace expressForm.Core.Forms
{
     public interface IFormRepository
    {
        Form Add(Form form);
        Form Delete(int id);
        Task<Form> FindAsync(Expression<Func<Form, bool>> predicate);
        Form Get(int id);
        Task<IEnumerable<Form>> GetAllAsync();
        Task SaveChangesAsync();
        Form Update(Form form);
        bool Any(int id);
    }
}
