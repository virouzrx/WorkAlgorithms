using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CashierAlgorithm.Database;
using Newtonsoft.Json;

namespace CashierAlgorithm.Algorithms
{
    public class FourBrigadeSystem : ISchedule
    {
        public string Generate(int coordinatorId, DateTime date)
        {
            var teams = GetTeamsToAlgorithm();
            var shifts = CreateShifts();
            var leaves = GetLeavesInfo();
            return "";
        }
        private object GetLeavesInfo()
        {
            throw new NotImplementedException();
        }
        public List<DateTime> GetWorkingDays()
        {
            throw new NotImplementedException();
        }
        public static Dictionary<int, List<Workers>> GetTeamsToAlgorithm()
        {

            var workerlist = new List<Workers>();

            using (var db = new TmpContext())
            {
                workerlist = db.Workers
                    .Where(x => x.DepartmentId == 49)
                    .ToList();

                var AssignedWorkers = workerlist
                    .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber != null)
                    .ToList();

                if (!AssignedWorkers.Any()) //workers have never been assigned
                {
                    return AssignAllWorkersToTeams(workerlist);
                }
                else if (AssignedWorkers.Count != workerlist.Count) //new workers were added to database
                {
                    ;
                    return AssignNewWorkersToAlreadyExistingTeams(workerlist);
                }
                else
                {
                    return GetTeams(workerlist);
                }
            }


        }
        public static Dictionary<int, List<Workers>> GetTeams(List<Workers> workerlist)
        {
            Dictionary<int, List<Workers>> Teams = new();
            for (int i = 0; i < 4; i++)
            {
                var team = workerlist
                    .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber == i + 1)
                    .ToList();
                Teams.Add(i + 1, team);
            }
            return Teams;
        }
        private static Dictionary<int, List<Workers>> AssignNewWorkersToAlreadyExistingTeams(List<Workers> workerlist)
        {
            Dictionary<int, List<Workers>> Teams = new();
            for (int i = 0; i < 4; i++)
            {
                var team = workerlist
                    .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber == i + 1)
                    .ToList();
                Teams.Add(i + 1, team);
            }
            var unassignedworkers = workerlist.Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber == null);
            using (var db = new TmpContext())
            {
                foreach (var item in unassignedworkers)
                {
                    var TeamToAdd = Teams.OrderBy(x => x.Value.Count).First();
                    item.SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo { TeamNumber = TeamToAdd.Key });
                    db.Update(item);
                    Teams[TeamToAdd.Key].Add(item);
                }
                db.SaveChanges();
            }

            return Teams;
        }
        public static Dictionary<int, List<Workers>> AssignAllWorkersToTeams(List<Workers> workerlist)
        {
            var Teams = new Dictionary<int, List<Workers>>();
            var WorkerAmount = workerlist.Count / 4;
            using (var db = new TmpContext())
            {
                for (int i = 0; i < 4; i++)
                {
                    var team = workerlist.Skip(i * WorkerAmount).Take(WorkerAmount).ToList();
                    foreach (var item in team)
                    {
                        item.SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo { TeamNumber = i + 1 });
                        db.Update(item);
                    }
                    Teams.Add(i + 1, team);
                }
                db.SaveChanges();
            }
            return Teams;
        }



        public static Dictionary<int, List<ShiftInfo>> CreateShifts()
        {

            var Shifts = GetShiftInfoFromUser();
            Dictionary<int, List<ShiftInfo>> ShiftList = new();
            using (var db = new TmpContext())
            {
                var schedules = db.Schedules.ToList();
                /* if (schedules.Any())
                 {
                     var lastschedule = db.Schedules
                         .Where(x => x.Id == Shifts.FirstOrDefault().coordinatorId)
                         .OrderByDescending(x => x.ScheduleMonth)
                         .Last();

                     if (lastschedule.ScheduleMonth.Month - DateTime.Now.Month == 1)
                     {
                         var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<int, List<ShiftInfo>>>(lastschedule.ScheduleInJSON);
                         var 
                         foreach (var item in keyValuePairs)
                         {

                         }

                     }
                 }*/
                //else
                // {

                var dateTimeList = new List<ShiftInfo>();
                var dateTimeListToAdd = new List<ShiftInfo>();
                int z = 0;
                int x = 0;
                int y = 0;
                var time = DateTime.DaysInMonth(2020, 8);
                foreach (var item in Shifts)
                {
                    if (item.IsOvernight == false)
                    {
                        for (int i = 1; i < time; i++)
                        {
                          
                            var begin = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetBeginTime.Hour,
                                item.ShiftSetBeginTime.Minute, item.ShiftSetBeginTime.Second);
                            var end = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetEndTime.Hour,
                                item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                            dateTimeList.Add(new ShiftInfo(begin, end));
                            y++;
                            if (dateTimeList.Count == item.ShiftLengthInDays)
                            {
                                dateTimeListToAdd = dateTimeList.ConvertAll(x => (ShiftInfo)x);
                                if (z == 0)
                                {
                                    x = Shifts.IndexOf(item) + 1;
                                    ShiftList.Add(x, dateTimeListToAdd);
                                    z++;
                                    y = 0;
                                }
                                else
                                {
                                    ShiftList.Add(x+3, dateTimeListToAdd);
                                    x = x+3;
                                    y = 0;
                                }
                                dateTimeList.Clear();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < DateTime.DaysInMonth(DateTime.Now.Year, item.ScheduleMonth); i++)
                        {
                            
                            var begin = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetBeginTime.Hour,
                                item.ShiftSetBeginTime.Minute, item.ShiftSetBeginTime.Second);
                            var end = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i + 1, item.ShiftSetEndTime.Hour,
                                item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                            dateTimeList.Add(new ShiftInfo(begin, end));
                            y++;
                            if (dateTimeList.Count == item.ShiftLengthInDays)
                            {
                                dateTimeListToAdd = dateTimeList.ConvertAll(x => (ShiftInfo)x);
                                if (z == 0)
                                {
                                    x = Shifts.IndexOf(item) + 1;
                                    ShiftList.Add(x, dateTimeListToAdd);
                                    z++;
                                    y = 0;
                                }
                                else
                                {
                                    ShiftList.Add(x + 3, dateTimeListToAdd);
                                    x = x + 3;
                                    y = 0;
                                }
                                dateTimeList.Clear();
                               
                            }
                        }
                      
                        //}
                    }
                    //dateTimeList.Clear();
                    dateTimeList.Clear();
                    dateTimeListToAdd.Clear();
                    z = 0;
                    x = z;
                }
            }
            return ShiftList;
        }
        //miesiac, dlugosc zmiany i id koorda
        private static List<ShiftInfoForScheduleGenerating> GetShiftInfoFromUser()
        {

            List<ShiftInfoForScheduleGenerating> sifsg = new();
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 6, 0, 0), new DateTime(2020, 5, 31, 14, 0, 0), 8, 4, 1, false)); // 6-14
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 14, 0, 0), new DateTime(2020, 5, 31, 22, 0, 0), 8, 4, 1, false)); //14-22
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 22, 0, 0), new DateTime(2020, 5, 31, 6, 0, 0), 8, 4, 1, true));//22-6

            return sifsg;

        }
    }
}

