using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatClient.ServiceChat;

namespace ChatClient
{
    public partial class Form1 : Form, IService1Callback
    {
        public bool isConnected = false;
        Service1Client client;
        int ID;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();

            }
            else
            {
                ConnectUser();
            }
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new Service1Client(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(textBox1.Text);
                Text = ID.ToString();
                textBox1.Enabled = false;
                isConnected = true;
                button1.Text = "Disconnect";
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                textBox1.Enabled = true;
                isConnected = false;
                button1.Text = "Connect";
            }
        }

        public void MsgCallBack(string msg)
        {
            listBox1.Items.Add(msg);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectUser();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(textBox2.Text, ID);
                    textBox2.Text = string.Empty; 
                }
                
            }
        }

        private void Form1_TextChanged(object sender, EventArgs e)
        {
            client = new Service1Client(new System.ServiceModel.InstanceContext(this));
            var messag = client.ListB();
            listBox1.Items.Clear();
            foreach (string i in messag)
            {
                listBox1.Items.Add(i);
            }
        }
    }
}
