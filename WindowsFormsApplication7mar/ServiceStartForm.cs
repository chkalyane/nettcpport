using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using remlbb;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApplication7mar
{
    public partial class ServiceStartForm : Form
    {
        ServiceHost host1,host2; 
        public ServiceStartForm()
        {
            InitializeComponent();
        }

        //NetTCP
        private void button1_Click(object sender, EventArgs e)
        {
            host1 = new ServiceHost(typeof(implementclass));
            NetTcpBinding binding1 = new NetTcpBinding(SecurityMode.None);
            binding1.PortSharingEnabled = true;

            host1.AddServiceEndpoint(typeof(myinterface), binding1, new Uri("net.tcp://localhost:5000/implementclass"));


            host1.Open();
            host2 = new ServiceHost(typeof(secondClass));
            NetTcpBinding binding2 = new NetTcpBinding(SecurityMode.None);
            binding2.PortSharingEnabled = true;


            host2.AddServiceEndpoint(typeof(MySecondInterface), binding2, new Uri("net.tcp://localhost:5000/secondClass"));


            host2.Open();
            MessageBox.Show("started two services");
            button1.Enabled = false;
            button2.Enabled = true;
        }

        //client
        private void button3_Click(object sender, EventArgs e)
        {
            var httpBinding = new WebHttpBinding();
            httpBinding.Security.Mode = WebHttpSecurityMode.Transport;
            httpBinding.Security.Transport = new HttpTransportSecurity() { ClientCredentialType = HttpClientCredentialType.Certificate };
            var httpUri = new Uri(String.Format("https://localhost:8080/implementclass"));
            var httpEndpoint = new EndpointAddress(httpUri);
            //ServiceMetadataBehavior sb = new ServiceMetadataBehavior();
            //sb.HttpsGetEnabled = true;
            //sb.HttpsGetUrl = new Uri("https://localhost:8080/implementclass");


            var factory = new ChannelFactory<myinterface>(httpBinding, httpEndpoint);
            X509Certificate2 trustedCertificate = new X509Certificate2(@"..\newapstest.pfx", "test1@3"); //SSL
            factory.Credentials.ClientCertificate.Certificate = trustedCertificate;
            //var behaviour = new WebHttpBehavior();
            ////behaviour.AddBindingParameters. = new Uri("https://localhost:8080/implementclass");
            ////behaviour.HttpsGetEnabled = true;
            //factory.Endpoint.Behaviors.Add(sb);

            var proxy = factory.CreateChannel();
                proxy.Upload("asdf");
        }

        private void webhttp_click(object sender, EventArgs e)
        {
            host1 = new ServiceHost(typeof(implementclass));
            WebHttpBinding binding1 = new WebHttpBinding();
            binding1.CrossDomainScriptAccessEnabled = true;
            binding1.Security.Mode = WebHttpSecurityMode.Transport;
            binding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
            ServiceMetadataBehavior sb = new ServiceMetadataBehavior();
            sb.HttpsGetEnabled = true;
            sb.HttpsGetUrl = new Uri("https://localhost:443/implementclass");
            host1.Description.Behaviors.Add(sb);
            host1.AddServiceEndpoint(typeof(myinterface), binding1, new Uri("https://localhost:443/implementclass"));
            host1.Open();
            MessageBox.Show("service is started");
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            host1.Close();
            host2.Close();

            button1.Enabled = true;
            button2.Enabled = false;


        }
    }
}
