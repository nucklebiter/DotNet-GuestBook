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
    public class GuestTypesController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public GuestTypesController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GuestTypes
        public IActionResult Index()
        {
            IEnumerable<GuestType> objGuestTypeList = _unitOfWork.GuestType.GetAll();
            return View(objGuestTypeList);
        }

        // GET: GuestTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.GuestType.GetAll == null)
            {
                return NotFound();
            }

            var guestType = _unitOfWork.GuestType.GetFirstOrDefault(m => m.Id == id);
            if (guestType == null)
            {
                return NotFound();
            }

            return View(guestType);
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
        public IActionResult Create([Bind("Id,GuestTypeName,GuestTypeCreatedDateTime")] GuestType guestType)
        {

            var checkDup = _unitOfWork.GuestType.GetFirstOrDefault(m => m.GuestTypeName == guestType.GuestTypeName);

            if (checkDup != null)
            {

                ModelState.AddModelError("GuestTypeName", "This type already exist.");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.GuestType.Add(guestType);
                _unitOfWork.Save();
                TempData["Success"] = "Guest type created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(guestType);
        }

        // GET: GuestTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _unitOfWork.GuestType.GetAll == null)
            {
                return NotFound();
            }

            var guestType = _unitOfWork.GuestType.GetFirstOrDefault(m => m.Id == id);
            if (guestType == null)
            {
                return NotFound();
            }
            return View(guestType);
        }

        // POST: GuestTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,GuestTypeName,GuestTypeDisplayOrder,GuestTypeCreatedDateTime")] GuestType guestType)
        {
            if (id != guestType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GuestType.Update(guestType);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestTypeExists(guestType.Id))
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
            TempData["Success"] = "Guest type updated successfully!";
            return View(guestType);
        }

        // GET: GuestTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork.GuestType.GetAll == null)
            {
                return NotFound();
            }

            var guestType = _unitOfWork.GuestType.GetFirstOrDefault(m => m.Id == id);
            if (guestType == null)
            {
                return NotFound();
            }

            return View(guestType);
        }

        // POST: GuestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_unitOfWork.GuestType.GetAll == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GuestTypes'  is null.");
            }
            var guestType = _unitOfWork.GuestType.GetFirstOrDefault(m => m.Id == id);
            if (guestType != null)
            {
                _unitOfWork.GuestType.Remove(guestType);
            }

            _unitOfWork.Save();
            TempData["Success"] = "Guest type deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool GuestTypeExists(int id)
        {

            var checkDup = _unitOfWork.GuestType.GetFirstOrDefault(m => m.Id == id);
            if (checkDup != null)
            {
                return true;
            }

            return false;

        }
    }
}
