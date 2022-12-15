using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Newtonsoft.Json;

namespace App3
{
    public class Post
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string userpassword { get; set; }
        public string email { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        private const string url = "http://192.168.0.109/ELECT-BSCOE3-ftl-master/Module06/REST/api_r2.php";
        private const string url_add = "http://192.168.0.109/ELECT-BSCOE3-ftl-master/Module06/REST/api_create.php";
        private const string url_delete = "http://192.168.0.109/ELECT-BSCOE3-ftl-master/Module06/REST/api-delete.php";

        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Post> _post;


        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var content = await _Client.GetStringAsync(url);

            var post = JsonConvert.DeserializeObject<List<Post>>(content);

            _post = new ObservableCollection<Post>(post);
            Post_List.ItemsSource = _post;
            base.OnAppearing();
        }
        private async void OnRefresh(object sender, System.EventArgs e)
        {
            var content = await _Client.GetStringAsync(url);

            var post = JsonConvert.DeserializeObject<List<Post>>(content);

            _post = new ObservableCollection<Post>(post);
            Post_List.ItemsSource = _post;
            base.OnAppearing();
        }


        private async void OnAdd(object sender, System.EventArgs e)
        {
            Post post = new Post { username = xName.Text, email = xemail.Text };

            var content = JsonConvert.SerializeObject(post);
            await _Client.PostAsync(url_add, new StringContent(content, Encoding.UTF8, "application/json"));
            _post.Insert(0, post);
        }

        private static HttpClient client = new HttpClient();

        private async void OnDelete(object sender, System.EventArgs e)
        {


            Post post = new Post { ID = int.Parse(xID.Text) };

            var content = JsonConvert.SerializeObject(post);
            await _Client.PostAsync(url_delete, new StringContent(content, Encoding.UTF8, "application/json"));
            //_post.Insert(0, post);


        }
    }
}
