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
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 

    public partial class Window1 : Window
    {
        string connectionString;
         
        public Window1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;


        }
        public Window1(DateTime c)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            DateTime y = c;
            today.SelectedDate = c;


        }

        async Task amass(string txt)
        {
             

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("income");
            var col = database.GetCollection<Month>("Months");
            int t = Int32.Parse(txt);
            int date = today.SelectedDate.Value.Month;

             

             
            var tt = await col.Find((x => x._id == (today.SelectedDate.Value.Month.ToString() + "." + today.SelectedDate.Value.Year.ToString()))).ToListAsync();

            int buf0= 0;
            foreach (var temp in tt)
            {
                buf0 += temp.balance;


            }
             

            int need = buf0 - t;
            if (need > 0)
            {
                MessageBox.Show("Эта сумма уже отложена!");
                return;
            }
            else
            {
                int y1, y2 = 0;
                int m1, m2 = 0;
                if (today.SelectedDate.Value.Month > 2)
                {
                    y1 = today.SelectedDate.Value.Year ;
                    y2= today.SelectedDate.Value.Year ;
                    m1 = date - 1;
                    m2 = date - 2;
                }
                else
                {
                    if(today.SelectedDate.Value.Month==1)
                    {
                        y1 = today.SelectedDate.Value.Year-1;
                        y2 = today.SelectedDate.Value.Year - 1;
                        m1 = 12;
                        m2 = 11;
                    }
                    else   
                    {
                        y1 = today.SelectedDate.Value.Year;
                        y2 = today.SelectedDate.Value.Year - 1;
                        m1 = 1;
                        m2 = 12;
                    }
                }
                 
                var filter1 = Builders<Month>.Filter.Eq("curr_month", m1.ToString());
                var filter3 = Builders<Month>.Filter.Eq("year", y1.ToString());
                var filterAnd = Builders<Month>.Filter.And(new List<FilterDefinition<Month>> { filter1, filter3 });
                var filter2 = Builders<Month>.Filter.Eq("curr_month", m2.ToString());
                var filter4 = Builders<Month>.Filter.Eq("year",y2.ToString());
                var filterAnd2 = Builders<Month>.Filter.And(new List<FilterDefinition<Month>> { filter2, filter4 });
                var filterOr = Builders<Month>.Filter.Or(new List<FilterDefinition<Month>> { filterAnd, filterAnd2 });

                var months = await col.Find(filterOr).ToListAsync();
                int buf = 0;
                foreach (var month in months)
                {
                    buf += month.balance;


                }
               
                int need_day = Math.Abs(need / (buf / 61));
                MessageBox.Show("Эта сумма будет накоплена за "+ need_day.ToString()+" дней.");
            }
            





        }
        //MessageBox.Show(need.ToString());

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
             amass(t_Am.Text);


        }

    } }
 
    

