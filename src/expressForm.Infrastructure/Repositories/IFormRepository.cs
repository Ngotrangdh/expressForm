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
        Form Delete(Form form);
        IEnumerable<Form> Find(Expression<Func<Form, bool>> predicate);
        Form Get(int id);
        Task<IEnumerable<Form>> GetAllAsync();
        void SaveChanges();
        Form Update(Form form);
    }
}