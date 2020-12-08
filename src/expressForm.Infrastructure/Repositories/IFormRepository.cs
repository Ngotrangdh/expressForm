using expressForm.Core.Form;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace expressForm.Infrastructure.Repositories
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
    }
}