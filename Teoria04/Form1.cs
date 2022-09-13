using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teoria04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            List<Person> people = new List<Person>();
            SqlConnection connection = new SqlConnection(@"data source=DESKTOP-Q9KC4IR\SQLEXPRESS;
            initial catalog = APITecsup2022; Integrated Security=True;");


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@FirstName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Value = txtFirstName.Text.Trim();


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@LastName";
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Value = txtLastName.Text.Trim();

            SqlCommand command = new SqlCommand("USP_GetPeople", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            connection.Open();


            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Person person = new Person
                {
                    PersonID = reader["PersonID"] != DBNull.Value ?(int) reader["PersonID"]:0,
                    FirstName = reader["FirstName"] != DBNull.Value? (string)reader["FirstName"] : string.Empty,
                    LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"]: string.Empty,
                    IsEnable = reader["IsEnable"] != DBNull.Value  ? (bool)reader["IsEnable"] :  false,                    

                };

                people.Add(person);

            }
            connection.Close();
            dgvPeople.DataSource = people;


             


            //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            //DataTable data = new DataTable();
            //dataAdapter.Fill(data);          
            //dgvPeople.DataSource = data;








            
        }
    }
}
