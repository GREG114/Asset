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
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.orders.Include(o => o.Operater).Include(o => o.Taker).Include(o => o.asset).Include(o => o.store).Include(o => o.unit);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.orders
                .Include(o => o.Operater)
                .Include(o => o.Taker)
                .Include(o => o.asset)
                .Include(o => o.store)
                .Include(o => o.unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int stockid,bool take)
        {
           
            ViewBag.take = take;
            var stock = _context.stocks.Find(stockid);
            if (stock != null)
            {
                ViewBag.stock = stock;
            }
            ViewData["OperaterId"] = new SelectList(_context.managers, "Id", nameof(Manager.Name));
            ViewData["TakerId"] = new SelectList(_context.managers, "Id", nameof(Manager.Name));
            ViewData["assetId"] = new SelectList(_context.assets, "Id", nameof(Asset.ItemNumber));
            ViewData["storeId"] = new SelectList(_context.stores, "Id", nameof(Store.StoreName));
            ViewData["unitId"] = new SelectList(_context.units, "Id", nameof(Unit.UnitName));
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OperaterId,storeId,unitId,assetId,Quntity,TakerId,Mark,take")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderTime = DateTime.Now;
                var stock = _context.stocks.Where(c => c.storeId == order.storeId && c.assetId == order.assetId && c.unitId == order.unitId);
                if (stock.Count() == 1)
                {
                    var targetstock = stock.Include(c=>c.store).First();
                    
                    if (targetstock.CurrentQuntity<order.Quntity&&order.take)
                    {
                        return Json($"库存不足，目标仓库：{targetstock.store.StoreName}，当前库存：{targetstock.CurrentQuntity}");
                    }
                    if (order.take)
                    {
                        targetstock.CurrentQuntity -= order.Quntity;
                    }
                    else
                    {
                        targetstock.CurrentQuntity += order.Quntity;
                    }
                 
                   
                    _context.stocks.Update(targetstock);

                }
                else
                {
                    if (order.take)
                    {
                        return Json($"仍未建立此库");
                    }
                    else
                    {
                        _context.stocks.Add(new Stock
                        {
                            assetId = order.assetId,
                            storeId = order.storeId,
                            unitId = order.unitId,
                            CurrentQuntity = order.Quntity
                        });
                    }
                  
                }
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Stocks");
            }
            ViewData["OperaterId"] = new SelectList(_context.managers, "Id", "Id", order.OperaterId);
            ViewData["TakerId"] = new SelectList(_context.managers, "Id", "Id", order.TakerId);
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id", order.assetId);
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", order.storeId);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", order.unitId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OperaterId"] = new SelectList(_context.managers, "Id", "Id", order.OperaterId);
            ViewData["TakerId"] = new SelectList(_context.managers, "Id", "Id", order.TakerId);
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id", order.assetId);
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", order.storeId);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", order.unitId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,OrderTime,OperaterId,storeId,unitId,assetId,Quntity,TakerId,Mark")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["OperaterId"] = new SelectList(_context.managers, "Id", "Id", order.OperaterId);
            ViewData["TakerId"] = new SelectList(_context.managers, "Id", "Id", order.TakerId);
            ViewData["assetId"] = new SelectList(_context.assets, "Id", "Id", order.assetId);
            ViewData["storeId"] = new SelectList(_context.stores, "Id", "Id", order.storeId);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", order.unitId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.orders
                .Include(o => o.Operater)
                .Include(o => o.Taker)
                .Include(o => o.asset)
                .Include(o => o.store)
                .Include(o => o.unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.orders.FindAsync(id);
            _context.orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.orders.Any(e => e.Id == id);
        }
    }
}
