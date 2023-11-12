using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FastFoodResturant.Contexts;
using FastFoodResturant.Models;
using System.Linq.Expressions;
using System.Drawing;
using FastFoodResturant.ViewModels.ListViewModels;

namespace FastFoodResturant.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext dbcon;

        public CategoriesController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }
        public async Task<IActionResult> Index(string searchterm , string orderBy = "", int CurrentPage = 1)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            { 
                CategoryListViewModel ObjModel = new();
                ObjModel.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
                var IndexList = dbcon.Category.Where(_ => _.IsAvailable == true
                            && _.CategoryName.Contains(searchterm) || searchterm == null);

                switch (orderBy)
                {
                    case "name_desc":
                        IndexList = IndexList.OrderBy(a => a.CategoryName);
                        break;
                    default:
                        IndexList = IndexList.OrderBy(a => a.CategoryId);
                        break;
                }
                int TotalRecords = IndexList.Count();
                int PageSize = 5;
                int TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
                IndexList = IndexList.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                ObjModel.CategoryList = IndexList;
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
        public async Task<IActionResult> Create([FromForm] Category category, IFormFile categoryImageData)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                   var CheckName = await dbcon.Category.FirstOrDefaultAsync(_=>_.CategoryName == category.CategoryName)   ;
                    if(CheckName == null)
                    {
                        category.IsAvailable = true;
                        category.CreatedAt = DateTime.UtcNow;
                        if (categoryImageData != null && categoryImageData.Length > 0)
                        {
                            category.CategoryImage = Helpers.ImageConverter.ConvertToByteArray(categoryImageData);
                        }
                        dbcon.Category.Add(category);
                        await dbcon.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ViewBag.Message = "Category Added Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Category Already Exist";
                        await transaction.RollbackAsync();
                    }
                }
                else
                {
                    ViewBag.Message = "Category Not Added";
                    await transaction.RollbackAsync();
                }
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || dbcon.Category == null)
            {
                return NotFound();
            }

            var category = await dbcon.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] Category category, IFormFile categoryImageData)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {    var FindCategory = await dbcon.Category.FirstOrDefaultAsync(_=>_.CategoryId == id
                                                            && _.IsAvailable == true);
                if (FindCategory!=null)
                {
                    FindCategory.UpdatedAt = DateTime.UtcNow;
                    FindCategory.CategoryName = category.CategoryName;
                    FindCategory.CategoryDescription = category.CategoryDescription;
                    if (categoryImageData != null && categoryImageData.Length > 0)
                    {
                        FindCategory.CategoryImage = Helpers.ImageConverter.ConvertToByteArray(categoryImageData);
                    }
                    dbcon.Category.Update(FindCategory);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "Category Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.Category == null)
            {
                ViewBag.Message = "Category Not Found";
                return NotFound();
            }

            var category = await dbcon.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id && m.IsAvailable == true);
            if (category == null)
            {
                ViewBag.Message = "Category Not Found";
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var category = await dbcon.Category.FirstOrDefaultAsync(m => m.CategoryId == id && m.IsAvailable == true);
                if (category != null)
                {
                    category.IsAvailable = false;
                    category.UpdatedAt = DateTime.UtcNow;
                    dbcon.Category.Update(category);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "Category Not Found";
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
