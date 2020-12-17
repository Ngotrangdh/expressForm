using expressForm.Core.Forms;
using expressForm.Shared.Utilities.Extensions;
using expressForm.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expressForm.Web.Controllers
{
    public class FormsController : Controller
    {
        private readonly IFormRepository _repository;

        public FormsController(IFormRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Form> forms = await _repository.GetAllAsync();
            return View(forms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _repository.FindAsync(id.Value);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description")] FormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var form = new Form(viewModel.Title, viewModel.Description.ToStringOrEmpty());
                _repository.Add(form);
                await _repository.SaveChangesAsync();
                return RedirectToRoute("questions", new { formId = form.Id});
            }
            return View();
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _repository.FindAsync(id.Value);
            if (form == null)
            {
                return NotFound();
            }
            return View(new FormViewModel
            {
                Id = form.Id,
                Title = form.Title,
                Description = form.Description
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] FormViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var form = new Form(id, viewModel.Title, viewModel.Description.ToStringOrEmpty());
                    _repository.Update(form);
                    await _repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FormExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _repository.FindAsync(id.Value);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                if (!await FormExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        private async Task<bool> FormExistsAsync(int id)
        {
            var form = await _repository.FindAsync(id);
            return form != null;
        }
    }
}
