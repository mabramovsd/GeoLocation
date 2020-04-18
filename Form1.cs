using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Windows.Forms;

namespace Geo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "http://free.ipwhois.io/json/" + textBox1.Text);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);

            string sReadData = sr.ReadToEnd();
            response.Close();

            dynamic d = JsonConvert.DeserializeObject(sReadData);
            label1.Text = d.city;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient("https://suggestions.dadata.ru/suggestions/api/4_1/rs/iplocate/address?ip="
                + textBox1.Text);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Token your_token");
            IRestResponse response = client.Execute(request);

            string stream = response.Content;

            dynamic d = JsonConvert.DeserializeObject(stream);
            label2.Text = d.location.value;
        }
    }
}
