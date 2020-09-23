using MarkomApplication.ViewModel;
using MarkomApplication.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Globalization;

namespace MarkomApplication.DataAccess
{
    public class EventDataAccess
    {
        public static string Message = string.Empty;

        //CREATE EVENT
        public static string CreateEvent(EventViewModel paramDataEvent)
        {

            string latestSaveCode = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        t_event c = new t_event();
                        c.code = EventCode();
                        c.event_name= paramDataEvent.eventName;
                        c.place = paramDataEvent.place;
                        c.start_date = paramDataEvent.startDate;
                        c.end_date = paramDataEvent.endDate;
                        c.budget = paramDataEvent.budget;
                        c.status = paramDataEvent.status;
                        c.is_delete = paramDataEvent.isDelete;
                        c.request_by = paramDataEvent.requestBy;
                        c.request_date = paramDataEvent.requestDate;
                        c.note = paramDataEvent.note;
                        c.create_by = paramDataEvent.createBy;
                        c.create_date = paramDataEvent.createDate;

                        db.t_event.Add(c);
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        //get latest save code
                        latestSaveCode = c.code;

                    }
                    catch (Exception ex)
                    {
                        Message = ex.Message;
                        dbContextTransaction.Rollback();
                        
                    }
                }
            }

            return latestSaveCode;

        }


        //GET LIST EVENT
        public static List<EventViewModel> GetListEvent(EventViewModel paramSearch)
        {
            List<EventViewModel> result = new List<EventViewModel>();

            if (paramSearch.statusName != null) {
                paramSearch.status = GetIntStatus(paramSearch.statusName);
            }

            using (var context = new MarkomApplicationDBEntities())
            {
                var res = context.spEventSearch(paramSearch.code, paramSearch.requestByName, paramSearch.requestDate2, paramSearch.status, paramSearch.createDate, paramSearch.createBy);

                List<EventViewModel> comList = res.Select(c => new EventViewModel
                {
                    id = c.id,
                    code = c.code,
                    requestByName = c.first_name +" " +c.last_name,
                    requestDate = c.request_date,
                    status = c.status,
                    statusName = GetStringStatus(c.status),
                    createDate = c.create_date,
                    createBy = c.create_by
                }).ToList();

                result = comList;
            }

            return result;
        }


        //GET DETAIL EVENT
        public static EventViewModel GetDetailEventById(int paramEvId)
        {
            EventViewModel result = new EventViewModel();

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spEventDetailByID(paramEvId);

                result = res.Select(c => new EventViewModel
                {
                    id = c.id,
                    code = c.code,
                    eventName = c.event_name,
                    place = c.place,
                    startDate = c.start_date,
                    endDate = c.end_date,
                    budget = c.budget,
                    requestBy = c.request_by,
                    requestByName = c.first_name + " "+c.last_name,
                    requestDate = c.request_date,
                    note = c.note,
                    status = c.status,
                    statusName = GetStringStatus(c.status)

                }).FirstOrDefault();
            }

            return result;
        }


        //UPDATE EVENT
        public static string UpdateEvent(EventViewModel paramEv)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {

                    ObjectParameter returnId = new ObjectParameter("Code", typeof(string));
                    db.spEventUpdate(
                        paramEv.id
                        ,returnId
                        ,paramEv.eventName
                        ,paramEv.place
                        ,paramEv.startDate
                        ,paramEv.endDate
                        ,paramEv.budget
                        ,paramEv.note
                        ,paramEv.updateBy
                        ,paramEv.updateDate);
                    latestSaveCode = (String)returnId.Value;

                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return latestSaveCode;
        }


        //APPROVE EVENT
        public static string ApproveEv(EventViewModel paramEv)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {

                    ObjectParameter returnId = new ObjectParameter("Code", typeof(string));

                    db.spEventApprove(
                        paramEv.id
                        , returnId
                        ,paramEv.status
                        , paramEv.assignTo
                        , paramEv.approveBy
                        , paramEv.approveDate);

                    latestSaveCode = (String)returnId.Value;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return latestSaveCode;
        }


        //Reject EVENT
        public static string RejectEv(EventViewModel paramEv)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {

                    ObjectParameter returnId = new ObjectParameter("Code", typeof(string));

                    db.spEventReject(
                        paramEv.id
                        , returnId
                        , paramEv.rejectReason
                        ,paramEv.status
                    );

                    latestSaveCode = (String)returnId.Value;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return latestSaveCode;
        }

        //Reject EVENT
        public static string CloseEv(EventViewModel paramEv)
        {
            string latestSaveCode = string.Empty;
            try
            {
                using (var db = new MarkomApplicationDBEntities())
                {

                    ObjectParameter returnId = new ObjectParameter("Code", typeof(string));

                    db.spEventClose(
                        paramEv.id
                        , returnId
                        , paramEv.status
                        ,paramEv.closedDate
                    );

                    latestSaveCode = (String)returnId.Value;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return latestSaveCode;
        }




        public static List<EventViewModel> GetAllEmpName()
        {
            List<EventViewModel> result = new List<EventViewModel>();
            try
            {
                using (MarkomApplicationDBEntities db = new MarkomApplicationDBEntities())
                {
                    var res = db.spGetEmpNameStaff();

                    List<EventViewModel> comList = res.Select(c => new EventViewModel
                    {
                        empId = c.id,
                        empFullName = c.first_name + " " + c.last_name,
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


        public static int? GetEventStatus(int eventId)
        {
            int? result = 0;

            using (var db = new MarkomApplicationDBEntities())
            {
                var evStat = (from te in db.t_event where te.id == eventId select te.status).First();
                result = evStat;
            }

                return result;
        }
        public static string GetStringStatus(int? stat) {
            string result;

            switch (stat)
            {
                case 1:
                    result = "Submited";
                    break;
                case 2:
                    result = "In Progress";
                    break;
                case 3:
                    result = "Done";
                    break;
                case 0:
                    result = "Rejected";
                    break;
                default:
                    result = "";
                    break;
            }


            return result;
        }
        public static int? GetIntStatus(string stat) {
            int? result;
            switch (stat)
            {
                case "submited":
                    result = 1;
                    break;
                case "in progress":
                    result = 2;
                    break;
                case "done":
                    result = 3;
                    break;
                case "rejected":
                    result = 0;
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }
        public static string GetEmployeeName(int empId)
        {

            string result = string.Empty;

            using (var db = new MarkomApplicationDBEntities())
            {
                var res = db.spEmpNameById(empId).Select(em => em.first_name + " " + em.last_name).FirstOrDefault();

                //var q = res.Select(c => new EventViewModel
                //{
                //    empId = c.id,
                //    firstName = c.first_name,
                //    lastName = c.last_name

                //}).FirstOrDefault();

                //result = q;
                result = res;
            }
            return result;
        }

        public static string EventCode()
        {
            string kode = "";

            using (var db = new MarkomApplicationDBEntities())
            {
                var maxCodeNum = db.t_event.Max(t => t.code);

                string d = DateTime.Now.ToString("dd");
                string m = DateTime.Now.ToString("MM");
                string y = DateTime.Now.ToString("yy");

                if (maxCodeNum != null)
                {
                    var max = 50;
                    string lastCode = maxCodeNum.Substring(12, maxCodeNum.Length - 12);

                    if (lastCode.Length < max - 2)
                    {
                        long numCode = long.Parse(lastCode) + 1;
                        kode = "TRWOEV" + d + m + y + AddZero(lastCode, numCode) + numCode.ToString();
                    }
                }
                else
                {
                    kode = "TRWOEV"+ d + m + y + "0001";
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
