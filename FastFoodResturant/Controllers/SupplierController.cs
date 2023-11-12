using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodResturant.Contexts;
using FastFoodResturant.Models;
using FastFoodResturant.Helpers;
using FastFoodResturant.ViewModels.ListViewModels;
using System.Drawing.Printing;

namespace FastFoodResturant.Controllers
{
    public class SupplierController : Controller
    {
        private readonly DataContext dbcon;

        public SupplierController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public async Task<IActionResult> Index(string searchterm, string orderBy = "", int CurrentPage = 1)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                SupplierListViewModel ObjModel = new();
                ObjModel.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
                var IndexList = dbcon.SupplierMaster.Where(_ => _.IsAvailable == true
                && _.SupplierName.Contains(searchterm) || searchterm == null);

                switch (orderBy)
                {
                    case "name_desc":
                        IndexList = IndexList.OrderBy(a => a.SupplierName);
                        break;
                    default:
                        IndexList = IndexList.OrderBy(a => a.SupplierId);
                        break;
                }
                int TotalRecords = IndexList.Count();
                int PageSize = 5;
                int TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
                IndexList = IndexList.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                ObjModel.SupplierMasterList = IndexList;
                ObjModel.CurrentPage = CurrentPage;
                ObjModel.TotalPage = TotalPages;
                ObjModel.PageSize = PageSize;
                ObjModel.Term = searchterm;
                ObjModel.OrderBy = orderBy;

                return View(ObjModel);
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
        public async Task<IActionResult> Create([FromForm] SupplierMaster supplierMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    var CheckName = await dbcon.SupplierMaster.FirstOrDefaultAsync(_ => _.SupplierName == supplierMaster.SupplierName);
                    if (CheckName == null)
                    {
                        supplierMaster.IsAvailable = true;
                        supplierMaster.CreatedAt = DateTime.UtcNow;
                        dbcon.SupplierMaster.Add(supplierMaster);
                        await dbcon.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ViewBag.Message = "Supplier Added Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Supplier Already Exist";
                        await transaction.RollbackAsync();
                    }
                }
                else
                {
                    ViewBag.Message = "Supplier Not Added";
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
            if (id == null || dbcon.SupplierMaster == null)
            {
                return NotFound();
            }

            var supplierMaster = await dbcon.SupplierMaster.FindAsync(id);
            if (supplierMaster == null)
            {
                return NotFound();
            }
            return View(supplierMaster);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] SupplierMaster supplierMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FindSupplier = await dbcon.SupplierMaster.FirstOrDefaultAsync(_ => _.SupplierId == id
                                                            && _.IsAvailable == true);
                if (FindSupplier != null)
                {
                    FindSupplier.UpdatedAt = DateTime.UtcNow;
                    FindSupplier.SupplierName = supplierMaster.SupplierName;
                    FindSupplier.SupplierAddress = supplierMaster.SupplierAddress;
                    FindSupplier.SupplierPhone = supplierMaster.SupplierPhone;
                    FindSupplier.SupplierEmail = supplierMaster.SupplierEmail;
                    dbcon.SupplierMaster.Update(FindSupplier);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "Supplier Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return View(supplierMaster);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.SupplierMaster == null)
            {
                ViewBag.Message = "Supplier Not Found";
                return NotFound();
            }

            var SupplierMaster = await dbcon.SupplierMaster
                .FirstOrDefaultAsync(m => m.SupplierId == id && m.IsAvailable == true);
            if (SupplierMaster == null)
            {
                ViewBag.Message = "Supplier Not Found";
                return NotFound();
            }

            return View(SupplierMaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var SupplierMaster = await dbcon.SupplierMaster.FirstOrDefaultAsync(m => m.SupplierId == id && m.IsAvailable == true);
                if (SupplierMaster != null)
                {
                    SupplierMaster.IsAvailable = false;
                    SupplierMaster.UpdatedAt = DateTime.UtcNow;
                    dbcon.SupplierMaster.Update(SupplierMaster);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "Supplier Not Found";
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
