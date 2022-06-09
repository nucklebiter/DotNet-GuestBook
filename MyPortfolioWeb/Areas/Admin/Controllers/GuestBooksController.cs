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
using MyPortfolio.Models.ViewModels;
using MyPortfolio.Utility;
using MyPortfolioWeb.DataAccess;


namespace MyPortfolioWeb.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GuestBooksController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public GuestBooksController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: GuestTypes
        public IActionResult Index()
        {
           
            return View();
        }

        // GET: GuestTypes/Details/5
        public IActionResult Upsert(int? id)
        {
            GuestBookVM guestBookVM = new()
            {

                GuestBook = new(),
                
                GuestTypeList = _unitOfWork.GuestType.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.GuestTypeName,
                        Value = u.Id.ToString()
                    }),

                AlienTypeList = _unitOfWork.AlienType.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.AlienTypeName,
                        Value = u.Id.ToString()
                    })

            };

            if (id == null || _unitOfWork.GuestBook.GetAll == null)
            {
                //Creat Guest Book
                //ViewBag.GuestTypeList = GuestTypeList;
                //ViewBag.AlienTypeList = AlienTypeList;
                return View(guestBookVM);
            }
            else
            {

                //Updare Guest Book
                guestBookVM.GuestBook = _unitOfWork.GuestBook.GetFirstOrDefault(u => u.Id == id);
                return View(guestBookVM);

            }

            return View(guestBookVM);
        }


        // POST: GuestTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(GuestBookVM obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.GuestBook.Id == 0)
                {
                    _unitOfWork.GuestBook.Add(obj.GuestBook);
                }
                else
                {
                    _unitOfWork.GuestBook.Update(obj.GuestBook);
                }
                
                _unitOfWork.Save();
                TempData["Success"] = "Guest book entry updated successfully!";
                return RedirectToAction("Index");

            }
            
            return View(obj);
        }

        //GET GuestBooks/Delete/5
        public IActionResult Delete(int? id)
        {


            var guestBook = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
            if (guestBook != null)
            {
                _unitOfWork.GuestBook.Remove(guestBook);
            }

            _unitOfWork.Save();
            TempData["Success"] = "Guest type deleted successfully!";
            return RedirectToAction(nameof(Index));


        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {

            var guestBookList = _unitOfWork.GuestBook.GetAll(includeProperties:"GuestType,AlienType");
            return Json(new {data = guestBookList});

        }

        //POST
        //[HttpDelete]
        //public IActionResult Delete(int? id)
        //{


        //    var guestBook = _unitOfWork.GuestBook.GetFirstOrDefault(m => m.Id == id);
        //    if (guestBook != null)
        //    {
        //        _unitOfWork.GuestBook.Remove(guestBook);
        //    }

            
        //    _unitOfWork.Save();
        //    return RedirectToAction(nameof(Index));


        //}
        #endregion
    }
}
