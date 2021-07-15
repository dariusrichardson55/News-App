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
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;

namespace News_App
{
    class SpotifyTrack
    {
        public SpotifyTrack(string Title,List<SimpleArtist> Artists, int Number, Panel TheParent)
        {
            Console.WriteLine("Track Number" + Number);

            Panel NextPanel = new Panel();
            NextPanel.Location = new Point(0, 0 + ((Number - 1) * 43));
            NextPanel.Size = new Size(200, 40);
            NextPanel.BackColor = Color.DimGray;
            NextPanel.Visible = true;
            NextPanel.Parent = TheParent;

			Button PlayButton = new Button();
			PlayButton.Location = new Point(10, 10);
			PlayButton.AutoSize = false;
			PlayButton.Size = new Size(20,20);
			PlayButton.BackColor = Color.Transparent;
			PlayButton.ForeColor = Color.Transparent;
			PlayButton.Visible = true;
			PlayButton.Parent = NextPanel;
			PlayButton.BackgroundImage = Properties.Resources.PlayButton;
			PlayButton.BackgroundImageLayout = ImageLayout.Stretch;
			PlayButton.FlatStyle = FlatStyle.Flat;
			PlayButton.FlatAppearance.BorderSize = 0;

			Label TrackName = new Label();
			TrackName.Parent = NextPanel;
			TrackName.Location = new Point(39, 9);
			TrackName.AutoSize = true;
			TrackName.Size = new Size(91, 13);
			TrackName.BackColor = Color.DimGray;
			TrackName.ForeColor = Color.White;
			TrackName.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
			TrackName.Text = Number.ToString() + ". " + Title;


			List<string> NewArtists = new List<string>();

			foreach (SimpleArtist Person in Artists)
			{
				NewArtists.Add(Person.Name);
			}
					   
			string[] ArtistArray = NewArtists.ToArray();
			string myString = string.Join(",", ArtistArray);

			Label ArtistsName = new Label();
			ArtistsName.Parent = NextPanel;
			ArtistsName.Location = new Point(40, 22);
			ArtistsName.AutoSize = true;
			ArtistsName.Size = new Size(30, 13);
			ArtistsName.BackColor = Color.DimGray;
			ArtistsName.ForeColor = Color.White;
			ArtistsName.Font = new Font("Microsoft Sans Serif", 8f);
			ArtistsName.Text = myString;

			NextPanel.Show();
            Console.WriteLine("Added Track: " + Title);
        }
    }
}
