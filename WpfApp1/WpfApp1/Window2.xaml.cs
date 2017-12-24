
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
using System.Collections;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        string connectionString;
        public Window2()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            InitializeComponent();
        }

        class day2
        {
            public int unexp_income2 { get; set; }
            public int unexp_expenses2 { get; set; }
            
            public string dateTime2 { get; set; }
        }
        async Task stats(string txt)

        {
                Char del = '.';
            ((ArrayList)table.Resources["day228"]).Clear();
        
        string[] sub = txt.Split(del);
        int p1 = Int32.Parse(sub[0]);
        int p2 = Int32.Parse(sub[1]);
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("income");
        var col = database.GetCollection<Day>("Days");
        var filter1 = Builders<Day>.Filter.Eq("month", p1);
        var filter2 = Builders<Day>.Filter.Eq("year", p2);
        var filterAnd = Builders<Day>.Filter.And(new List<FilterDefinition<Day>> { filter1, filter2 });
        var b_days = await col.Find(filterAnd).ToListAsync();
        /* foreach (var day in b_days)
          {
              day2 d = new day2 { dateTime2 = day.dateTime, unexp_income2 = day.unexp_income, unexp_expenses2 = day.unexp_expenses };
              ((ArrayList)table.Resources["day228"]).Add(d);
              table.Items.Add(d);

          }*/
         for (int i=0;i<b_days.Count;i++)
            {
                day2[] d = new day2[b_days.Count];
                d[i] = new day2();
                d[i].dateTime2 = b_days[i].dateTime.ToShortDateString();
                d[i].unexp_income2 = b_days[i].unexp_income;
                d[i].unexp_expenses2 = b_days[i].unexp_expenses;
                ((ArrayList)table.Resources["day228"]).Add(d[i]);
                table.Items.Refresh();
            }
    }
    private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            stats(t_date.Text);
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ((ArrayList)table.Resources["day228"]).Clear();
        }
    }
}
