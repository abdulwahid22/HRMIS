using Microsoft.AspNetCore.Mvc;
using NHCM.Domain.Entities;
using NHCM.Persistence;
using NHCM.WebUI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHCM.WebUI.API
{

    [Route("api/[controller]/[action]")]
    public class AttendanceLogger : ControllerBase
    {
        private HCMContext _context;

        public AttendanceLogger(HCMContext context)
        {
            _context = context;
        }
        

        [HttpPost]
        public async Task<IActionResult> Async([FromBody] List<DailyLog> data)
        {

            // Validate the request: return 400 if the request contains 0 no of records.
            if (data.Count >= 1)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (DailyLog log in data)
                        {
                            //Validate the request: rollback the transaction if a duplicate entry is found in the request.
                            if (_context.DailyLog.Where(l => l.UserId == log.UserId && l.AttendanceDate == log.AttendanceDate).Any())
                            {
                                throw new Exception(new StringBuilder()
                                                        .Append($"Attendance record for the employee with id  [{log.UserId}] in [{log.AttendanceDate}]")
                                                        .Append(" already exists").ToString());
                            }
                            else
                            {
                                _context.DailyLog.Add(log);
                            }
                        }


                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        return Ok(new JsonResult(new UIResult() { Text = "Records were logged successfully" }));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        StringBuilder builder = new StringBuilder();
                        builder.Append("Transaction Error").Append("\n")
                               .Append("Message")
                               .Append("\n")
                               .Append(ex.Message);


                        return StatusCode(500, new JsonResult(new UIResult() { Text = builder.ToString() }));
                    }
                }
            }
            else
            {
                return StatusCode(400, new JsonResult(new UIResult() { Text = "Sent list is empty" }));
            }
        }
         

    }
}
