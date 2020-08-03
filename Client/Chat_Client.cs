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
    public partial class Chat_Client : Form
    {
        delegate void SetText(string text);
        Socket client;
        byte[] data = new byte[1024];
        string username;
        public Chat_Client()
        {
            InitializeComponent();
          
            
        }
        public Chat_Client(string username1,Socket client1)
        {
            InitializeComponent();
            username = username1;
            client = client1;
            MessageBox.Show(username);
           
         //   client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveData), client);

        }
        

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"),1998);
            //client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //client.BeginConnect(ipe, new AsyncCallback(Connected), client);
            
        }
        //private void Connected(IAsyncResult iar)
        //{
                            
        //    Socket newsock =(Socket) iar.AsyncState;
        //    try
        //    {
        //        data = new byte[1024];
        //        newsock.EndConnect(iar);
        //        newsock.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveData), newsock);

        //    }
        //    catch 

        //    {
        //        MessageBox.Show("Loi ket noi");
        //    }


        //}

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MessageBox.Show(username);
            byte [] message=new byte[1024];
            if(txtMessage.Text!=""){
            message = Encoding.UTF8.GetBytes("From "+username+": "+txtMessage.Text+"\r\n");
            client.BeginSend(message,0,message.Length,SocketFlags.None,new AsyncCallback(SendData),client);
            txtLog.AppendText("Me:"+txtMessage.Text+"\r\n");
            txtMessage.Clear();
            }
            
        }
        private void SendData(IAsyncResult iar)
        {

            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            remote.BeginReceive(data,0,data.Length,SocketFlags.None,new AsyncCallback(ReceiveData),remote);
        }
         void ReceiveData(IAsyncResult iar)
        {
            
            Socket remote = (Socket)iar.AsyncState;
            int recv = remote.EndReceive(iar);
            
            string stringData = Encoding.UTF8.GetString(data, 0, recv);
            addTextResult(stringData + "\n\r");  
            client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveData), client);

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

        
    }
}
