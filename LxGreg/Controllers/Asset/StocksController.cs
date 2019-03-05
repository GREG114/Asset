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
            var appDbContext = _context.stocks.Include(s => s.item).Include(s=>s.item.store).Include(s=>s.unit);
            return View(await appDbContext.ToListAsync());
        }

    
     
      

        private bool StockExists(int id)
        {
            return _context.stocks.Any(e => e.id == id);
        }
    }
}
