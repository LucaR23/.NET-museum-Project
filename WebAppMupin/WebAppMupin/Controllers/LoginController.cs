using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebAppMupin.Models;

namespace WebAppMupin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult FormLogin()
        {
            Models.Login l = new Models.Login();
            return View(l);
        }


        public ActionResult VerifyUser(Models.Login l)
        {
            if (l.username == null || l.password == null)
            {
                l.message = "Login Fallito... Riprova";
                return View("formLogin", l);
            }
            else
            {
                bool isAdmin = UtilityLogin.verifyAdmin(l.username);
                if (isAdmin)
                {
                    bool tryLogin = UtilityLogin.tryLogin(l.username, l.password);
                    if (tryLogin)
                    {
                        Session["Administrator"] = l.username;
                        return RedirectToAction("Index", "Administrator");      // administrator logged 
                    }
                    else
                    {
                        l.message = "Login Fallito... Riprova";
                        return View("formLogin", l);          //login fallito 
                    }
                }
                bool esisteUser = UtilityLogin.verifiyUser(l.username);
                if (!esisteUser)
                {
                    l.message = "Login Fallito... Riprova";
                    return View("formLogin", l);
                }
                else 
                {   
                    int verificaPassword = UtilityLogin.isSetted(l.username, l.password);
                    if (verificaPassword == 2)
                    {
                        bool tryLogin = UtilityLogin.tryLogin(l.username, l.password);
                        if (tryLogin)
                        {
                            Session["utente"] = l.username;
                            return RedirectToAction("Index", "User");    // login effettuato
                        }
                        else
                        {
                            l.message = "Login Fallito... Riprova";
                            return View("formLogin", l);          //login fallito 
                        }
                    }
                 
                    if (verificaPassword == 1)
                    {
                        setPassword set = new setPassword();
                        set.utente = l.username;
                        Session["utente"] = l.username;
                        return View("setPassword", set);  // situazione di setPassword
                    }
                    else
                    {
                        l.message = "Login Fallito... Riprova";
                        return View("formLogin", l);          //login fallito 
                    }

                }
           
            }
            
        }


        public ActionResult createPassword(setPassword set)
        {
            if(set.Password==null || set.repeatPassword == null)
            {
                set.errorMessage = "Non è possibile lasciare vuoti questi campi";
                return View("setPassword", set);
            }
            if(set.Password.Length<6 || set.repeatPassword.Length < 6)
            {
                set.errorMessage = " Password Troppo Corta ** minimo 6 caratteri";
                return View("setPassword", set);
            }
            if(set.Password != set.repeatPassword)
            {
                set.errorMessage = " Le password non coincidono";
                return View("setPassword", set);
            }
            else
            {
                string utente = (string)Session["utente"];
              bool insert=  UtilityLogin.insertPassword(set.Password, utente);
              
                if (!insert)
                {
                    set.errorMessage = "Qualcosa NON ha funzionato";        // internal error
                    return View("setPassword", set);
                }
                else
                {
                    Models.Login l = new Models.Login();
                    l.message = "Ripetere il Login per Accedere";      // password inserted 
                    return View("formLogin", l);
                }
            }
           
        }
    }
}