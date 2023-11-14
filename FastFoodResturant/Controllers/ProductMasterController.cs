using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodResturant.Contexts;
using FastFoodResturant.Models;
using FastFoodResturant.ViewModels.ListViewModels;
using Microsoft.CodeAnalysis;

namespace FastFoodResturant.Controllers
{
    public class ProductMasterController : Controller
    {
        private readonly DataContext dbcon;

        public ProductMasterController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public async Task<IActionResult> Index(string searchterm, string orderBy = "", int CurrentPage = 1)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                ProductMasterListViewModel ObjModel = new();
                ObjModel.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
                var IndexList = dbcon.ProductMaster.AsNoTracking().Include(_ => _.UnitMaster).Include(_ => _.ProductStock).Include(_ => _.GSTMaster)
                    .Where(_ => _.IsAvailable == true && _.ProductName.Contains(searchterm) || searchterm == null);

                switch (orderBy)
                {
                    case "name_desc":
                        IndexList = IndexList.OrderBy(a => a.ProductName);
                        break;
                    default:
                        IndexList = IndexList.OrderBy(a => a.ProductId);
                        break;
                }
                int TotalRecords = IndexList.Count();
                int PageSize = 5;
                int TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
                IndexList = IndexList.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                ObjModel.ProductMasterList = IndexList;
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
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName");
            ViewData["UnitId"] = new SelectList(dbcon.UnitMaster, "UnitId", "UnitName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductMaster productMaster, IFormFile ProductImageData)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var CheckName = await dbcon.ProductMaster.FirstOrDefaultAsync(_ => _.ProductName == productMaster.ProductName);
                if (CheckName == null)
                {
                    long? ProductId = await dbcon.ProductMaster.Select(p => (long?)p.ProductId).MaxAsync();
                    productMaster.IsAvailable = true;
                    productMaster.CreatedAt = DateTime.UtcNow;
                    if (ProductImageData != null && ProductImageData.Length > 0)
                    {
                        productMaster.ProductImage = Helpers.ImageConverter.ConvertToByteArray(ProductImageData);
                    }

                    ProductStock newStockEntry = new()
                    {
                         ProductId = ProductId.GetValueOrDefault() + 1,
                         StockQuantity = 0,
                         IsAvailable = true,
                         CreatedAt = DateTime.UtcNow
                    };
                    dbcon.ProductStock.Add(newStockEntry);
                    productMaster.StockId = await dbcon.ProductStock.OrderBy(_=>_.ProductStockId).Select(_=>_.ProductStockId).FirstOrDefaultAsync();
                    dbcon.ProductMaster.Add(productMaster);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "Product Added Successfully";
                }
                else
                {
                    ViewBag.Message = "Product Already Exist";
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ViewBag.Message = "Product Not Added";
                throw;
            }
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName", productMaster.GSTId);
            ViewData["UnitId"] = new SelectList(dbcon.UnitMaster, "UnitId", "UnitName", productMaster.UnitId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || dbcon.ProductMaster == null)
            {
                return NotFound();
            }

            var productMaster = await dbcon.ProductMaster.FindAsync(id);
            if (productMaster == null)
            {
                return NotFound();
            }
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName", productMaster.GSTId);
            ViewData["UnitId"] = new SelectList(dbcon.UnitMaster, "UnitId", "UnitName", productMaster.UnitId);
            return View(productMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] ProductMaster productMaster, IFormFile ProductImageData)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FindProduct = await dbcon.ProductMaster.FirstOrDefaultAsync(_ => _.ProductId == id
                                                            && _.IsAvailable == true);
                if (FindProduct != null)
                {
                    FindProduct.UpdatedAt = DateTime.UtcNow;
                    FindProduct.ProductName = productMaster.ProductName;
                    FindProduct.ProductPrice = productMaster.ProductPrice;
                    FindProduct.ProductDescription = productMaster.ProductDescription;
                    FindProduct.UnitId = productMaster.UnitId;
                    FindProduct.GSTId = productMaster.GSTId;
                    if (ProductImageData != null && ProductImageData.Length > 0)
                    {
                        FindProduct.ProductImage = Helpers.ImageConverter.ConvertToByteArray(ProductImageData);
                    }
                    dbcon.ProductMaster.Update(FindProduct);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "Product Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName", productMaster.GSTId);
            ViewData["UnitId"] = new SelectList(dbcon.UnitMaster, "UnitId", "UnitName", productMaster.UnitId);
            return View(productMaster);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.ProductMaster == null)
            {
                return NotFound();
            }

            var productMaster = await dbcon.ProductMaster
                .Include(p => p.GSTMaster)
                .Include(p => p.UnitMaster)
                .Include(_ => _.ProductStock)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productMaster == null)
            {
                return NotFound();
            }

            return View(productMaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FoodProduct = await dbcon.ProductMaster.FirstOrDefaultAsync(m => m.ProductId == id && m.IsAvailable == true);
                if (FoodProduct != null)
                {
                    FoodProduct.IsAvailable = false;
                    FoodProduct.UpdatedAt = DateTime.UtcNow;
                    dbcon.ProductMaster.Update(FoodProduct);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "Product Not Found";
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
