using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DataAccess.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolio.Utility;
using MyPortfolioWeb.DataAccess;


namespace MyPortfolioWeb.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AlienTypesController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public AlienTypesController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GuestTypes
        public IActionResult Index()
        {
            IEnumerable<AlienType> objAlienTypeList = _unitOfWork.AlienType.GetAll();
            return View(objAlienTypeList);
        }

        // GET: GuestTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.AlienType.GetAll == null)
            {
                return NotFound();
            }

            var alienType = _unitOfWork.AlienType.GetFirstOrDefault(m => m.Id == id);
            if (alienType == null)
            {
                return NotFound();
            }

            return View(alienType);
        }

        // GET: GuestTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,AlienTypeName,AlienTypeDateTimeCreated")] AlienType alienType)
        {

            var checkDup = _unitOfWork.AlienType.GetFirstOrDefault(m => m.AlienTypeName == alienType.AlienTypeName);

            if (checkDup != null)
            {

                ModelState.AddModelError("AlienTypeName", "This type already exist.");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.AlienType.Add(alienType);
                _unitOfWork.Save();
                TempData["Success"] = "Alien type created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(alienType);
        }

        // GET: GuestTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _unitOfWork.AlienType.GetAll == null)
            {
                return NotFound();
            }

            var alienType = _unitOfWork.AlienType.GetFirstOrDefault(m => m.Id == id);
            if (alienType == null)
            {
                return NotFound();
            }
            return View(alienType);
        }

        // POST: GuestTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("Id,AlienTypeName,AlienTypeDateTimeCreated")] AlienType alienType)
        {
            if (id != alienType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.AlienType.Update(alienType);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlienTypeExists(alienType.Id))
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
            TempData["Success"] = "Alien type updated successfully!";
            return View(alienType);
        }

        // GET: GuestTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork.AlienType.GetAll == null)
            {
                return NotFound();
            }

            var alienType = _unitOfWork.AlienType.GetFirstOrDefault(m => m.Id == id);
            if (alienType == null)
            {
                return NotFound();
            }

            return View(alienType);
        }

        // POST: GuestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_unitOfWork.AlienType.GetAll == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AlienTypes'  is null.");
            }
            var alienType = _unitOfWork.AlienType.GetFirstOrDefault(m => m.Id == id);
            if (alienType != null)
            {
                _unitOfWork.AlienType.Remove(alienType);
            }

            _unitOfWork.Save();
            TempData["Success"] = "Alien type deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool AlienTypeExists(int id)
        {

            var checkDup = _unitOfWork.AlienType.GetFirstOrDefault(m => m.Id == id);
            if (checkDup != null)
            {
                return true;
            }

            return false;

        }
    }
}
