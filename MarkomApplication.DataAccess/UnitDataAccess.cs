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
    public class UnitDataAccess
    {
        public static string Message = string.Empty;

        public static string CreateUnit(UnitViewModel paramDataUnit)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_unit c = new m_unit();
                        c.code = UnitCode();
                        c.name = paramDataUnit.name;
                        c.description = paramDataUnit.description;
                        c.is_delete = paramDataUnit.isDelete;
                        c.create_by = paramDataUnit.createBy;
                        c.create_date = paramDataUnit.createDate;

                        db.m_unit.Add(c);
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


        public static List<UnitViewModel> GetListUnit(UnitViewModel paramSearch)
        {
            List<UnitViewModel> result = new List<UnitViewModel>();

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spUnitSearch(paramSearch.code, paramSearch.name, paramSearch.createDate2, paramSearch.createBy);

                List<UnitViewModel> comList = res.Select(c => new UnitViewModel
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

        //GET DETAIL Unit
        public static UnitViewModel GetDetailUnitById(int paramUnitId)
        {
            UnitViewModel result = new UnitViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spUnitDetailByID(paramUnitId);

                result = res.Select(c => new UnitViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    description = c.description

                }).FirstOrDefault();
            }

            return result;
        }

        //UPDATE Unit
        public static bool UpdateUnit(UnitViewModel paramEditEmp)
        {
            bool result = true;
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    db.spUnitUpdate(
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
        public static string DeleteUnit(int name)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {
                    ObjectParameter returnId = new ObjectParameter("IdNumber", typeof(string)); //Create Object parameter to receive a output value.It will behave like output parameter  
                    db.spUnitDelete(name, returnId); //calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
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
                var res = db.spUnitCountName(name).FirstOrDefault();
                result = res;
            }

            return result;
        }



        public static string UnitCode()
        {
            string kode = "";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.m_unit.Max(mc => mc.code);

                if (maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(2, maxCodeNum.Length - 2);

                    if (lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "UN" + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "UN0001";
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
