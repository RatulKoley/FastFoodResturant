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
using System.Drawing;

namespace FastFoodResturant.Controllers
{
    public class FoodMastersController : Controller
    {
        private readonly DataContext dbcon;

        public FoodMastersController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }

        // GET: FoodMasters
        public async Task<IActionResult> Index(string searchterm, string orderBy = "", int CurrentPage = 1)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                FoodMasterListViewModel ObjModel = new();
                ObjModel.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
                var IndexList = dbcon.FoodMaster.AsNoTracking().Include(_=>_.Category).Include(_=>_.GSTMaster)
                    .Where(_ => _.IsAvailable == true && _.FoodName.Contains(searchterm) || searchterm == null);

                switch (orderBy)
                {
                    case "name_desc":
                        IndexList = IndexList.OrderBy(a => a.FoodName);
                        break;
                    default:
                        IndexList = IndexList.OrderBy(a => a.FoodMasterId);
                        break;
                }
                int TotalRecords = IndexList.Count();
                int PageSize = 5;
                int TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
                IndexList = IndexList.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                ObjModel.FoodMasterList = IndexList;
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
            ViewData["CategoryId"] = new SelectList(dbcon.Category, "CategoryId", "CategoryName");
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FoodMaster foodMaster, IFormFile FoodImageData)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                    var CheckName = await dbcon.FoodMaster.FirstOrDefaultAsync(_ => _.FoodName == foodMaster.FoodName);
                    if (CheckName == null)
                    {
                        foodMaster.IsAvailable = true;
                        foodMaster.CreatedAt = DateTime.UtcNow;
                        if (FoodImageData != null && FoodImageData.Length > 0)
                        {
                            foodMaster.FoodImage = Helpers.ImageConverter.ConvertToByteArray(FoodImageData);
                        }
                        dbcon.FoodMaster.Add(foodMaster);
                        await dbcon.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ViewBag.Message = "FoodMaster Added Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "FoodMaster Already Exist";
                        await transaction.RollbackAsync();
                    }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                ViewBag.Message = "FoodMaster Not Added";
                throw;
            }
            ViewData["CategoryId"] = new SelectList(dbcon.Category, "CategoryId", "CategoryName", foodMaster.CategoryId);
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName", foodMaster.GSTId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || dbcon.FoodMaster == null)
            {
                return NotFound();
            }

            var foodMaster = await dbcon.FoodMaster.FindAsync(id);
            if (foodMaster == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(dbcon.Category, "CategoryId", "CategoryName", foodMaster.CategoryId);
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName", foodMaster.GSTId);
            return View(foodMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] FoodMaster foodMaster, IFormFile FoodImageData)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FindFood = await dbcon.FoodMaster.FirstOrDefaultAsync(_ => _.FoodMasterId == id
                                                            && _.IsAvailable == true);
                if (FindFood != null)
                {
                    FindFood.UpdatedAt = DateTime.UtcNow;
                    FindFood.FoodName = foodMaster.FoodName;
                    FindFood.FoodDescription = foodMaster.FoodDescription;
                    FindFood.FoodPrice = foodMaster.FoodPrice;
                    FindFood.CategoryId = foodMaster.CategoryId;
                    FindFood.GSTId = foodMaster.GSTId;
                    FindFood.FoodType = foodMaster.FoodType;
                    FindFood.IsVegan = foodMaster.IsVegan;
                    FindFood.IsGlutenFree = foodMaster.IsGlutenFree;
                    FindFood.IsSpicy = foodMaster.IsSpicy;
                    FindFood.Fat = foodMaster.Fat;
                    FindFood.Calories = foodMaster.Calories;
                    FindFood.Protein = foodMaster.Protein;
                    FindFood.PreparationTime = foodMaster.PreparationTime;
                    FindFood.Carbohydrates = foodMaster.Carbohydrates;
                    FindFood.Fiber = foodMaster.Fiber;
                    if (FoodImageData != null && FoodImageData.Length > 0)
                    {
                        FindFood.FoodImage = Helpers.ImageConverter.ConvertToByteArray(FoodImageData);
                    }
                    dbcon.FoodMaster.Update(FindFood);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "FoodMaster Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            ViewData["CategoryId"] = new SelectList(dbcon.Category, "CategoryId", "CategoryName", foodMaster.CategoryId);
            ViewData["GSTId"] = new SelectList(dbcon.GSTMaster, "GSTId", "GSTName", foodMaster.GSTId);
            return View(foodMaster);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.FoodMaster == null)
            {
                ViewBag.Message = "FoodMaster Not Found";
                return NotFound();
            }

            var foodMaster = await dbcon.FoodMaster
                .Include(f => f.Category)
                .Include(f => f.GSTMaster)
                .FirstOrDefaultAsync(m => m.FoodMasterId == id);
            if (foodMaster == null)
            {
                ViewBag.Message = "FoodMaster Not Found";
                return NotFound();
            }

            return View(foodMaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FoodFind = await dbcon.FoodMaster.FirstOrDefaultAsync(m => m.FoodMasterId == id && m.IsAvailable == true);
                if (FoodFind != null)
                {
                    FoodFind.IsAvailable = false;
                    FoodFind.UpdatedAt = DateTime.UtcNow;
                    dbcon.FoodMaster.Update(FoodFind);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "FoodMaster Not Found";
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
