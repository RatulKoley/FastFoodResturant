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

namespace FastFoodResturant.Controllers
{
    public class TableController : Controller
    {
        private readonly DataContext dbcon;

        public TableController(DataContext dbcon)
        {
            this.dbcon = dbcon;
        }

        public async Task<IActionResult> Index(string searchterm, string orderBy = "", int CurrentPage = 1)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                TableMasterListViewModel ObjModel = new();
                ObjModel.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "name_desc" : "";
                var IndexList = dbcon.TableMaster.Where(_ => _.IsAvailable == true
                && _.TableName.Contains(searchterm) || searchterm == null);

                switch (orderBy)
                {
                    case "name_desc":
                        IndexList = IndexList.OrderBy(a => a.TableName);
                        break;
                    default:
                        IndexList = IndexList.OrderBy(a => a.TableId);
                        break;
                }
                int TotalRecords = IndexList.Count();
                int PageSize = 5;
                int TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
                IndexList = IndexList.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

                ObjModel.TableMasterList = IndexList;
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
        public async Task<IActionResult> Create([FromForm] TableMaster tableMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                if (ModelState.IsValid)
                {
                    var CheckName = await dbcon.TableMaster.FirstOrDefaultAsync(_ => _.TableName == tableMaster.TableName);
                    if (CheckName == null)
                    {
                        tableMaster.IsAvailable = true;
                        tableMaster.IsOccupied = false;
                        tableMaster.CreatedAt = DateTime.UtcNow;
                        dbcon.TableMaster.Add(tableMaster);
                        await dbcon.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ViewBag.Message = "Table Added Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Table Already Exist";
                        await transaction.RollbackAsync();
                    }
                }
                else
                {
                    ViewBag.Message = "Table Not Added";
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
            if (id == null || dbcon.TableMaster == null)
            {
                return NotFound();
            }

            var tableMaster = await dbcon.TableMaster.FindAsync(id);
            if (tableMaster == null)
            {
                return NotFound();
            }
            return View(tableMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [FromForm] TableMaster tableMaster)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var FindTable= await dbcon.TableMaster.FirstOrDefaultAsync(_ => _.TableId == id
                                                            && _.IsAvailable == true);
                if (FindTable != null)
                {
                    FindTable.UpdatedAt = DateTime.UtcNow;
                    FindTable.TableName = tableMaster.TableName;
                    FindTable.FloorType = tableMaster.FloorType;
                    FindTable.IsOccupied = tableMaster.IsOccupied;
                    FindTable.IsOutSide = tableMaster.IsOutSide;
                    FindTable.Capacity = tableMaster.Capacity;
                    dbcon.TableMaster.Update(FindTable);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Message = "Table Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return View(tableMaster);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || dbcon.TableMaster == null)
            {
                ViewBag.Message = "Table Not Found";
                return NotFound();
            }

            var TableMaster = await dbcon.TableMaster
                .FirstOrDefaultAsync(m => m.TableId == id && m.IsAvailable == true);
            if (TableMaster == null)
            {
                ViewBag.Message = "Table Not Found";
                return NotFound();
            }

            return View(TableMaster);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using var transaction = await dbcon.Database.BeginTransactionAsync();
            try
            {
                var TableMaster = await dbcon.TableMaster.FirstOrDefaultAsync(m => m.TableId == id && m.IsAvailable == true);
                if (TableMaster != null)
                {
                    TableMaster.IsAvailable = false;
                    TableMaster.UpdatedAt = DateTime.UtcNow;
                    dbcon.TableMaster.Update(TableMaster);
                    await dbcon.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    ViewBag.Message = "Table Not Found";
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
