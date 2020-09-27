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
    public class MenuDataAccess
    {

        public static string Message = string.Empty;


        public static string CreateMenu(MenuViewModel paramDataMenu)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_menu c = new m_menu();
                        c.code = MenuCode();
                        c.name = paramDataMenu.name;
                        c.controller = paramDataMenu.controller;
                        c.parent_id = paramDataMenu.parentId;
                        c.is_delete = paramDataMenu.isDelete;
                        c.create_by = paramDataMenu.createBy;
                        c.create_date = paramDataMenu.createDate;

                        db.m_menu.Add(c);
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


        public static List<MenuViewModel> GetListMenu(MenuViewModel paramSearch)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spMenuSearch(paramSearch.code, paramSearch.name, paramSearch.createDate2, paramSearch.createBy);

                List<MenuViewModel> menuList = res.Select(c => new MenuViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    createDate = c.create_date,
                    createBy = c.create_by
                }).ToList();

                result = menuList;
            }

            return result;
        }


        //GET DETAIL Menu
        public static MenuViewModel GetDetailMenuById(int paramMenuId)
        {
            MenuViewModel result = new MenuViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spMenuDetailByID(paramMenuId);

                result = res.Select(c => new MenuViewModel
                {
                    id = c.id,
                    code = c.code,
                    name = c.name,
                    controller = c.controller,
                    parentId = c.parent_id,
                    parentName = db.m_menu.Where(x => x.id == c.parent_id).SingleOrDefault()?.name
                }).FirstOrDefault();
            }

            return result;
        }

        //UPDATE Menu
        public static bool UpdateMenu(MenuViewModel paramEditMenu)
        {
            bool result = true;
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    db.spMenuUpdate(
                            paramEditMenu.id
                            ,paramEditMenu.name
                            , paramEditMenu.controller
                            ,paramEditMenu.parentId
                            , paramEditMenu.updateBy
                            , paramEditMenu.updateDate
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
        public static string DeleteMenu(int menuId)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {
                    ObjectParameter returnId = new ObjectParameter("IdNumber", typeof(string)); //Create Object parameter to receive a output value.It will behave like output parameter  
                    db.spMenuDelete(menuId, returnId); //calling our entity imported function "Bangalore" is our input parameter, returnId is a output parameter, it will receive the output value   
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
                var res = db.spMenuCountName(name).FirstOrDefault();
                result = res;
            }

            return result;
        }

        public static List<MenuViewModel> SearchMenuStringCode(string prefix)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spMenuCode(prefix);

                    List<MenuViewModel> codeList = res.Select(c => new MenuViewModel
                    {
                        id = c.id,
                        code = c.code,
                    }).ToList();

                    result = codeList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public static List<MenuViewModel> SearchMenuStringName(string prefix)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spMenuName(prefix);

                    List<MenuViewModel> nameList = res.Select(c => new MenuViewModel
                    {
                        id = c.id,
                        name = c.name
                    }).ToList();

                    result = nameList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }

        public static List<MenuViewModel> SearchMenuStringParent(string prefix)
        {
            List<MenuViewModel> result = new List<MenuViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spMenuParent(prefix);

                    List<MenuViewModel> nameList = res.Select(c => new MenuViewModel
                    {
                        parentId = c.id,
                        parentName = c.name
                    }).ToList();

                    result = nameList;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return result;
        }



        public static string MenuCode()
        {
            string kode = "";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.m_menu.Max(mc => mc.code);

                if (maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(2, maxCodeNum.Length - 2);

                    if (lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "ME" + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "ME0001";
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
