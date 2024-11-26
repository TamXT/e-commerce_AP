using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_AP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce_AP.Models.EF;

namespace e_commerce_AP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscribesController : Controller
    {
        private readonly ECommerce_APDbContext _context;

        public SubscribesController(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Subscribes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbSubscribes.ToListAsync());
        }

        // GET: Admin/Subscribes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSubscribe = await _context.TbSubscribes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbSubscribe == null)
            {
                return NotFound();
            }

            return View(tbSubscribe);
        }

        // GET: Admin/Subscribes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Subscribes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,CreatedDate")] TbSubscribe tbSubscribe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbSubscribe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbSubscribe);
        }

        // GET: Admin/Subscribes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSubscribe = await _context.TbSubscribes.FindAsync(id);
            if (tbSubscribe == null)
            {
                return NotFound();
            }
            return View(tbSubscribe);
        }

        // POST: Admin/Subscribes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,CreatedDate")] TbSubscribe tbSubscribe)
        {
            if (id != tbSubscribe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbSubscribe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbSubscribeExists(tbSubscribe.Id))
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
            return View(tbSubscribe);
        }

        // GET: Admin/Subscribes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSubscribe = await _context.TbSubscribes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbSubscribe == null)
            {
                return NotFound();
            }

            return View(tbSubscribe);
        }

        // POST: Admin/Subscribes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbSubscribe = await _context.TbSubscribes.FindAsync(id);
            if (tbSubscribe != null)
            {
                _context.TbSubscribes.Remove(tbSubscribe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbSubscribeExists(int id)
        {
            return _context.TbSubscribes.Any(e => e.Id == id);
        }
    }
}
