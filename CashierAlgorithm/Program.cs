using System;
using System.Collections.Generic;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using CashierAlgorithm.Algorithms;
using CashierAlgorithm.Database;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace CashierAlgorithm
{


    class Program
    {
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }
        public static List<DateTime> GetFreeDays()
        {
            List<DateTime> freeDays = new();
            bool keepAsking = true;
            while (keepAsking)
            {
                Console.WriteLine("Insert the date of the free day. Type \"stop\" if you finished.");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime userDateTime))
                {
                    bool dateDecision = false;
                    while (dateDecision != true)
                    {
                        Console.WriteLine("\nYou've selected {0} as a free-from-work date. \nConfirm? (type \"yes\", or \"no\")", userDateTime);
                        var x = Console.ReadLine();
                        if (x == "yes")
                        {
                            freeDays.Add(userDateTime);
                            dateDecision = true;
                        }
                        else if (x == "no")
                        {
                            dateDecision = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect decision entered.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect value.");
                }
                Console.WriteLine("Do you wish to enter another date? (type \"yes\", or \"no\")");
                var y = Console.ReadLine();
                if (y == "yes")
                {
                    continue;
                }
                else if (y == "no")
                {
                    keepAsking = false;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect value.");
                }
            }
            return freeDays;
        }

        static void InsertPeopleToDb()
        {
            List<PersonInfo> personInfos = new();
            var x = File.ReadAllLines(@"C:\Users\ASUS\Desktop\names.txt");
            foreach (var item in x)
            {
                var y = item.Split(' ');
                personInfos.Add(new PersonInfo(y[0], y[1]));
            }
            var hospital1people = personInfos.Take(16); //484, 16
            var hospital2people = personInfos.Skip(16).Take(16); //468, 32
            var fourbrigade1 = personInfos.Skip(32).Take(40); //396 , 72 
            var fourbrigade2 = personInfos.Skip(72).Take(40); // 324, 144 
            var lazyrotation1 = personInfos.Skip(144).Take(3);
            var lazyrotation2 = personInfos.Skip(147).Take(3);
            var fourshift1 = personInfos.Skip(150).Take(50);
            var fourshift2 = personInfos.Skip(200).Take(50);

            using (var db = new TmpContext())
            {
                db.Database.EnsureCreated();
                foreach (var item in hospital1people)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 29,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    }); ;
                }

                foreach (var item in hospital2people)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 44,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }

                foreach (var item in fourbrigade1)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 49,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }

                foreach (var item in fourbrigade2)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 51,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }

                foreach (var item in lazyrotation1)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 58,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }

                foreach (var item in lazyrotation2)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 59,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }

                foreach (var item in fourshift1)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 62,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }

                foreach (var item in fourshift2)
                {
                    db.Add(new Workers()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DepartmentId = 65,
                        Role = 1,
                        SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo
                        {
                            TeamNumber = null,
                            IsWeekendWorker = null,
                            GuaranteedFreeDay = null,
                            OverTime = null
                        })
                    });
                }
                db.SaveChanges();
            }

        }
        internal class PersonInfo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public PersonInfo(string FirstName, string LastName)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
            }
        }
        static void Main(string[] args)
        {

           var xx = FourBrigadeSystem.CreateShifts();
            foreach (var item in xx)
            {
                Console.WriteLine("\n" +item.Key+"\n");
                foreach (var item2 in item.Value)
                {
                    Console.WriteLine(item2.ShiftBegin.ToString() + " " + item2.ShiftEnd.ToString());
                }
            }

        }
    }
}
