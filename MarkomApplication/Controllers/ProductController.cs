using MarkomApplication.DataAccess;
using MarkomApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkomApplication.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ListProduct()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateDataProduct(ProductViewModel paramAddProd)
        {
            if (ModelState.IsValid)
            {
                //is delete default value
                paramAddProd.isDelete = false;
                //update data manual createby and createdate
                paramAddProd.createBy = "Anastasia";
                paramAddProd.createDate = DateTime.Now;


                int? nameV = ProductDataAccess.NameValidation(paramAddProd.name);

                if (nameV == 0)
                {
                    string latestCode = ProductDataAccess.CreateProduct(paramAddProd);

                    if (latestCode != "")
                    {
                        return Json(new { success = true, latestCode, message = ProductDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = ProductDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Product name " + paramAddProd.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult Index(ProductViewModel paramSearch)
        {
            List<ProductViewModel> listSearchProduct = ProductDataAccess.GetListProduct(paramSearch);

            return PartialView(listSearchProduct);
        }


        //EDIT ROLE
        public ActionResult EditProduct(int paramId)
        {
            return PartialView(ProductDataAccess.GetDetailProductById(paramId));
        }

        [HttpPost]
        public ActionResult EditDataProduct(ProductViewModel paramEditProd)
        {
            if (ModelState.IsValid)
            {
                //update data manual createby and createdate
                paramEditProd.updateBy = "Tian";
                paramEditProd.updateDate = DateTime.Now;

                int? nameV = ProductDataAccess.NameValidation(paramEditProd.name);

                if (nameV <= 1)
                {
                    if (ProductDataAccess.UpdateProduct(paramEditProd))
                    {
                        return Json(new { success = true, message = ProductDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = ProductDataAccess.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Product name " + paramEditProd.name + " is exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Wajib menginputkan semua kotak bertanda bintang" }, JsonRequestBehavior.AllowGet);

            }
        }


        //VIEW ROLE
        public ActionResult ViewProduct(int paramId)
        {
            return PartialView(ProductDataAccess.GetDetailProductById(paramId));
        }



        //DELETE EMPLOYEE
        [HttpPost]
        public JsonResult DeleteDataProduct(int paramId)
        {
            string latestCode = ProductDataAccess.DeleteProduct(paramId);

            if (latestCode != "")
            {
                return Json(new { success = true, latestCode, message = ProductDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = ProductDataAccess.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}