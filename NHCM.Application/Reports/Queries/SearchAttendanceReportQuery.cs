using MediatR;
using Microsoft.EntityFrameworkCore;
using NHCM.Application.Reports.Models;
using NHCM.Domain.Entities;
using NHCM.Domain.ViewsEntities;
using NHCM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHCM.Application.Reports.Queries
{
    public class SearchAttendanceReportQuery : IRequest<List<MonthlyRecord>>
    { 

        public int? DepartmentID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        
    }


    public class SearchAttendanceReportQueryHandler : IRequestHandler<SearchAttendanceReportQuery, List<MonthlyRecord>>
    {
        private HCMContext _context;
        public SearchAttendanceReportQueryHandler(HCMContext context)
        {
            _context = context;
        } 

        public async Task<List<MonthlyRecord>> Handle(SearchAttendanceReportQuery request, CancellationToken cancellationToken)
        {
            List<vUserInfo> DepUsers = new List<vUserInfo>();
            List<vUserInfo> FilteredUsers = new List<vUserInfo>();
            DepUsers = await _context.VUserInfos.FromSql("select * from att.vuserinfo").ToListAsync();

            if (request.DepartmentID != null)
            {
                FilteredUsers = DepUsers.Where(u => u.departmentid == request.DepartmentID).ToList();
            }
            else
            {
                FilteredUsers = DepUsers;
            }
             

            List<MonthlyRecord> ResultList = new List<MonthlyRecord>();
            DateTime StartDate = PersianLibrary.PersianDate.ToDate(request.Year, request.Month, 1);
            PersianLibrary.PersianDate SPD = PersianLibrary.PersianDate.Convert(StartDate);

            String MonthYear = "ماه " + SPD.MonthString + " سال " + SPD.Year;
            int LastDate = 31;
            if (request.Month > 6 && request.Month < 12) LastDate = 30;
            if (request.Month == 12) LastDate = 29;

            DateTime EndDate = PersianLibrary.PersianDate.ToDate(request.Year, request.Month, LastDate);
            DateTime Counter = StartDate;
             
            List<DateTime> DateList = new List<DateTime>();

            while (Counter.Date < EndDate.Date)
            {
                if (!Counter.DayOfWeek.Equals(DayOfWeek.Friday))
                {
                    DateList.Add(Counter.Date);
                }
                Counter = Counter.AddDays(1);
            }



            foreach (vUserInfo User in FilteredUsers)
            {
                MonthlyRecord MR = new MonthlyRecord();
                MR.Present = 0;
                MR.Leave = 0;
                MR.Absent = 0;
                MR.NameLocal = User.FullName;
                MR.FatherNameLocal = User.FatherName;
                MR.Bast = User.bast;
                MR.Qadam = User.qadam;
                MR.UserID = User.userid.ToString();
                MR.DeptID = User.departmentid;
                MR.Department = User.DepartmentText;
                MR.TITLE = User.Title;
                 
                MR.AbsentDates = "<ul>$list</ul>";
                String absentlist = "";

                foreach (DateTime Date in DateList)
                {
                    List<DailyLog> AttResult = _context.DailyLog.Where(c => c.UserId == User.userid && c.CheckIn.Date.Equals(Date)).ToList();
                    if (AttResult.Any())
                    {
                        MR.Present++;
                    }
                    else
                    {
                        PersianLibrary.PersianDate PD = PersianLibrary.PersianDate.Convert(Date);
                        String PDString = PD.DayOfWeek + "، " + PD.Day + " " + PD.MonthString + " " + PD.Year;
                        absentlist = absentlist + "<li>" + PDString + "</li>";
                        MR.Absent++;
                    }
                }

                MR.AbsentDates = MR.AbsentDates.Replace("$list", absentlist);
                ResultList.Add(MR);
            }
            return ResultList;
        }
    }
}
