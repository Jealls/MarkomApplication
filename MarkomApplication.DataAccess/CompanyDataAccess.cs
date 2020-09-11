using MarkomApplication.ViewModel;
using MarkomApplication.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.DataAccess
{
    public class CompanyDataAccess
    {

        public static string Message = string.Empty;

        public static bool CreateCompany(CompanyViewModel paramDataCompany) {
            bool result = true;

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
                    }
                    catch(Exception ex)
                    {
                        result = false;
                        Message = ex.Message;
                        dbContextTransaction.Rollback();
                        //throw;
                    }
                }
            }
            return result;
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
