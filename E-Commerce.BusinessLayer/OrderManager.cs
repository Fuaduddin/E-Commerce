﻿using E_Commerce.DataLayerSQL;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BusinessLayer
{
    public class OrderManager
    {
        // Order
        public static long AddNewOrder(OrderModel order)
        {
            OrderSQLProvider provider = new OrderSQLProvider();
            var chargeid = provider.AddNewOrder(order);
            return chargeid;
        }
        //public static OrderModel GetSIngleOrder(string order)
        //{
        //    OrderSQLProvider provider = new OrderSQLProvider();
        //    var chargeid = provider.GetSIngleOrder(order);
        //    return chargeid;
        //}
        //Shipment
        public static long AddNewShipment(ShipmentModel shipment)
        {
            OrderSQLProvider provider = new OrderSQLProvider();
            var chargeid = provider.AddNewShipment(shipment);
            return chargeid;
        }
        //public static ShipmentModel GetSIngleShipment(int order)
        //{
        //    OrderSQLProvider provider = new OrderSQLProvider();
        //    var chargeid = provider.GetSIngleShipment(order);
        //    return chargeid;
        //}
        // Order Item
        public static long AddNewOrderItem(OrderItemModel OrderItem)
        {
            OrderSQLProvider provider = new OrderSQLProvider();
            var chargeid = provider.AddNewOrderItem(OrderItem);
            return chargeid;
        }
        //public static List<OrderItemModel> GetSIngleOrderItem(int order)
        //{
        //    OrderSQLProvider provider = new OrderSQLProvider();
        //    var chargeid = provider.GetSIngleOrderItem(order);
        //    return chargeid;
        //}
        // Payment
        public static long AddNewPayment(Payment payment)
        {
            OrderSQLProvider provider = new OrderSQLProvider();
            var chargeid = provider.AddNewPayment(payment);
            return chargeid;
        }
        //public static Payment GetSInglePayment(int id)
        //{
        //    OrderSQLProvider provider = new OrderSQLProvider();
        //    var chargeid = provider.GetSInglePayment(id);
        //    return chargeid;
        //}



        //public static List<DeliveryCharge> GetAllDeliveryCost()
        //{
        //    DeliverySettingsSQLProvider provider = new DeliverySettingsSQLProvider();
        //    var chargeid = provider.ViewAllDeliveryCharge();
        //    return chargeid;
        //}
        //public static bool UpdateDeliveryCost(DeliveryCharge category)
        //{
        //    DeliverySettingsSQLProvider provider = new DeliverySettingsSQLProvider();
        //    var chargeid = provider.UpdateDeliveryCharge(category);
        //    return chargeid;
        //}
        //public static DeliveryCharge GetSingleDeliveryCost(int categoryId)
        //{
        //    DeliverySettingsSQLProvider provider = new DeliverySettingsSQLProvider();
        //    var chargeid = provider.GetSingleDelivery(categoryId);
        //    return chargeid;
        //}
        //public static bool DeleteDeliveryCost(int categoryId)
        //{
        //    DeliverySettingsSQLProvider provider = new DeliverySettingsSQLProvider();
        //    var Categoriesd = provider.DeleteDeliveryCharge(categoryId);
        //    return Categoriesd;
        //}
        //public static List<DeliveryCharge> SearchDeliveryCost(string SearchKeyword)
        //{
        //    DeliverySettingsSQLProvider provider = new DeliverySettingsSQLProvider();
        //    var Categoriesd = provider.SearchDeliveryCost(SearchKeyword);
        //    return Categoriesd;
        //}
    }
}
