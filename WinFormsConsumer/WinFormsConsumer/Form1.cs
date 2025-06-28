using System.Net.Http.Json;
namespace WinFormsConsumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //Build Request "URL" Verb
            //Extension Method Json "Ser OR DeSer"
            HttpClient httpClient = new HttpClient();
            var empList = await httpClient.GetFromJsonAsync<List<Emp>>("http://localhost:5005/api/Employee");
            label1.Text = "";

            foreach (var item in empList)
            {
                label1.Text += item.name + "\n";
            }
        }
    }
}
