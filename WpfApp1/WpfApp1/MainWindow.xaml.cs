using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
 
using System.Configuration;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        string connectionString;
       



        public MainWindow()
        {

            InitializeComponent();
             connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            datapicker.SelectedDate = DateTime.Today;
           
             

        }
       
        async Task unexp_income(string t)
        {


            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("income");
            var col = database.GetCollection<Day>("Days");
            string txt1 = t;
            int txt = Int32.Parse(txt1);

            string date = datapicker.SelectedDate.Value.ToShortDateString();
            
            
            long check = await col.Find(x => x._id == datapicker.SelectedDate.Value.ToShortDateString()).CountAsync();
            if (check != 0)
            {
                if (cb.IsChecked == false)
                {
                    var filter = Builders<Day>.Filter.Eq("_id", date);
                    var days = await col.Find(filter).ToListAsync();
                    int buf = 0;
                    
                    foreach (var p in days)
                    {
                        buf = p.unexp_income + txt;
                        
                    }

                    var update = Builders<Day>.Update.Set(x => x.unexp_income, buf);
                   
                   ;
                    var result = await col.UpdateOneAsync(filter, update);
                }
                else
                {   
                    
                    var filter = Builders<Day>.Filter.Eq("_id", date);
                    var days = await col.Find(filter).ToListAsync();
                    int buf = 0;
                    
                    foreach (var p in days)
                    {
                        buf = p.unexp_income + txt;
                        
                        
                    }

                    var update = Builders<Day>.Update.Set(x => x.unexp_income, buf);
                    var update2 = Builders<Day>.Update.Set(x => x.salary, txt);
                    
                    var result = await col.UpdateOneAsync(filter, update);
                    var result2 = await col.UpdateOneAsync(filter, update2);
                   
                }
            }
            
            else
            {
                if (cb.IsChecked == true)
                {
                    int m = datapicker.SelectedDate.Value.Month;
                    int y = datapicker.SelectedDate.Value.Year;
                    int d = datapicker.SelectedDate.Value.Day +1;
                    Day day1 = new Day
                    {
                        _id = date,
                        unexp_income = txt,
                        salary=txt,
                        month = datapicker.SelectedDate.Value.Month,
                        year = datapicker.SelectedDate.Value.Year,
                        day= datapicker.SelectedDate.Value.Day,
                        dateTime=new DateTime(y,m,d),
                        

                    };
                    await col.InsertOneAsync(day1);
                }
                else
                {
                    int m = datapicker.SelectedDate.Value.Month;
                    int y = datapicker.SelectedDate.Value.Year;
                    int d = datapicker.SelectedDate.Value.Day+1;
                    Day day1 = new Day
                    {
                        _id = date,
                        unexp_income = txt,
                        month = datapicker.SelectedDate.Value.Month,
                        year = datapicker.SelectedDate.Value.Year,
                        day = datapicker.SelectedDate.Value.Day,
                        dateTime = new DateTime(y, m, d),


                    };
                    await col.InsertOneAsync(day1);
                }
            }

            // параметр обновления

            //var result = await col.UpdateOneAsync(filter, update);
            

        }
        async Task income ()
        {


            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("income");
            var col = database.GetCollection<Day>("Days");
            var col2 = database.GetCollection<Month>("Months");

            string date = datapicker.SelectedDate.Value.ToShortDateString();
            long check = await  col2.Find(x => x._id == (datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString())).CountAsync();
            if (check != 0)
            {

                var income =   await col.Find(x => x.month == datapicker.SelectedDate.Value.Month && x.year == datapicker.SelectedDate.Value.Year).ToListAsync();

                int buf = 0;
                int sal_date = 0;
                foreach (var a in income)
                {
                    buf += a.unexp_income;
                    if (a.salary != 0)
                        sal_date = a.day;
                }

                
                 

                

                var filter = Builders<Month>.Filter.Eq("_id", (datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString()));
               

                var update = Builders<Month>.Update.Set(x => x.income, buf);
                var update2 = Builders<Month>.Update.Set(x => x.salary_date, sal_date);
                var result = await  col2.UpdateManyAsync(filter, update);
                var result2 = await col2.UpdateManyAsync(filter, update2);
            }

            else
            {
                 var income = await col.Find(x => x.month == datapicker.SelectedDate.Value.Month && x.year == datapicker.SelectedDate.Value.Year).ToListAsync();

                int buf = 0;
                int sal_date = 0;
                foreach (var a in income)
                {
                    buf += a.unexp_income;
                    if (a.salary != 0)
                        sal_date = a.day;
                }

                int y1 = 0;
                int m1 = 0;
                if (datapicker.SelectedDate.Value.Month > 1)
                {
                    y1 = datapicker.SelectedDate.Value.Year;

                    m1 = datapicker.SelectedDate.Value.Month - 1;

                }
                else
                {

                    y1 = datapicker.SelectedDate.Value.Year - 1;

                    m1 = 12;



                }

                var filter1 = Builders<Month>.Filter.Eq("curr_month", m1.ToString());
                var filter3 = Builders<Month>.Filter.Eq("year", y1.ToString());
                var filterAnd = Builders<Month>.Filter.And(new List<FilterDefinition<Month>> { filter1, filter3 });
                int buf_exp = 0;


                var last_month = await col2.Find(filterAnd).ToListAsync();
                foreach (var l in last_month)
                {
                    buf_exp = l.balance;
                }

                Month month1 = new Month
                {
                    _id = datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString(),
                    income = buf,
                    balance = buf,
                    expenses = 0,
                    salary_date = sal_date,
                    type = "balance",
                    curr_month = datapicker.SelectedDate.Value.Month,
                    in_day=0.0,
                    year=datapicker.SelectedDate.Value.Year,
                    exp=buf_exp,

                };
                    await col2.InsertOneAsync(month1);
                
            }

            expenses();
            

        }

        async Task expenses()
        {


            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("income");
            var col = database.GetCollection<Day>("Days");
            var col2 = database.GetCollection<Month>("Months");

            string date = datapicker.SelectedDate.Value.ToShortDateString();
            long  check = await  col2.Find(x => x._id == (datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString())).CountAsync();
            if (check != 0)
            {

                var exp = await col.Find(x => x.month == datapicker.SelectedDate.Value.Month && x.year == datapicker.SelectedDate.Value.Year).ToListAsync();

                int buf = 0;
                foreach (var a in exp)
                {
                    buf += a.unexp_expenses;
                }
                var filter = Builders<Month>.Filter.Eq("_id", (datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString()));



                var update = Builders<Month>.Update.Set(x => x.expenses, buf);
                var result =  await col2.UpdateManyAsync(filter, update);
            }

            else
            {
                var exp = await col.Find(x => x.month == datapicker.SelectedDate.Value.Month && x.year == datapicker.SelectedDate.Value.Year).ToListAsync();

                int buf = 0;
                foreach (var a in exp)
                {
                    buf += a.unexp_expenses;
                }

                int y1 = 0;
                int m1 = 0;
                if (datapicker.SelectedDate.Value.Month > 1)
                {
                    y1 = datapicker.SelectedDate.Value.Year;

                    m1 = datapicker.SelectedDate.Value.Month - 1;

                }
                else
                {

                    y1 = datapicker.SelectedDate.Value.Year - 1;

                    m1 = 12;



                }

                var filter1 = Builders<Month>.Filter.Eq("curr_month", m1.ToString());
                var filter3 = Builders<Month>.Filter.Eq("year", y1.ToString());
                var filterAnd = Builders<Month>.Filter.And(new List<FilterDefinition<Month>> { filter1, filter3 });
                int buf_exp = 0;


                var last_month = await col2.Find(filterAnd).ToListAsync();
                foreach (var l in last_month)
                {
                    buf_exp = l.balance;
                }

                Month month1 = new Month
                {
                    _id = datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString(),
                    expenses = buf,
                    balance = -buf,
                    type = "balance",
                    curr_month = datapicker.SelectedDate.Value.Month,
                    in_day=0.0,
                    year=datapicker.SelectedDate.Value.Year,
                    exp=buf_exp,

                };
                 await  col2.InsertOneAsync(month1);
            }
            balance();
        }







        async Task balance()
           {





               var client = new MongoClient(connectionString);
               var database = client.GetDatabase("income");

               var col2 = database.GetCollection<Month>("Months");

               string date = datapicker.SelectedDate.Value.ToShortDateString();


               var temp = await col2.Find(x => x._id==(datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString())).ToListAsync();

               int buf = 0;
               foreach (var a in temp)
               {
                   buf = a.exp+a.income-a.expenses;
               }
               var filter =  Builders<Month>.Filter.Eq("_id", (datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString()));
            var update = Builders<Month>.Update.Set(x => x.balance, buf);
            var result = await col2.UpdateManyAsync(filter, update);

            var temp2 = await col2.Find(x => x._id == (datapicker.SelectedDate.Value.Month.ToString() + "." + datapicker.SelectedDate.Value.Year.ToString())).ToListAsync();

            int[] buf2 = { 0 };
            foreach(var a in temp2)
            {
                buf2[0] = a.balance;
            }

            listBox1.ItemsSource = buf2;
             
               

            





           }


       



        async Task add_expenses(string t)
        {


            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("income");
            var col = database.GetCollection<Day>("Days");
            string txt1 = t;
            int txt = Int32.Parse(txt1);

            string date = datapicker.SelectedDate.Value.ToShortDateString();
            // var filter = Builders<Day>.Filter.Eq("_id", date);
            // var update = Builders<Day>.Update.Set("expenses", t);
            long check = await col.Find(x => x._id == datapicker.SelectedDate.Value.ToShortDateString()).CountAsync();
            if (check != 0)
            {
                var filter = Builders<Day>.Filter.Eq("_id", date);
                var days = await col.Find(filter).ToListAsync();
                
                int buf = 0;
                
                foreach (var p in days)
                {
                    buf = p.unexp_expenses + txt;
                    
                }

                var update = Builders<Day>.Update.Set(x => x.unexp_expenses, buf);
               
                var result = await col.UpdateOneAsync(filter, update);
               


            }

            else
            {

                int m = datapicker.SelectedDate.Value.Month;
                int y = datapicker.SelectedDate.Value.Year;
                int d = datapicker.SelectedDate.Value.Day+1;
                Day day1 = new Day
                {
                    _id = date,

                    unexp_expenses = txt,
                    month = datapicker.SelectedDate.Value.Month,
                    year = datapicker.SelectedDate.Value.Year,
                    dateTime =  new DateTime(y,m,d),
                    day= datapicker.SelectedDate.Value.Day,


                };

                
                await col.InsertOneAsync(day1);
            }

        }

      

       


        private     void button_Click(object sender, RoutedEventArgs e)
        {   

                 unexp_income(textBox.Text);
            textBox.Text = "0";
            
               
           
            
            MessageBox.Show("Запись добавлена");
            
           
        }
        

        private   void button1_Click(object sender, RoutedEventArgs e)
        {
            
            
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private        void Button_Click_1(object sender, RoutedEventArgs e)
        {
                    income()  ;
              //await   expenses();
               //  await    balance();                                  
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            add_expenses(textBox1.Text);
            MessageBox.Show("Запись добавлена");
            textBox1.Text = "0";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window1 amass = new Window1(datapicker.SelectedDate.Value);
            amass.Owner = this;

            amass.Show();
        }

        async Task amass(string txt)
        {

            //  int t = Int32.Parse(txt);
            //   var client = new MongoClient(connectionString);
            //   var database = client.GetDatabase("income");


            //   var col2 = database.GetCollection<Day>("Days");


            // /*  var days = await col2.Find(x => x.month == today.SelectedDate.Value.Month && x.day <= today.SelectedDate.Value.Day ||
            //   x.month == (today.SelectedDate.Value.Month) - 1 || x.month == (today.SelectedDate.Value.Month) - 2).ToListAsync();
            //  */
            // var days = await col2.Find(x => x.month == today.SelectedDate.Value.Month && x.day <= today.SelectedDate.Value.Day).ToListAsync();

            //int[] buf=new int [days.Count];

            //       for(int i=0; i<=days.Count;i++)
            //       {
            //      buf[i] = days[i].unexp_income-days[i].unexp_expenses;

            //       }

            // int c = buf.Length;
            // MessageBox.Show(c.ToString());


            // var client = new MongoClient(connectionString);
            // var database = client.GetDatabase("income");
            // var col = database.GetCollection<Day>("Days");

            // int t = Int32.Parse(txt);
            // string date = datapicker.SelectedDate.Value.Month.ToString();
            // var filter = Builders<Day>.Filter.Eq("month", date);
            // var days = await col.Find(filter).ToListAsync();

            //// var days = await col.Find(x => x.month == datapicker.SelectedDate.Value.Month  ).ToListAsync();
            // MessageBox.Show(days.Count.ToString());

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("income");
            var col = database.GetCollection<Month>("Months");
            int t = Int32.Parse(txt);
            int date = datapicker.SelectedDate.Value.Month;
            var filter1 = Builders<Month>.Filter.Eq("curr_month", (date-1).ToString());
            
            var filter2 = Builders<Month>.Filter.Eq("curr_month", (date - 2).ToString());
            var filterOr = Builders<Month>.Filter.Or(new List<FilterDefinition<Month>> { filter1, filter2 });

            var months = await col.Find(filterOr).ToListAsync();
            int buf=0;
            foreach(var month in months)
            {
                buf += month.balance;
                
                
            }
            MessageBox.Show(buf.ToString());
            double inday = buf / 61;
            var update = Builders<Month>.Update.Set(x => x.in_day, inday);

            var result = await col.UpdateOneAsync(filterOr, update);

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window stat = new Window2();
            stat.Owner = this;
            stat.Show();
        }
        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //  {
        //    amass(textBox.Text);
        // }
    }
}
