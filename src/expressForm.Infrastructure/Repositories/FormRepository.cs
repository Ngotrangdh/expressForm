﻿using expressForm.Core.Models.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace expressForm.Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Form Add(Form form)
        {
            return _context.Add(form).Entity;
        }

        public Task<Form> FindAsync(int id)
        {
            return FindAsync(form => form.Id == id);
        }

        public Task<Form> FindAsync(Guid guid)
        {
            return FindAsync(form =>  form.Guid == guid);
        }

        private async Task<Form> FindAsync(Expression<Func<Form, bool>> predicate)
        {
            return await _context.Forms
                .Include(f => f.Questions)
                .Include(f => f.Responses)
                    .ThenInclude(r => r.Answers)
                .SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            return await _context.Set<Form>().ToListAsync();
        }

        public Form Update(Form form)
        {
            form.UpdatedDate = DateTime.Now;
            return _context.Update(form).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Form> DeleteAsync(int id)
        {
            var form = await FindAsync(id);
            return _context.Remove(form).Entity;
        }
    }
}
