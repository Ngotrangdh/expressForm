using expressForm.Core.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace expressForm.Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository() : this(new ApplicationDbContext())
        { }

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Form Add(Form form)
        {
            return _context.Add(form).Entity;
        }

        public Form Get(int id)
        {
            return _context.Find<Form>(id);
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            return await _context.Set<Form>().ToListAsync();
        }

        public Form Update(Form form)
        {
            var entity = _context.Forms.Find(form.Id);
            _context.Entry(entity).CurrentValues.SetValues(form);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Form> FindAsync(Expression<Func<Form, bool>> predicate)
        {
            return await _context.Set<Form>().AsQueryable().SingleOrDefaultAsync(predicate);
        }

        public Form Delete(int id)
        {
            var form = _context.Forms.Single(form => form.Id == id);
            return _context.Remove(form).Entity;
        }
    }
}
