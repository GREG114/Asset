using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LxGreg.Data;
using LxGreg.Models;

namespace LxGreg.Controllers.Asset
{
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.items.Include(i => i.store);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .Include(i => i.store)
                .FirstOrDefaultAsync(m => m.ItemNumber == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public string GetNewItemNumber(int storeid)
        {
            var haved = _context.items.Where(c=>c.storeId==storeid).OrderByDescending(c=>c.storeId);
            if (haved.Count() > 0)
            {
                var shortnow = Convert.ToInt32(haved.First().ItemShortNumber) + 1;
                return storeid+"."+shortnow.ToString().PadLeft(4, '0');
            }
            return storeid + "." + "0001";
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["storeId"] = new SelectList(_context.stores, "Id", nameof(Store.StoreName));

            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemNumber,ItemName,Model,Mark,ItemShortNumber,storeId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", item.storeId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", item.storeId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemNumber,ItemName,Model,Mark,ItemShortNumber,storeId")] Item item)
        {
            if (id != item.ItemNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemNumber))
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
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", item.storeId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .Include(i => i.store)
                .FirstOrDefaultAsync(m => m.ItemNumber == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var item = await _context.items.FindAsync(id);
            _context.items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
            return _context.items.Any(e => e.ItemNumber == id);
        }
    }
}
