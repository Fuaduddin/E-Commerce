﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.BusinessLayer;
using E_Commerce.Model;
using Newtonsoft.Json;

namespace E_commerce.Web.Controllers
{
    public class cartController : Controller
    {
        // GET: cart
        public ActionResult cart()
        {
          //  CustomerModel customer = 
            CustomerViewModel cart = new CustomerViewModel();
            cart.viewzoneList = DeliverySettingsManager.GetAllZone();
            cart.PaymentMethodList = DeliverySettingsManager.GetAllDeliveryCost();
            cart.areaList = DeliverySettingsManager.GetAllArea();
            cart.Customer= (CustomerModel)Session["CustomerDetails"];
            cart.Cart = new CartModel();
            //cart.Cart.Payment = new Payment();
            //cart.Cart.Shipment = new ShipmentModel();
            //cart.Cart.Payment = new Payment();
            //cart.Cart.Shipment = new ShipmentModel();
            return View(cart);
        }
        [HttpPost]
        public ActionResult cart(CartModel cart,int [] Productitem, int [] Quantityitem, int[] ProductPriceitem)
        {
            CustomerModel customer = (CustomerModel)Session["CustomerDetails"];
            cart.Order.OrderDeliveryUpdate = 0;
            cart.Order.OrderOfficialId = Guid.NewGuid().ToString();
            cart.Order.CustomerID = 1;
            cart.Order.OrderAddedDate = DateTime.Now;
            long orderid = OrderManager.AddNewOrder(cart.Order);
            long shipment = 0;
            long Payment = 0;
            if (orderid>0)
            {
                cart.Shipment.OrderId = (int)orderid;
                cart.Shipment.ShipmentAddedDate = DateTime.Now;
                cart.Shipment.ShipmentUpdate = 0;
                cart.Payment.OrderId= (int)orderid;
                shipment = OrderManager.AddNewShipment(cart.Shipment);
                Payment = OrderManager.AddNewPayment(cart.Payment);
                for (var n=0;n<=Productitem.Length;n++)
                {
                    OrderItemModel orderitem = new OrderItemModel
                    {
                        OrderId = (int)orderid,
                        Product= Productitem[n],
                        Quantity = Quantityitem[n],
                        ProductPrice = ProductPriceitem[n],
                    };
                     OrderManager.AddNewOrderItem(orderitem);
                }
            }
            if(orderid>0 && shipment>0 && Payment>0)
            {
                ModelState.Clear();
                return View("confirmation");
            }
            //CustomerViewModel cart = new CustomerViewModel();
            //cart.viewzoneList = DeliverySettingsManager.GetAllZone();
            //cart.Cart = new CartModel();
            return View("cart");
        }
        public ActionResult confirmation()
        {
            return View("confirmation");
        }
        public ActionResult TrackOrder()
        {
            return View("TrackOrder");
        }
        [HttpPost]
        public ActionResult TrackOrder(string OrderId)
        {
            CustomerViewModel trackorder = new CustomerViewModel();
            //trackorder.Cart.Order = OrderManager.GetSIngleOrder(OrderId);
            //trackorder.Cart.Shipment = OrderManager.GetSIngleShipment(trackorder.Cart.Order.OrderId);
            //trackorder.Cart.Payment= OrderManager.GetSInglePayment(trackorder.Cart.Order.OrderId);
            //trackorder.Cart.OrderItem= OrderManager.GetSIngleOrderItem(trackorder.Cart.Order.OrderId);
            return View("TrackOrder", trackorder);
        }
        public JsonResult GetOrderDetails(string orderid)
        {
            var output = "";
            //   List<Area> categorylist = DeliverySettingsManager.GetSingleZoneAllArea(zoneid);
            //    string result = JsonConvert.SerializeObject(categorylist);
                return Json(output, JsonRequestBehavior.AllowGet);
            //return output;
        }
        /// Ajax Code
        public JsonResult GetAreaName(int zoneid)
        {
            List<Area> arealist = new List<Area>();
            arealist = DeliverySettingsManager.GetSingleZoneAllArea(zoneid);
            var result = JsonConvert.SerializeObject(arealist);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}