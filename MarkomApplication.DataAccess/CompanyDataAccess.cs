using MarkomApplication.ViewModel;
using MarkomApplication.DataModel;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace MarkomApplication.DataAccess
{
    public class CompanyDataAccess
    {

        public static string Message = string.Empty;


        public static List<CompanyViewModel> GetListCompany()
        {
            List<CompanyViewModel> result = new List<CompanyViewModel>();

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spCompanyList();

                List<CompanyViewModel> comList = res.Select(c => new CompanyViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    createDate = c.create_date,
                    createBy = c.create_by
                }).ToList();

                result = comList;
            }

            return result;
        }
        public static string CreateCompany(CompanyViewModel paramDataCompany) 
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_company c = new m_company();
                        c.code = CompanyCode();
                        c.name = paramDataCompany.name;
                        c.address = paramDataCompany.address;
                        c.phone = paramDataCompany.phone;
                        c.email = paramDataCompany.email;
                        c.is_delete = paramDataCompany.isDelete;
                        c.create_by = paramDataCompany.createBy;
                        c.create_date = paramDataCompany.createDate;

                        db.m_company.Add(c);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        
                        //get latest save code
                        latestSaveCode = c.code;
                        
                    }
                    catch(Exception ex)
                    {
                        Message = ex.Message;
                        dbContextTransaction.Rollback();
                        //throw;
                    }
                }
            }

         return latestSaveCode;
          
        }


        public static CompanyViewModel GetDetailCompanyById(int paramComId)
        {
            CompanyViewModel result = new CompanyViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spCompanyDetailByID(paramComId);

                result = res.Select(c => new CompanyViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    address = c.address,
                    email = c.email,
                    phone = c.phone
                }).FirstOrDefault();
            }

            return result;
        }


        public static bool UpdateCompany(CompanyViewModel paramEditCompany)
        {
            bool result = true;
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {

                    db.spCompanyUpdate(
                        paramEditCompany.id
                        , paramEditCompany.name
                        , paramEditCompany.email
                        , paramEditCompany.address
                        , paramEditCompany.phone
                        , paramEditCompany.updateBy
                        , paramEditCompany.updateDate
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


        public static string DeleteCompany(int paramComId)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {

                    //latestSaveCode = db.m_company.Where(x => x.id == paramComId).SingleOrDefault()?.name;
                    // ?. will prevent the code from throwing a null-reference exception and will return null when a point with Id  does not exist

                    string tableName = "dbo.m_company";
                    ObjectParameter returnId = new ObjectParameter("Code", typeof(string)); //Create Object parameter to receive a output value.It will behave like output parameter  
                     db.spDeleteRow(paramComId, tableName, returnId); //calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
                    latestSaveCode = (String)returnId.Value;
                    
                    
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return latestSaveCode;
        }


        public static string CompanyCode()
        {
            string kode ="";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.m_company.Max(mc => mc.code);

                if(maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(2, maxCodeNum.Length - 2);
                    
                    if( lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "CP" + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "CP0001";
                }
            }

            return kode;
        }

        public static string AddZero(string limit, long start)
        {
            string output = "";

            int startAt = start.ToString().Length;
            int finishAt = limit.Length - startAt;

            if(startAt == limit.Length)
            {
                finishAt += 1;
            }

            for(int i = 1; i <= finishAt; i++)
            {
                output += "0";
            }

            return output;
        }
    }
}
