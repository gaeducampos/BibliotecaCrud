using Microsoft.AspNetCore.Mvc;
using BibliotecaCrud.Data;
using BibliotecaCrud.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BibliotecaCrud.Controllers
{

    public class PrincipalController : Controller
    {
        LibraryData libraryData = new LibraryData();
        sha256 sha256Object = new sha256();

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        public IActionResult CloseSesion()
        {
            return RedirectToAction("Login");
        }

        [HttpPost]

        public IActionResult Register(User userObject) 
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");


            if (userObject.Password == userObject.ConfirmPassword && validateEmailRegex.IsMatch(userObject.Email))
            {
                userObject.Password = sha256Object.convertToSha256(userObject.Password);


            } else
            {
                ViewData["Message"] = "Las contrasenas no son iguales o el correo no es valido";
                return View();
            }


           libraryData.RegisterStoredProcedure(userObject);

            ViewData["Message"] = libraryData.Message;

            if(libraryData.isRegister)
            {
                return RedirectToAction("Login");
            } 
            else
            {
                return View();
            }

        }


        [HttpPost]

        public IActionResult Login(User userObject)
        {
            userObject.Password = sha256Object.convertToSha256(userObject.Password);

            libraryData.LogInStoredProcedure(userObject);

            if(userObject.Id != 0)
            {
               
                return RedirectToAction("ShowList");
            } 
            else
            {
                ViewData["Message"] = "Usuario No encontrado";
                return View();
            }

            
        }

        public IActionResult showList()
        {
            // mostrar toda la tabla

            var libraryList = libraryData.addToList();
            return View(libraryList);
        }

        public IActionResult save()
        {
            return View();
        }


        [HttpPost]
        public IActionResult save(LibraryModel libraryObject)
        {
            // tomar un objeto y guardalo en la BD

            var isValid = libraryData.save(libraryObject);

            if(!ModelState.IsValid)
            {
                return View();
            }
            
            if(isValid)
            {
                return RedirectToAction("showList");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Edit(int bookId)
        {
            var bookData = libraryData.getBookData(bookId);
            return View(bookData);
        }


        [HttpPost]
        public IActionResult Edit(LibraryModel libraryObject)
        {
            

            if (!ModelState.IsValid)
            {
                return View();
            }

            var isValid = libraryData.Edit(libraryObject);

            if (isValid)
            {
                return RedirectToAction("showList");
            }
            else
            {
                return View();
            };
        }

        public IActionResult Delete(int bookId)
        {
            var bookData = libraryData.getBookData(bookId);
            return View(bookData);
        }


        [HttpPost]
        public IActionResult Delete(LibraryModel libraryObjectToDelete)
        {


            var isValid = libraryData.delete(libraryObjectToDelete.IdBook);

            if (isValid)
            {
                return RedirectToAction("showList");
            }
            else
            {
                return View();
            }
        }
    }
}
