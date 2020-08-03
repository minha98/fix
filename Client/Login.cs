using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Login : Form
    {
        //string username;
        Socket client;
        byte[] data = new byte[1024];
        public Login()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            
           
        }
      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
              if (txtUsername.Text!= "" && txtPassword.Text != "")
            {
                byte[] message = Encoding.UTF8.GetBytes(txtUsername.Text + " " + txtPassword.Text);
               
                try {

                    client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1998);
                    
                    client.Connect(iep);
                    client.Send(message);
                    
                }
                catch
                {

                }

                client.BeginReceive(data,0,data.Length,SocketFlags.None,new AsyncCallback(ReceiveCallBack),client);

               
            }
            else
                MessageBox.Show("Chưa nhập đầy đủ thông tin!!!");
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
           
            int recv = client.EndReceive(ar);
            string keylogin = Encoding.UTF8.GetString(data, 0,recv);
            if (keylogin.Equals("1"))
            {
                MessageBox.Show("Đăng nhập thành công");
                Chat_Client f = new Chat_Client(txtUsername.Text, client);
                f.Show();
                this.Hide();
               
            }
            else
            {
                client.Close();
                MessageBox.Show("Username hoặc mật khẩu không tồn tại trong hệ thống");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void UserName_Click(object sender, EventArgs e)
        {

        }
    }
}
