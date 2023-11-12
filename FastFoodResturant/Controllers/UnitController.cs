using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodResturant.Contexts;
using FastFoodResturant.Models;

namespace FastFoodResturant.Controllers
{
    public class UnitController : Controller
    {
        private readonly DataContext dbcon;

        public UnitController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public async Task<IActionResult> Index(string searchterm)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                return View(await dbcon.UnitMaster.Where(_ => _.IsAvailable == true
                    && _.UnitName.Contains(searchterm) || searchterm == null).ToListAsync());
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
        public async Task<IActionResult> Create([FromForm] UnitMaster unitMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    var CheckName = await dbcon.UnitMaster.FirstOrDefaultAsync(_ => _.UnitName == unitMaster.UnitName);
                    if (CheckName == null)
                    {
                        unitMaster.IsAvailable = true;
                        unitMaster.CreatedAt = DateTime.UtcNow;
                        dbcon.UnitMaster.Add(unitMaster);
                        await dbcon.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ViewBag.Message = "Unit Added Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Unit Already Exist";
                        await transaction.RollbackAsync();
                    }
                }
                else
                {
                    ViewBag.Message = "Unit Not Added";
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
            if (id == null || dbcon.UnitMaster == null)
            {
                return NotFound();
            }

            var unitMaster = await dbcon.UnitMaster.FindAsync(id);
            if (unitMaster == null)
            {
                return NotFound();
            }
            return View(unitMaster);
        }                                                                     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] UnitMaster unitMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FindUnit = await dbcon.UnitMaster.FirstOrDefaultAsync(_ => _.UnitId == id
                                                            && _.IsAvailable == true);
                if (FindUnit != null)
                {
                    FindUnit.UpdatedAt = DateTime.UtcNow;
                    FindUnit.UnitName = unitMaster.UnitName;
                    dbcon.UnitMaster.Update(FindUnit);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "Unit Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return View(unitMaster);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.UnitMaster == null)
            {
                ViewBag.Message = "Unit Not Found";
                return NotFound();
            }
            var UnitMaster = await dbcon.UnitMaster
                .FirstOrDefaultAsync(m => m.UnitId == id && m.IsAvailable == true);
            if (UnitMaster == null)
            {
                ViewBag.Message = "Unit Not Found";
                return NotFound();
            }
            return View(UnitMaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var UnitMaster = await dbcon.UnitMaster.FirstOrDefaultAsync(m => m.UnitId == id && m.IsAvailable == true);
                if (UnitMaster != null)
                {
                    UnitMaster.IsAvailable = false;
                    UnitMaster.UpdatedAt = DateTime.UtcNow;
                    dbcon.UnitMaster.Update(UnitMaster);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "Unit Not Found";
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
