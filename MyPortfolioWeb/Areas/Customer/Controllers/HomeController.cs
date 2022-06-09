using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortfolio.DataAccess.Repository.IRepository;
using MyPortfolio.Models;
using MyPortfolio.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace MyPortfolioWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitOfWork, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            IEnumerable<GuestBook> guestBookList = _unitOfWork.GuestBook.GetAll(includeProperties: "GuestType,AlienType");
            return View(guestBookList);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Upsert(GuestBookVM obj)
        {

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.Name);
            //obj.ApplicationUser.Id = claim.Value;
            
            if (ModelState.IsValid)
            {
                if (obj.GuestBook.Id == 0)
                {
                    _emailSender.SendEmailAsync(obj.GuestBook.GuestBookEmail, "Guest Book Entry",
                        $"Thank you for signing the guest book. If you would like to have the entry deleted for any reason, please email me at erictingler@gmail.com");
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {

            var guestBookList = _unitOfWork.GuestBook.GetAll(includeProperties: "GuestType,AlienType");
            return Json(new { data = guestBookList });

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