using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LxGreg.Data;
using LxGreg.Models;

namespace LxGreg.Controllers
{
    public class StocksController : Controller
    {
        private readonly AppDbContext _context;

        public StocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.stocks.Include(s => s.asset).Include(s => s.store).Include(s => s.unit);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.stocks
                .Include(s => s.asset)
                .Include(s => s.store)
                .Include(s => s.unit)
                .FirstOrDefaultAsync(m => m.id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id");
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id");
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,storeId,unitId,assetId,CurrentQuntity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id", stock.assetId);
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", stock.storeId);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", stock.unitId);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id", stock.assetId);
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", stock.storeId);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", stock.unitId);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,storeId,unitId,assetId,CurrentQuntity")] Stock stock)
        {
            if (id != stock.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.id))
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
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id", stock.assetId);
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", stock.storeId);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", stock.unitId);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.stocks
                .Include(s => s.asset)
                .Include(s => s.store)
                .Include(s => s.unit)
                .FirstOrDefaultAsync(m => m.id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.stocks.FindAsync(id);
            _context.stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
            return _context.stocks.Any(e => e.id == id);
        }
    }
}
