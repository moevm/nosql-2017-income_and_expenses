using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace WpfApp1
{
    class Day
    {
        public string _id { get; set; }
        
        public int unexp_income { get; set; }
        public int unexp_expenses { get; set; }
        public int salary { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year{ get; set; }
        public DateTime dateTime { get; set; }
       
    }
    class Month
    {
        public int salary_date { get; set; }
        public string _id { get; set; }
        public int income { get; set; }
        public int expenses { get; set; }
        public int balance { get; set; }
        public string type { get; set; }
        public int curr_month { get; set; }
        public Double in_day { get;set; }
        public int year { get; set; }
        public int exp { get; set; }
    }

}
