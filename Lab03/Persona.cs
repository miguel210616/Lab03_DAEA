using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WDFSemana04;

namespace Lab03
{
    public partial class Persona : Form
    {
        SqlConnection conn;
        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string sql = "SELECT * FROM tbl_usuario";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
            }
            else
            {
                MessageBox.Show("La conexión esta cerrada");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string FirstName = txtNombre.Text;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BuscarPersonaNombre";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@FirstName";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Value = FirstName;

                cmd.Parameters.Add(param);


                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();
            }
            else
            {
                MessageBox.Show("La conexión esta cerrada");
            }

        }

        private void btnBuscarDataReader_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                string FirstName = txtNombre.Text;

                List<Person> people = new List<Person>();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BuscarPersonaNombre";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@FirstName";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Value = FirstName;

                cmd.Parameters.Add(param);

                SqlDataReader dataReader = cmd.ExecuteReader();

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

                dgvListado.DataSource = people;
                dgvListado.Refresh();

            }
            else
            {
                MessageBox.Show("La conexión esta cerrada");
            }
        }
    }
}
