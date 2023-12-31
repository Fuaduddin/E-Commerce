﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using E_Commerce.BusinessLayer;
using E_Commerce.Model;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Configuration;

namespace E_commerce.Web.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult login()
        {
            return View("login");
        }
        [HttpPost]
        public ActionResult login(UserModel user)
        {
            UserModel model = new UserModel();
            model = LoginManager.Login(user.UserName);
            var password = PasswordDencrypt(model.UserPassword);
            string SuperAdminUserName = ConfigurationManager.AppSettings["SuperAdminusername"];
            string SuperAdminUserPassword = ConfigurationManager.AppSettings["SuperAdminuserpassword"];
            if (password == user.UserPassword && model.UserType=="Customer")
            {
                model.UserLastLogin = DateTime.Now;
                model.UserTotalLogin = model.UserTotalLogin + 1;
                if(LoginManager.UpdateUser(model))
                {
                    //Session["CustomerDetails"]= new CustomerModel();
                    //var CustomerList=CustomerManager.
                    //var Customer= CustomerList.Where(x => x.UserId == model.UserId).ToList();
                    //Session["CustomerDetails"]=(CustomerModel)Customer[0];

                    return RedirectToAction("DashBoard", "CustomerDashBoard");
                }      
            }
            else if (user.UserPassword == SuperAdminUserPassword && user.UserName == SuperAdminUserName)
            {
                return RedirectToAction("DashBoard", "SuperAdminDashboard");
                //model.UserLastLogin = DateTime.Now;
                //model.UserTotalLogin = model.UserTotalLogin + 1;
                //if (LoginManager.UpdateUser(model))
                //{
                //    Session["SuperAdminDetails"] = CustomerManager.GetSingleCustomer(model.UserId);
                //    return RedirectToAction("DashBoard", "SuperAdminDashboard");
                //}
            }
            else if (password == user.UserPassword && model.UserType == "Delivery Man")
            {
                model.UserLastLogin = DateTime.Now;
                model.UserTotalLogin = model.UserTotalLogin + 1;
                if (LoginManager.UpdateUser(model))
                {
                    Session["DeliveryManDetails"] = new DeliveryManModel();
                    var deliverymanlist = StaffSettingsManager.GetAllDeliveryMan();
                    var deliveryman= deliverymanlist.Where(x => x.UserId == model.UserId).ToList();
                    Session["DeliveryManDetails"] =(DeliveryManModel)deliveryman[0];
                    return RedirectToAction("DashBoard", "DeliveryManDashBoard");
                }
            }
            else if (password == user.UserPassword && model.UserType == "Supplier")
            {
                model.UserLastLogin = DateTime.Now;
                model.UserTotalLogin = model.UserTotalLogin + 1;
                if (LoginManager.UpdateUser(model))
                {
                    Session["SupplierDetails"] = new SupplierModel();
                    var Supplierlist = StaffSettingsManager.GetAllSupplier();
                    var Supplier =Supplierlist.Where(x=>x.UserId==model.UserId).ToList();
                    Session["SupplierDetails"] = (SupplierModel)Supplier[0];
                    return RedirectToAction("DashBoard", "SupplierDashBoard");
                }

            }
            else if (password == user.UserPassword && model.UserType == "Admin")
            {
                model.UserLastLogin = DateTime.Now;
                model.UserTotalLogin = model.UserTotalLogin + 1;
                if (LoginManager.UpdateUser(model))
                {
                    Session["AdminDetails"] = new AdminModel();
                    var Adminlist = StaffSettingsManager.GetAllAdmin();
                    var Admin= Adminlist.Where(x => x.UserId == model.UserId).ToList();
                    Session["AdminDetails"] = (AdminModel)Admin[0];
                    return RedirectToAction("DashBoard", "AdminDashboard");
                }
               
            }
            else
            {
                ViewData["Message"] = "Your Password or User Name is incorrect";
                
            }
            return View("login",user);
        }
        public ActionResult registration()
        {
            CustomerViewModel customer = new CustomerViewModel();
            customer.Customer = new CustomerModel();
            customer.user = new UserModel();
            customer.viewzone = DeliverySettingsManager.GetAllZone();
            return View("registration", customer);
        }
        [HttpPost]
        public ActionResult registration(int districts, CustomerModel customer,UserModel user)
        {
            user.UserPassword = PasswordDencrypt(user.UserPassword);
            var userdetails = UserSettingManager.AddNewUser(user);
            customer.UserId = userdetails;
            customer.CustomerArea = districts;
            if (CustomerManager.AddNewCustomer(customer) > 0)
            {
                ViewData["Message"] = "Account Has Been Created";
                ModelState.Clear();
                return View("login");
            }
            else
            {
                ViewData["Message"] = "Error !!!";
                return View("registration");
            }
        }
        public JsonResult GetSelectedArea(int zoneid)
        {
            List<Area> categorylist = DeliverySettingsManager.GetAllArea();
            var output = categorylist.Where(x => x.Placeid == zoneid).ToList();
            string result = JsonConvert.SerializeObject(output);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string PasswordDencrypt(string Password)
        {
            string encryptedpassword;
            if (!string.IsNullOrEmpty(Password))
            {
                // parse base64 string
                byte[] encryptpassword = Convert.FromBase64String(Password);
                //decrypt data
                encryptedpassword = Encoding.Unicode.GetString(encryptpassword);
            }
            else
            {
                encryptedpassword = "";
            }
            return encryptedpassword;
        }
    }
}