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

namespace Server
{
    public partial class QuanLyTK : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\Cuoiki\Server\login.mdf;Integrated Security=True");
        public QuanLyTK()
        {
            
            
            InitializeComponent();
            LoadData();
           
            
        }

        private void LoadData()
        {
            conn.Open();
            string sql = "SELECT * FROM dangnhap";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grdataAcc.DataSource = dt;
            conn.Close();
                

            
           
  
           
        
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username=textBox1.Text;
            string password = textBox2.Text;
            conn.Open();
            string str = "insert into dangnhap(username,password) values"+" ("+"'"+username+"'"+","+"'"+password+"'"+")";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.ExecuteNonQuery();
            
            conn.Close();
            LoadData();
        }

        private void grdataAcc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

                DataGridViewRow row = this.grdataAcc.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            conn.Open();
            string str = "delete from dangnhap where username="+"'"+username+"'"+"";
            SqlCommand cmd = new SqlCommand(str, conn);
            int c=(int) cmd.ExecuteNonQuery();
            if(c>0){
                MessageBox.Show("Xóa thành công");
            }
            conn.Close();
            LoadData();
        }
    }
}
