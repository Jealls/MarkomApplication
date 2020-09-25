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
    public class RoleDataAccess
    {
        public static string Message = string.Empty;

        public static string CreateRole(RoleViewModel paramDataRole)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_role c = new m_role();
                        c.code = RoleCode();
                        c.name = paramDataRole.name;
                        c.description = paramDataRole.description;
                        c.is_delete = paramDataRole.isDelete;
                        c.create_by = paramDataRole.createBy;
                        c.create_date = paramDataRole.createDate;

                        db.m_role.Add(c);
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

        public static List<RoleViewModel> GetListRole(RoleViewModel paramSearch)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spRoleSearch(paramSearch.code, paramSearch.name, paramSearch.createDate2, paramSearch.createBy);

                List<RoleViewModel> comList = res.Select(c => new RoleViewModel
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

        //GET DETAIL Role
        public static RoleViewModel GetDetailRoleById(int paramRoleId)
        {
            RoleViewModel result = new RoleViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spRoleDetailByID(paramRoleId);

                result = res.Select(c => new RoleViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    description = c.description

                }).FirstOrDefault();
            }

            return result;
        }

        //UPDATE Role
        public static bool UpdateRole(RoleViewModel paramEditEmp)
        {
            bool result = true;
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    db.spRoleUpdate(
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
        public static string DeleteRole(int name)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {
                    ObjectParameter returnId = new ObjectParameter("IdNumber", typeof(string)); //Create Object parameter to receive a output value.It will behave like output parameter  
                    db.spRoleDelete(name, returnId); //calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
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
                var res = db.spRoleCountName(name).FirstOrDefault();
                result = res;
            }

            return result;
        }

        public static List<RoleViewModel> SearchStringRoleCode(string prefix)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spRoleCode(prefix);

                    List<RoleViewModel> roleList = res.Select(c => new RoleViewModel
                    {
                        id = c.id,
                        code = c.code
                    }).ToList();

                    result = roleList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }
        
        public static List<RoleViewModel> SearchStringRoleName(string prefix)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spRoleName(prefix);

                    List<RoleViewModel> roleList = res.Select(c => new RoleViewModel
                    {
                        id = c.id,
                        name = c.name
                    }).ToList();

                    result = roleList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }


        public static string RoleCode()
        {
            string kode = "";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.m_role.Max(mc => mc.code);

                if (maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(2, maxCodeNum.Length - 2);

                    if (lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "RO" + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "RO0001";
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
