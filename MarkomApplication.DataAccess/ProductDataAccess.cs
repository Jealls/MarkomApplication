using MarkomApplication.ViewModel;
using MarkomApplication.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace MarkomApplication.DataAccess
{
    public class ProductDataAccess
    {
        public static string Message = string.Empty;


        public static string CreateProduct(ProductViewModel paramDataProd)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_product c = new m_product();
                        c.code = ProductCode();
                        c.name = paramDataProd.name;
                        c.description = paramDataProd.description;
                        c.is_delete = paramDataProd.isDelete;
                        c.create_by = paramDataProd.createBy;
                        c.create_date = paramDataProd.createDate;

                        db.m_product.Add(c);
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        //get latest save code
                        latestSaveCode = c.code;

                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message;
                        dbContextTransaction.Rollback();
                        //throw;
                    }
                }
            }

            return latestSaveCode;
        }

        public static List<ProductViewModel> GetListProduct(ProductViewModel paramSearch)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spProductSearch(paramSearch.code, paramSearch.name, paramSearch.description, paramSearch.createDate2, paramSearch.createBy);

                List<ProductViewModel> comList = res.Select(c => new ProductViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    description = c.description,
                    createDate = c.create_date,
                    createBy = c.create_by
                }).ToList();

                result = comList;
            }

            return result;
        }

        //GET DETAIL Product
        public static ProductViewModel GetDetailProductById(int paramProdId)
        {
            ProductViewModel result = new ProductViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spProductDetailByID(paramProdId);

                result = res.Select(c => new ProductViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    description = c.description

                }).FirstOrDefault();
            }

            return result;
        }

        //UPDATE Product
        public static bool UpdateProduct(ProductViewModel paramEditEmp)
        {
            bool result = true;
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    db.spProductUpdate(
                            paramEditEmp.id
                            , paramEditEmp.name
                            , paramEditEmp.description
                            , paramEditEmp.updateBy
                            , paramEditEmp.updateDate
                        );
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                result = false;
                //throw;
            }
            return result;
        }


        //DELETE ROLE
        public static string DeleteProduct(int name)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {
                    ObjectParameter returnId = new ObjectParameter("IdNumber", typeof(string));
                    db.spProductDelete(name, returnId); 
                    latestSaveCode = (String)returnId.Value;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return latestSaveCode;
        }

        public static int? NameValidation(string name)
        {
            int? result = 0;
            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spProductCountName(name).FirstOrDefault();
                result = res;
            }

            return result;
        }




        public static string ProductCode()
        {
            string kode = "";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.m_product.Max(mc => mc.code);

                if (maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(2, maxCodeNum.Length - 2);

                    if (lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "PR" + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "PR0001";
                }
            }

            return kode;
        }

        public static string AddZero(string limit, long start)
        {
            string output = "";

            int startAt = start.ToString().Length;
            int finishAt = limit.Length - startAt;

            if (startAt == limit.Length)
            {
                finishAt += 1;
            }

            for (int i = 1; i <= finishAt; i++)
            {
                output += "0";
            }

            return output;
        }
    }
}
