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
    public class SouvenirDataAccess
    {


        public static string Message = string.Empty;

        public static string CreateSouvenir(SouvenirViewModel paramDataSou)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_souvenir c = new m_souvenir();
                        c.code = SouvenirCode();
                        c.name = paramDataSou.name;
                        c.m_unit_id = paramDataSou.mUnitId;
                        c.description = paramDataSou.description;
                        c.is_delete = paramDataSou.isDelete;
                        c.create_by = paramDataSou.createBy;
                        c.create_date = paramDataSou.createDate;

                        db.m_souvenir.Add(c);
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


        public static List<SouvenirViewModel> GetListSouvenir(SouvenirViewModel paramSearch)
        {
            List<SouvenirViewModel> result = new List<SouvenirViewModel>();

            if (paramSearch.mUnitId != 0) {
                paramSearch.unitId = paramSearch.mUnitId;
            }

            using (var context = new MarkomApplicationDBEntities())
            {

                var res = context.spSouSearch(paramSearch.code, paramSearch.name,paramSearch.unitId, paramSearch.createDate2, paramSearch.createBy);

                List<SouvenirViewModel> comList = res.Select(c => new SouvenirViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    unitName = c.unit_name,
                    createDate = c.create_date,
                    createBy = c.create_by
                }).ToList();

                result = comList;
            }

            return result;
        }

        //GET DETAIL Souvenir
        public static SouvenirViewModel GetDetailSouvenirById(int paramSouvenirId)
        {
            SouvenirViewModel result = new SouvenirViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spSouDetailByID(paramSouvenirId);

                result = res.Select(c => new SouvenirViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    mUnitId = c.m_unit_id,
                    unitName = c.unit_name,
                    description = c.description

                }).FirstOrDefault();
            }

            return result;
        }

        //UPDATE Souvenir
        public static bool UpdateSouvenir(SouvenirViewModel paramEditEmp)
        {
            bool result = true;
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    db.spSouUpdate(
                            paramEditEmp.id
                            , paramEditEmp.name
                            , paramEditEmp.mUnitId
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


        //DELETE 
        public static string DeleteSouvenir(int name)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {
                    ObjectParameter returnId = new ObjectParameter("IdNumber", typeof(string)); //Create Object parameter to receive a output value.It will behave like output parameter  
                    db.spSouDelete(name, returnId); //calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
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
                var res = db.spSouCountName(name).FirstOrDefault();
                result = res;
            }

            return result;
        }



        public static List<SouvenirViewModel> SearchUnitName(string prefix)
        {
            List<SouvenirViewModel> result = new List<SouvenirViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spUnitName(prefix);

                    List<SouvenirViewModel> comList = res.Select(c => new SouvenirViewModel
                    {
                        mUnitId = c.id,
                        unitName = c.name,
                    }).ToList();

                    result = comList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public static List<SouvenirViewModel> SearchSouvenirName(string prefix)
        {
            List<SouvenirViewModel> result = new List<SouvenirViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spSouName(prefix);

                    List<SouvenirViewModel> comList = res.Select(c => new SouvenirViewModel
                    {
                        id = c.id,
                        name = c.name
                    }).ToList();

                    result = comList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public static List<SouvenirViewModel> SearchSouvenirCode(string prefix)
        {
            List<SouvenirViewModel> result = new List<SouvenirViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spSouCode(prefix);

                    List<SouvenirViewModel> comList = res.Select(c => new SouvenirViewModel
                    {
                        id = c.id,
                        code = c.code
                    }).ToList();

                    result = comList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public static string SouvenirCode()
        {
            string kode = "";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.m_souvenir.Max(mc => mc.code);

                if (maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(2, maxCodeNum.Length - 2);

                    if (lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "SV" + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "SV0001";
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
