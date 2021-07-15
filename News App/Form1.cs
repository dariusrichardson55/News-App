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
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;

namespace News_App
{
    public partial class Form1 : Form
    {

        // API
        private static SpotifyWebAPI _spotify;
        string clientId = "3e3955b65e54416195986bc285a56b41";
        string clientSecret = "d7dc286ccbd2496a96d91f4038f7829e";
        
        public String NewsKey = "db5f8db514fc496696159cb9697d2e87";
        public String SearchNewsURL = "https://newsapi.org/v2/top-headlines?country=ie&apiKey=";

        public int Articles = 0;
		public int TracksInt = 1;

        public string RequestNewsKey()
        {
            return NewsKey;
        }

        public string RequestSearchURL()
        {
            return SearchNewsURL;
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

		public List<string> Load_Articles()
		{
			string Data = Get(RequestSearchURL() + RequestNewsKey());
			dynamic stuff = JObject.Parse(Data);

			List<string> AllTitles = new List<string>();

			foreach (var Item in stuff.articles)
			{
				string Title = Item.title;
				string Author = Item.author;
				string Desc = Item.description;
				string Img = Item.urlToImage;
				string Pub = Item.publishedAt;
				NewsPanel Stuff = new NewsPanel(Title, Author, Desc, Img, Pub, Articles, panel2);
				AllTitles.Add(Title);
				Articles++;
			}
			return AllTitles;
		}

		public void Load_Tracks(FullTrack Track)
		{
			string Title = Track.Name;
			List<SimpleArtist> Author = Track.Artists;

			foreach (SimpleArtist Person in Author)
			{
				Console.WriteLine(Person.Name);
			}

			SpotifyTrack Stuff = new SpotifyTrack(Title, Author, TracksInt, panel1);
			TracksInt++;
		}

		public List<string> Word_Extractor(string head)
		{
			var sb = new StringBuilder();

			foreach (char c in head)
			{
				if (!char.IsPunctuation(c))
					sb.Append(c);
			}

			head = sb.ToString();
			string notuse = "after a while afterwards at once at this moment at this point before that finally first second third here in the end lastly later on meanwhile next next time now on another occasion previously since soon straightaway then until then when whenever while additionally also as well even furthermore in addition indeed let alone moreover not only alternatively anyway but by contrast differs from elsewhere even so however in contrast in fact in other respects in spite of this in that respect instead nevertheless on the contrary on the other hand rather though whereas yet after all anyway besides moreover besides for example for instance in other words in that that is to say firstly secondly first of all finally lastly for one thing for another in the first place to begin with next in summation to conclude accordingly all the same  an effect of an outcome of an upshot of as a consequence of as a result of because caused by consequently despite this even though hence however in that case moreover nevertheless otherwise so so as stemmed from still then therefore though under the circumstances yet accordingly as a result as exemplified by consequently for example for instance for one thing including provided that since so such as then therefore these include through unless without";
			string[] nousearray = notuse.Split(new string[] { " " }, StringSplitOptions.None);
			List<string> NoUseList = new List<string>();
			NoUseList = nousearray.ToList();

			string[] title = head.Split(new string[] { " " }, StringSplitOptions.None);
			List<string> TitleList = new List<string>();
			TitleList = title.ToList();

			List<string> NewList = new List<string>();
			bool IsBadWord = false;

			foreach (string Word in TitleList)
			{
				//Console.WriteLine(Word);
				IsBadWord = false;
				foreach (string DontUse in NoUseList)
				{
					//Console.WriteLine(DontUse);
					if (Word.ToLower() == (DontUse.ToString()).ToLower())
					{
						IsBadWord = true;
					}
				}

				if (!IsBadWord)
				{
					NewList.Add(Word);
				}
			}
			return NewList;
		}

		// Form Stuff

		public Form1()
        {
            InitializeComponent();
            Opacity = 1;
            TransparencyKey = Color.DarkGray;
            BackColor = Color.DarkGray;
            FormBorderStyle = FormBorderStyle.None;
        }

		private void QueryList(string Query)
		{
			SearchItem item = _spotify.SearchItems(Query, SpotifyAPI.Web.Enums.SearchType.Track,1);
			Paging<FullTrack> Tracks = item.Tracks;
			if (Tracks.Items != null)
			{
				Console.WriteLine(Tracks.Items.Count);
				foreach (FullTrack Track in Tracks.Items)
				{
					Console.WriteLine(Track.Name, Track.Artists);
					Load_Tracks(Track);
				}
				//if (!Tracks.HasNextPage())
				//break;
				//Tracks = _spotify.GetNextPage(Tracks);
			}
		}

        private async void button1_Click(object sender, EventArgs e)
        {

			CredentialsAuth auth = new CredentialsAuth(clientId, clientSecret);
			Token token = await auth.GetToken();
			_spotify = new SpotifyWebAPI()
			{
				AccessToken = token.AccessToken,
				TokenType = token.TokenType
			};

			//FullTrack track = _spotify.GetTrack("3Hvu1pq89D4R0lyPBoujSv");
			//Console.WriteLine("Current Track: " + track.Name); //Yeay! We just printed a tracks name.
			//string Search = textBox1.Text;
			//SearchItem item = _spotify.SearchItems(Search, SpotifyAPI.Web.Enums.SearchType.Album | SpotifyAPI.Web.Enums.SearchType.Playlist);
			//Console.WriteLine(item.Albums.Total);

			List<string> AllTitles = Load_Articles();

			foreach (string Word in AllTitles)
			{
				List<string> ImportantWords = Word_Extractor(Word);
				foreach (string Query in ImportantWords)
				{
					QueryList(Query);
					break;
				}
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

		private void button4_Click(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{

		}

		private void button5_Click(object sender, EventArgs e)
		{

		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
