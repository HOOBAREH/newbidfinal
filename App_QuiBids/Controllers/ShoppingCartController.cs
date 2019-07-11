using App_QuiBids.Models;
using DataLayer.IRepository;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using App_QuiBids.ServiceZarinPalTest;
using DataLayer.Models;

namespace App_QuiBids.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepo _repoPro ;
        private readonly IOrderRepo _repoOr;
        public ShoppingCartController(IProductRepo repo, IOrderRepo repoOr)
        {
            _repoPro = repo;
            _repoOr = repoOr;
        }
        public ShoppingCartController() : this(new ProductRepo(),new OrderRepo())
        {

        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<ShopCartViewModel> list = new List<ShopCartViewModel>();
            if (Session["ShoppingCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShoppingCart"] as List<ShopCartItem>;
                foreach (var item in cart)
                {
                    var product = _repoPro.GetProduct(item.ProductID);
                    list.Add(new ShopCartViewModel
                    {
                        ProductID = item.ProductID,
                        Count = item.Count,
                        Price = product.Price,
                        Title = product.Name,
                        Sum = product.Price * item.Count,
                        ImageUrl = product.Image
                    });

                }
                return View(list);
            }
            ViewBag.Error = "سبد خرید شما خالی است.";
            return View(list);
        }
        public ActionResult RemoveFromCart(int id)
        {
            if (Session["ShoppingCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShoppingCart"] as List<ShopCartItem>;

                int index = cart.FindIndex(p => p.ProductID == id);
                cart.RemoveAt(index);
                Session["ShoppingCart"] = cart;
                Session["Count"] = cart.Sum(p => p.Count);
            }
            return RedirectToAction("Index");
        }
        public int ShopCountCart()
        {
            int count = 0;

            //اگر سیشن خالی نبود
            if (Session["ShoppingCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShoppingCart"] as List<ShopCartItem>;

                count = cart.Sum(p => p.Count);
                Session["Count"] = count;
            }
            return count;

        }
        public ActionResult Payment()
        {
            List<ShopCartItem> cart = Session["ShoppingCart"] as List<ShopCartItem>;
            if (cart.Count==0)
            {
                ViewBag.Error = "سبد خرید شما خالی است.";
                return RedirectToAction("Index");
            }
            var order = _repoOr.AddOrder(cart,int.Parse( User.Identity.Name));
            ServicePointManager.Expect100Continue = false;
            PaymentGatewayImplementationServicePortTypeClient zp = new PaymentGatewayImplementationServicePortTypeClient();
            string Authority;

            int Status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", order.TotalPrice, "درگاه پرداخت تست زرین پال", "you@yoursite.com", "09361997137", "http://localhost:13327/ShoppingCart/Verify/"+order.Id, out Authority);

            if (Status == 100)
            {
                Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                ViewBag.Error = Status;
            }

            return View();
        }


        public ActionResult Verify(int id)
        {
            //پیدا کردن sum

            var order = _repoOr.GetOrder(id);

            //Status == Ok || NOK
            //Authority == كد يكتاي شناسه مرجع درخواست
            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null
                && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                //اگر وضعیت پرداخت اوکی بود
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    //jame tamame maghadir onja save hastesh
                    int Amount = order.TotalPrice;
                    long RefID;
                    ServicePointManager.Expect100Continue = false;
                    PaymentGatewayImplementationServicePortTypeClient zp =
                        new PaymentGatewayImplementationServicePortTypeClient();

                    int Status =
                        zp.PaymentVerification("YOUR-ZARINPAL-MERCHANT-CODE",
                        Request.QueryString["Authority"].ToString(), Amount, out RefID);

                    if (Status == 100)
                    {
                        Session["ShoppingCart"] = null;
                        Session["Count"] = null;
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                        _repoOr.UpdateIsFinaly(order.Id,RefID.ToString());
                    }
                    else
                    {
                        ViewBag.Status = Status;
                    }
                }
                else
                {
                    ViewBag.Status = "Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString();
                }
            }
            else
            {
                Response.Write("Invalid Input");
            }
            return View();
        }
    }
}