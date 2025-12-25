using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;userId=postgres;password=1234";

        void EmployeeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Employees";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }   
        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string EmplpoyeeName = txtEmployeeName.Text;
            string EmployeeSurname = txtEmployeeSurname.Text;
            decimal EmployeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int DepartmentId = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Employees (EmployeeName, EmployeeSurname, EmployeeSalary, DepartmentId) VALUES (@employeeName, @employeenSurname, @employeenSalary, @departmentId)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeName", EmplpoyeeName);
            command.Parameters.AddWithValue("@employeenSurname", EmployeeSurname);
            command.Parameters.AddWithValue("@employeenSalary", EmployeeSalary);
            command.Parameters.AddWithValue("@departmentId", DepartmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarılı");
            connection.Close();
            EmployeeList();
        }
    }
}
