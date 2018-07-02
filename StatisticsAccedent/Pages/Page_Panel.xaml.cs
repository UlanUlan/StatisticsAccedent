using OfficeOpenXml;
using StatisticsAccedent.Module;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for Page_Panel.xaml
    /// </summary>
    public partial class Page_Panel : Page
    {
        List<Accident> accidents = new List<Accident>();
        public Page_Panel()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string path = @"C:\Users\ГерценЕ\Documents\visual studio 2015\Projects\StatisticsAccedent\StatisticsAccedent\2010 Fatalities by Type.xlsx";
            FileInfo template = new FileInfo(path);

            //  ExcelPackage package = new ExcelPackage();
            using (ExcelPackage package =
                new ExcelPackage(template, true))
            {
                ExcelWorksheet workshet =
                    package.Workbook.Worksheets["Motorcyles"];

                for (int i = 2; i < workshet.Dimension.End.Row; i++)
                {
                    //var test = workshet.Cells[i, 1].Value;
                    Accident accident = new Accident();
                    accident.AccidentDate = DateTime.Parse(workshet.Cells[i, 1].Value.ToString());
                    accident.DayOfWeek = workshet.Cells[i, 2].Value.ToString();
                    accident.Country = workshet.Cells[i, 4].Value.ToString();
                    accident.Age = Int32.Parse(workshet.Cells[i, 12].Value.ToString());
                    accident.Gender = workshet.Cells[i, 13].Value.ToString();

                    accidents.Add(accident);
                }  
            }
            // Two step

            SqlConnection con = new SqlConnection();
            con.ConnectionString = 
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            SqlCommand sc = new SqlCommand();
            sc.Connection = con;

            
            try
            {
                con.Open();
                tbStatus.Text += con.State;
                foreach (Accident item in accidents)
                {
                    string sf = string.Format("INSERT INTO Accidents (AccidentDate, County, Age, Gender, DayOfWeek)" +
                                                       "VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                                       string.Format("{0:yyyy-MM-dd}",item.AccidentDate.Date), item.Country, item.Age, item.Gender, item.DayOfWeek
                                                       );
                    sc.CommandText = sf;

                    sc.ExecuteNonQuery();
                }
            }catch(Exception ex)
            {
                tbStatus.Text += ex.Message;
            }
            finally { con.Close(); }
        }
    }
}
