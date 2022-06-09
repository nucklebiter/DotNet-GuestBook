using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DataAccess.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolioWeb.DataAccess;


namespace MyPortfolioWeb.Areas.Admin.Controllers

{
    [Area("Admin")]
    public class OLD_GuestBooksController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public OLD_GuestBooksController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GuestTypes
        public IActionResult Index()
        {
            IEnumerable<GuestBook> guestBookList = _unitOfWork.GuestBook.GetAll();
            return View(guestBookList);
        }

        // GET: GuestTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.GuestBook.GetAll == null)
            {
                return NotFound();
            }

            var guestBook = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
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
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,GuestType,AlienType")] GuestBook guestBook)
        {

            var checkDup = _unitOfWork.GuestBook.GetFirstOrDefault(
                m => m.GuestBookFirstName == guestBook.GuestBookFirstName &&
                m.GuestBookLastName == guestBook.GuestBookLastName
                
                );

            if (checkDup != null)
            {

                ModelState.AddModelError("FirstName", "You have already signed the guest book");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.GuestBook.Add(guestBook);
                _unitOfWork.Save();
                TempData["Success"] = "Guest book signed successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(guestBook);
        }

        // GET: GuestTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _unitOfWork.GuestBook.GetAll == null)
            {
                return NotFound();
            }

            var guestBook = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
            if (guestBook == null)
            {
                return NotFound();
            }
            return View(guestBook);
        }

        // POST: GuestTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("Id,FirstName,LastName,Email,GuestType,AlienType")] GuestBook guestBook)
        {
            if (id != guestBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GuestBook.Update(guestBook);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookExists(guestBook.Id))
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
            TempData["Success"] = "Guest book entry updated successfully!";
            return View(guestBook);
        }

        // GET: GuestTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork.GuestBook.GetAll == null)
            {
                return NotFound();
            }

            var guestBook = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // POST: GuestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_unitOfWork.GuestBook.GetAll == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GuestBook'  is null.");
            }
            var guestBook = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
            if (guestBook != null)
            {
                _unitOfWork.GuestBook.Remove(guestBook);
            }

            _unitOfWork.Save();
            TempData["Success"] = "Guest book entry deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookExists(int id)
        {

            var checkDup = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
            if (checkDup != null)
            {
                return true;
            }

            return false;

        }
    }
}
