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
using System.Threading;
using System.Collections;
using System.Data.SqlClient;
namespace Server
{
    public partial class Server : Form
    {
       
        delegate void SetText(string text );
        Socket server,client;
        byte[] data=new byte[1024];
        int size = 1024;
        Hashtable clienttable = new Hashtable(30);
       // ArrayList arrlistSocket = new ArrayList();

        public Server()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            
          
        }
           
        private void btnStart_Click(object sender, EventArgs e)
        {
            
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any,1998);
            server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            try
            {
                server.Bind(ipe);
                server.Listen(20);
                server.BeginAccept(new AsyncCallback(CallAccept), server);
            }
            catch
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            
            
            
            txtLog.AppendText("Chờ kết nối.......\r\n");
            
        }



        private void CallAccept(IAsyncResult iar)
        {
            //MessageBox.Show("co client");
            Socket server = (Socket)iar.AsyncState;
            client = server.EndAccept(iar);
            //đăng nhập
            int recv = client.Receive(data);
            string datalogin = Encoding.UTF8.GetString(data, 0, recv);
            string[] account = datalogin.Split(' ');
            
            try
            {

              //  csdl và truy vấn người dùng
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\Cuoiki\Server\login.mdf;Integrated Security=True");
                string sql = "Select count(*) from dangnhap where username='" + account[0] + "' and password='" + account[1] + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                int i = (int)cmd.ExecuteScalar();
                conn.Close();

                 if (i > 0)
                //if (true)
                {

                    addTextResult(account[0] + "  đã vào phòng!!! \r\n");

                    clienttable.Add(account[0],client);

                    client.Send(Encoding.UTF8.GetBytes("1"));
                    //txtLog.Invoke(new Action(() => txtLog.AppendText(client.RemoteEndPoint.ToString())));
                    client.BeginReceive(data, 0, size, SocketFlags.None, new AsyncCallback(ReceiveData), client);

                    server.BeginAccept(new AsyncCallback(CallAccept), server);



                }
                else
                {
                    client.Send(Encoding.UTF8.GetBytes("0"));
                    server.BeginAccept(new AsyncCallback(CallAccept), server);
                }
                server.BeginAccept(new AsyncCallback(CallAccept), server);
            }
            catch
            { }
        }
        
        private void ReceiveData(IAsyncResult iar)
        {

            Socket client = (Socket)iar.AsyncState;
            string mess = Encoding.UTF8.GetString(data,0,data.Length);
            addTextResult(mess);
            try
            {
                data = new byte[1024];
                client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveData), client);
            }
            catch
            {
               
            }

            if (mess != "")
            {
                byte[] datasend = Encoding.UTF8.GetBytes(mess);
                Socket[] arrayClient = new Socket[20];
                clienttable.Values.CopyTo(arrayClient, 0);

                foreach (Socket c in arrayClient)
                {
                    if(c!=null){
                        if(c!=client){
                            c.BeginSend(datasend, 0, datasend.Length, SocketFlags.None,
                        new AsyncCallback(SendData), c);
                        }
                        
                    }
                    
                }
            }

        }
        private void SendData(IAsyncResult iar)
        {
            //Socket server = (Socket)iar.AsyncState;
            //int sent = server.EndSend(iar);
            server.Listen(10);
            server.BeginAccept(new AsyncCallback(CallAccept), server);
        }

        private void addTextResult(string text)
        {
            if (this.txtLog.InvokeRequired)
            {
                SetText d = new SetText(addTextResult);
                //results.Invoke(new addTextResultCallback(addTextResult),new object []{text});
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtLog.AppendText(text); 
            }
        }
        private void UpdateLog(string mess)
        {
            txtLog.AppendText(mess);
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            string mess = "From Server:"+txtMess.Text+"\r\n";
            if(mess!=""){
                byte[] datasend = Encoding.UTF8.GetBytes(mess);
                Socket[] arrayClient=new Socket[20];
                clienttable.Values.CopyTo(arrayClient,0);

                foreach (Socket c in arrayClient)
                {
                    if(c!=null){
                        c.Send(datasend, 0, datasend.Length, SocketFlags.None);
                       // MessageBox.Show("Hello");
                    }
                    
                }
                addTextResult("Server:"+mess);
                txtMess.Clear();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            QuanLyTK ql = new QuanLyTK();
            ql.ShowDialog();
        }
    }
}
