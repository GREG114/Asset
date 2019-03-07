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
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {

            _context = context;
            /*    if (_context.managers.Count() <5)
                {
                    _context.managers.Add(new Manager { Id = "liuchao1", Name = "管理员1" });
                 //   _context.SaveChanges();
                }*/
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.orders.Include(o => o.Operater).Include(o => o.Taker).Include(o => o.item).Include(o => o.unit);
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
                .Include(o => o.item)
                .Include(o => o.unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int stockid, bool take)
        {
            ViewBag.take = take;
            if (stockid != 0)
            {
                var stock = _context.stocks.Include(c => c.item).Include(c => c.unit).Where(c => c.id == stockid).First();
                if (stock != null)
                {
                    ViewBag.stock = stock;
                }
            }

            ViewData["OperaterId"] = new SelectList(_context.managers, "Id", nameof(Manager.Name));
            ViewData["TakerId"] = new SelectList(_context.managers, "Id", nameof(Manager.Name));
            ViewData["itemItemNumber"] = new SelectList(_context.items, "ItemNumber", "ItemNumber");
            ViewData["unitId"] = new SelectList(_context.units, "Id", nameof(Unit.UnitName));
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Mark,take,unitId,itemItemNumber,TakerId,OperaterId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderTime = DateTime.Now;
                var stock = _context.stocks.Include(c => c.item).Where(c => c.itemItemNumber == order.itemItemNumber && c.unitId == order.unitId);
                if (stock.Count() == 1)
                {
                    var targetstock = stock.First();
                    if (targetstock.CurrentQuantity < order.Quantity && order.take)
                    {
                        return Json($"库存不足，目标仓库：{targetstock.item.store.StoreName}，当前库存：{targetstock.CurrentQuantity}");
                    }
                    if (order.take)
                    {

                        targetstock.CurrentQuantity -= order.Quantity;
                    }
                    else
                    {
                        targetstock.CurrentQuantity += order.Quantity;
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
                            itemItemNumber = order.itemItemNumber,
                            unitId = order.unitId,
                            CurrentQuantity = order.Quantity
                        });
                    }
                }
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "stocks");
            }
            ViewData["OperaterId"] = new SelectList(_context.managers, "Id", "Id", order.OperaterId);
            ViewData["TakerId"] = new SelectList(_context.managers, "Id", "Id", order.TakerId);
            ViewData["itemItemNumber"] = new SelectList(_context.items, "ItemNumber", "ItemNumber", order.itemItemNumber);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", order.unitId);
            return View(order);
        }
        public IActionResult GetAsset(string str)
        {
            return Json(_context.items.Where(c => c.ItemName.Contains(str) ||
            c.ItemNumber.Contains(str) ||
            c.Model.Contains(str)
            ));
        }

        public IActionResult GetManager(string str)
        {
            var result = _context.managers.Where(
                c => c.Id.Contains(str) ||
                c.Name.Contains(str)
                );
            return Json(result);
        }

        public IActionResult Query(string usefor, string str)
        {
            try
            {
                switch (usefor)
                {
                    case "user":
                        var result = _context.managers.Where(
                            c => c.Id.Contains(str) ||
                            c.Name.Contains(str)
                            );
                        return Json(result);
                    case "item":
                        return Json(_context.items.Where(c => c.ItemName.Contains(str) ||
                        c.ItemNumber.Contains(str) ||
                        c.Model.Contains(str)
                        ));
                    default: return Json(null);
                }

            } catch(Exception ex)
            {
                return Json(ex);
            }
  
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
            ViewData["itemItemNumber"] = new SelectList(_context.items, "ItemNumber", "ItemNumber", order.itemItemNumber);
            ViewData["unitId"] = new SelectList(_context.units, "Id", "Id", order.unitId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,OrderTime,Quantity,Mark,take,unitId,itemItemNumber,TakerId,OperaterId")] Order order)
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
            ViewData["itemItemNumber"] = new SelectList(_context.items, "ItemNumber", "ItemNumber", order.itemItemNumber);
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
                .Include(o => o.item)
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
