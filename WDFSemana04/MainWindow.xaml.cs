using System;
using System.Collections.Generic;
using System.Data;
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

namespace WDFSemana04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("data source=MIGUELAIN ;initial catalog = db_lab03;  Integrated Security = True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("TodosLosNombres", connection);
            command.CommandType = CommandType.StoredProcedure;

            //SqlParameter parameter1 = new SqlParameter();
            //parameter1.SqlDbType = SqlDbType.VarChar;
            //parameter1.Size = 50;
            //parameter1.Value = "";
            //parameter1.ParameterName = "@LastName";

            //SqlParameter parameter2 = new SqlParameter();
            //parameter2.SqlDbType = SqlDbType.VarChar;
            //parameter2.Size = 50;

            //parameter2.Value = "";
            //parameter2.ParameterName = "@FirstName";

            //command.Parameters.Add(parameter1);
            //command.Parameters.Add(parameter2);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader["person_id"].ToString(),
                    LastName = dataReader["last_name"].ToString(),
                    FirstName = dataReader["first_name"].ToString(),
                    FullName = string.Concat(dataReader["first_name"].ToString(), " ",
                    dataReader["last_name"].ToString())

                });
            }

            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}
