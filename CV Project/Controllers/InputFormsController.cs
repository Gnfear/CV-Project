using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CV_Project.Context;
using CV_Project.Models;

namespace CV_Project.Views
{
    public class InputFormsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InputFormsController(ApplicationDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: InputForms
        public async Task<IActionResult> Index()
        {
              return _context.input != null ? 
                          View(await _context.input.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.input'  is null.");
        }

        // GET: InputForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.input == null)
            {
                return NotFound();
            }

            var inputForm = await _context.input
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputForm == null)
            {
                return NotFound();
            }

            return View(inputForm);
        }

        // GET: InputForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InputForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] InputForm inputForm)
        {
            if (ModelState.IsValid)
            {
                if (inputForm.ProfilePicture != null && inputForm.ProfilePicture.Length > 0)
                {
                    string folder = "ProfilPhoto\\";
                    folder += inputForm.ProfilePicture.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await inputForm.ProfilePicture.CopyToAsync(new FileStream(serverFolder, FileMode.Create)); 
                    inputForm.ProfilePictureUrl = "\\" + folder;
                }
                inputForm.Created_on = DateTime.Now;
                

                _context.Add(inputForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inputForm);
        }

        // GET: InputForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.input == null)
            {
                return NotFound();
            }

            var inputForm = await _context.input.FindAsync(id);
            if (inputForm == null)
            {
                return NotFound();
            }
            return View(inputForm);
        }

        // POST: InputForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] int id, InputForm inputForm)
        {
            if (id != inputForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (inputForm.ProfilePicture != null && inputForm.ProfilePicture.Length > 0)
                    {
                        string folder = "ProfilPhoto\\";
                        folder += inputForm.ProfilePicture.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                        await inputForm.ProfilePicture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        inputForm.ProfilePictureUrl = "\\" + folder;
                    }

                    _context.Update(inputForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InputFormExists(inputForm.Id))
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
            return View(inputForm);
        }

        // GET: InputForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.input == null)
            {
                return NotFound();
            }

            var inputForm = await _context.input
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputForm == null)
            {
                return NotFound();
            }

            return View(inputForm);
        }

        // POST: InputForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.input == null)
            {
                return Problem("Entity set 'ApplicationDBContext.input'  is null.");
            }
            var inputForm = await _context.input.FindAsync(id);
            if (inputForm != null)
            {
                _context.input.Remove(inputForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InputFormExists(int id)
        {
          return (_context.input?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
