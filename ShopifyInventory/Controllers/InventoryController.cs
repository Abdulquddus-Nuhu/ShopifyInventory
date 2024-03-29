﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopifyInventory.Data;
using ShopifyInventory.Data.Entities;
using ShopifyInventory.Models;
using System.Data.Common;

namespace ShopifyInventory.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ShopifyDbContext _context;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ShopifyDbContext context, ILogger<InventoryController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;
            var items = await _context.Items!.Where(i => !i.IsDeleted && i.Username == username).OrderBy(i => i.Created).Select(i => new ItemModel() 
            { 
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Quantity = i.Quantity
            }).ToListAsync();
            return View(items);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var username = HttpContext.User.Identity!.Name;
            _logger.LogInformation("{0} is trying to create a new item", username);

            var item = new ItemModel();
            return View(item);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ItemModel itemModel)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError("Model", "Invalid properties");

            var username = HttpContext.User.Identity!.Name;
            var newItem = new Item()
            {
                Name = itemModel.Name,
                Quantity = itemModel.Quantity,
                Description = itemModel.Description,
                Username = username!
            };

            _logger.LogInformation("Saving newly created item by {0} to database", username);
            try
            {
                await _context.AddAsync(newItem);
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogInformation(ex.Message);
                ModelState.AddModelError("Database", "Error while saving to database");
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _context.Items!.FindAsync(id);
            var itemModel = new ItemModel() 
            {
                Description = item!.Description,
                Name = item.Name,
                Quantity = item.Quantity,
                Id = item.Id
            };
            return View(itemModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ItemModel itemModel)
        {
            var item = await _context.Items!.FindAsync(itemModel.Id);

            item!.Description = itemModel.Description;
            item.Quantity = itemModel.Quantity;
            item.Name = itemModel.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogInformation(ex.Message);
                ModelState.AddModelError("Database", "Error while saving to database");
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Items!.FindAsync(id);
            var itemModel = new ItemModel()
            {
                Description = item!.Description,
                Name = item.Name,
                Quantity = item.Quantity,
                Id = item.Id
            };
            return View(itemModel);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(ItemModel itemModel)
        {
            var item = await _context.Items!.FindAsync(itemModel.Id);
            item!.IsDeleted = true;
            item.DeletionComments = itemModel.DeletionComments;
            item.DeletedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogInformation(ex.Message);
                ModelState.AddModelError("Database", "Error while saving to database");
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Archive()
        {
            var username = HttpContext.User.Identity!.Name;
            var items = _context.Items!.Where(i => i.IsDeleted && i.Username == username).OrderBy(i => i.DeletedAt).Select(i => new ItemModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Quantity = i.Quantity,
                DeletedAt = i.DeletedAt,
                DeletionComments = i.DeletionComments!
            }).ToList();
            return View(items);
        }


        public async Task<IActionResult> UnDeleteItem(Guid id)
        {
            var item = await _context.Items!.FindAsync(id);
            item!.IsDeleted = false;
           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogInformation(ex.Message);
                ModelState.AddModelError("Database", "Error while saving to database");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PermanentDelete(ItemModel itemModel)
        {
            var item = await _context.Items!.FindAsync(itemModel.Id);
            _context.Items.Remove(item!);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                _logger.LogInformation(ex.Message);
                ModelState.AddModelError("Database", "Error while saving to database");
            }
            return RedirectToAction(nameof(Archive));
        }
    }
}
