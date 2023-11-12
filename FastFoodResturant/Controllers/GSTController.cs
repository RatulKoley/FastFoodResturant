using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodResturant.Contexts;
using FastFoodResturant.Models;
using System.Drawing;

namespace FastFoodResturant.Controllers
{
    public class GSTController : Controller
    {
        private readonly DataContext dbcon;

        public GSTController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public async Task<IActionResult> Index(string searchterm)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                return View(await dbcon.GSTMaster.Where(_ => _.IsAvailable == true
                && _.GSTName.Contains(searchterm) || searchterm == null).ToListAsync());
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] GSTMaster gSTMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    var CheckName = await dbcon.GSTMaster.FirstOrDefaultAsync(_ => _.GSTName == gSTMaster.GSTName);
                    if (CheckName == null)
                    {
                        gSTMaster.IsAvailable = true;
                        gSTMaster.CreatedAt = DateTime.UtcNow;
                        dbcon.GSTMaster.Add(gSTMaster);
                        await dbcon.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ViewBag.Message = "GST Added Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "GST Already Exist";
                        await transaction.RollbackAsync();
                    }
                }
                else
                {
                    ViewBag.Message = "GST Not Added";
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || dbcon.GSTMaster == null)
            {
                return NotFound();
            }

            var gSTMaster = await dbcon.GSTMaster.FindAsync(id);
            if (gSTMaster == null)
            {
                return NotFound();
            }
            return View(gSTMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] GSTMaster gSTMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FindGST = await dbcon.GSTMaster.FirstOrDefaultAsync(_ => _.GSTId == id
                                                            && _.IsAvailable == true);
                if (FindGST != null)
                {
                    FindGST.UpdatedAt = DateTime.UtcNow;
                    FindGST.GSTName = gSTMaster.GSTName;
                    FindGST.GSTPercentage = gSTMaster.GSTPercentage;
                    dbcon.GSTMaster.Update(FindGST);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "GST Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return View(gSTMaster);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.GSTMaster == null)
            {
                ViewBag.Message = "GST Not Found";
                return NotFound();
            }

            var gstMaster = await dbcon.GSTMaster
                .FirstOrDefaultAsync(m => m.GSTId == id && m.IsAvailable == true);
            if (gstMaster == null)
            {
                ViewBag.Message = "GST Not Found";
                return NotFound();
            }

            return View(gstMaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var gstMaster = await dbcon.GSTMaster.FirstOrDefaultAsync(m => m.GSTId == id && m.IsAvailable == true);
                if (gstMaster != null)
                {
                    gstMaster.IsAvailable = false;
                    gstMaster.UpdatedAt = DateTime.UtcNow;
                    dbcon.GSTMaster.Update(gstMaster);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "GST Not Found";
                    return NotFound();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
