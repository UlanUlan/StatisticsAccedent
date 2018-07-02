using StatisticsAccedent.Module;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace StatisticsAccedent.Pages
{
    /// <summary>
    /// Interaction logic for Page_Stat.xaml
    /// </summary>
    public partial class Page_Stat : Page
    {
        public Page_Stat()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; ;

            SqlCommand sc = new SqlCommand();
            sc.Connection = con;
            sc.CommandText = "SELECT top 3 County, count(*) as c FROM Accidents GROUP BY County Order by c desc;" +
                "SELECT top 3 Age, count(*) as c FROM Accidents GROUP BY Age Order by c desc;" +
                "SELECT top 3 Gender, count(*) as c FROM Accidents GROUP BY Gender Order by c desc;" +
                "SELECT top 3 DayOfWeek, count(*) as c FROM Accidents GROUP BY DayOfWeek Order by c desc";

            con.Open();

            SqlDataReader sdr = sc.ExecuteReader();
            Stat_accident stat_accident = new Stat_accident();
            int s = 0;
            while (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    switch (s)
                    {
                        case 0:
                            stat_accident.topCountry.Add(sdr[0].ToString());
                            break;
                        case 1:
                            stat_accident.topAge.Add(sdr[0].ToString());
                            break;
                        case 2:
                            stat_accident.topGender.Add(sdr[0].ToString());
                            break;
                        case 3:
                            stat_accident.topDaysOfWeek.Add(sdr[0].ToString());
                            break;
                    }
                  
                   
                   // stat_accident.topAge.Add(sdr[0].ToString());
                }
                s++;
                sdr.NextResult();
            }

        }
    }
}
