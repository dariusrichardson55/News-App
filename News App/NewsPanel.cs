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

namespace News_App
{
    class NewsPanel
    {
        public NewsPanel(string Title, string Author, string Description, string Image, string PublishedAt, int Number, Panel TheParent)
        {
            Console.WriteLine("Articles Number" + Number);
            Panel NextPanel = new Panel();
            NextPanel.Location = new Point(10, 0 + (Number * 110));
            NextPanel.Size = new Size(490, 106);
            NextPanel.BackColor = Color.DimGray;
			NextPanel.Visible = true;
            NextPanel.Parent = TheParent;

            PictureBox NewsImage = new PictureBox();
            NewsImage.Parent = NextPanel;
            NewsImage.Location = new Point(3, 3);
            NewsImage.Size = new Size(100, 100);
            NewsImage.Visible = true;
            NewsImage.ImageLocation = Image;

            Label TitleText = new Label();
            TitleText.Parent = NextPanel;
            TitleText.Location = new Point(107, 1);
            TitleText.AutoSize = false;
            TitleText.Size = new Size(300, 50);
            TitleText.BackColor = Color.Transparent;
            TitleText.ForeColor = Color.White;
            TitleText.Font = new Font("Arial", 8f, FontStyle.Bold);
            TitleText.Text = Title;

            Label AuthorText = new Label();
            AuthorText.Parent = NextPanel;
            AuthorText.Location = new Point(109, 27);
            AuthorText.AutoSize = false;
            AuthorText.Size = new Size(154, 13);
            AuthorText.BackColor = Color.Transparent;
            AuthorText.ForeColor = Color.White;
            AuthorText.Font = new Font("Arial", 8f, FontStyle.Regular);
            AuthorText.Text = Author;

            Label DescriptionText = new Label();
            DescriptionText.Parent = NextPanel;
            DescriptionText.Location = new Point(109, 51);
            DescriptionText.AutoSize = false;
            DescriptionText.Size = new Size(308, 52);
            DescriptionText.BackColor = Color.Transparent;
            DescriptionText.ForeColor = Color.White;
            DescriptionText.Font = new Font("Arial", 8f, FontStyle.Regular);
            DescriptionText.Text = Description;

            Label PublishedText = new Label();
            PublishedText.Parent = NextPanel;
            PublishedText.Location = new Point(333, 1);
            PublishedText.AutoSize = false;
            PublishedText.Size = new Size(154, 13);
            PublishedText.BackColor = Color.Transparent;
            PublishedText.ForeColor = Color.White;
            PublishedText.Font = new Font("Arial", 6f, FontStyle.Bold);
            PublishedText.TextAlign = ContentAlignment.TopRight;
            PublishedText.Text = PublishedAt;

            NextPanel.Show();
            Console.WriteLine("Added Article: " + Title);
        }
    }
}
