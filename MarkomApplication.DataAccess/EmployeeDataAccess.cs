using MarkomApplication.ViewModel;
using MarkomApplication.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.DataAccess
{
    public class EmployeeDataAccess
    {
        public static string Message = string.Empty;

        public static List<EmployeeViewModel> GetListEmployee(EmployeeViewModel paramSearch)
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spEmployeeSearch(paramSearch.code, paramSearch.fullName,paramSearch.mCompanyId, paramSearch.createDate, paramSearch.createBy);

                List<EmployeeViewModel> comList = res.Select(c => new EmployeeViewModel
                {
                    id = c.id,
                    createDate = c.create_date,
                    createBy = c.create_by
                }).ToList();

                result = comList;
            }

            return result;
        }

        public static string CreateEmployee(EmployeeViewModel paramDataEmployee)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        m_employee c = new m_employee();
                        c.employee_number = paramDataEmployee.code;
                        c.first_name = paramDataEmployee.firstName;
                        c.last_name = paramDataEmployee.lastName;
                        c.m_company_id = paramDataEmployee.mCompanyId;
                        c.email = paramDataEmployee.email;
                        c.is_delete = paramDataEmployee.isDelete;
                        c.create_by = paramDataEmployee.createBy;
                        c.create_date = paramDataEmployee.createDate;

                        db.m_employee.Add(c);
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        //get latest save code
                        latestSaveCode = c.employee_number;

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

        
        public static List<EmployeeViewModel> SearchStringCompanyName(string prefix)
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spEmployeeCompanyName(prefix);

                    List<EmployeeViewModel> comList = res.Select(c => new EmployeeViewModel
                    {
                        mCompanyId = c.id,
                        companyName = c.name,
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

        public static List<EmployeeViewModel> SearchStringEmployeeName(string prefix)
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spEmployeeName(prefix);

                    List<EmployeeViewModel> comList = res.Select(c => new EmployeeViewModel
                    {
                        id = c.id,
                        fullName = c.first_name + ' ' + c.last_name
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

        
        public static List<EmployeeViewModel> SearchStringEmployeeNumber(string prefix)
        {
            List<EmployeeViewModel> result = new List<EmployeeViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spEmployeeNumber(prefix);

                    List<EmployeeViewModel> comList = res.Select(c => new EmployeeViewModel
                    {
                        id = c.id,
                        code = c.employee_number
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
    }
}
