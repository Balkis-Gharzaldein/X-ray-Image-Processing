namespace WindowsFormsApp1
{
    public partial class NextPageForm : Form
    {
        private PictureBox pictureBox2 = new PictureBox();
        private PictureBox pictureBox3 = new PictureBox();
        private Button buttonLoadImages = new Button();
        private Button buttonTestImages = new Button();
        private Button backPageButton = new Button();
        private MainForm mainForm;

        public NextPageForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NextPageForm));
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // buttonLoadImages
            // 
            buttonLoadImages.BackColor = SystemColors.ButtonFace;
            buttonLoadImages.FlatAppearance.MouseOverBackColor = Color.Red;
            buttonLoadImages.FlatStyle = FlatStyle.Flat;
            buttonLoadImages.Font = new Font("Arial Nova Cond", 9.75F);
            buttonLoadImages.ForeColor = Color.Black;
            buttonLoadImages.Location = new Point(12, 12);
            buttonLoadImages.Name = "buttonLoadImages";
            buttonLoadImages.Size = new Size(60, 26);
            buttonLoadImages.TabIndex = 1;
            buttonLoadImages.Text = "Open";
            buttonLoadImages.UseVisualStyleBackColor = false;
            buttonLoadImages.Click += buttonLoadImages_Click;
            // 
            // buttonTestImages
            // 
            buttonTestImages.BackColor = SystemColors.ButtonFace;
            buttonTestImages.FlatAppearance.MouseOverBackColor = Color.Red;
            buttonTestImages.FlatStyle = FlatStyle.Flat;
            buttonTestImages.Font = new Font("Arial Nova Cond", 9.75F);
            buttonTestImages.ForeColor = Color.Black;
            buttonTestImages.Location = new Point(78, 12);
            buttonTestImages.Name = "buttonTestImages";
            buttonTestImages.Size = new Size(60, 26);
            buttonTestImages.TabIndex = 1;
            buttonTestImages.Text = "Comparison";
            buttonTestImages.UseVisualStyleBackColor = false;
            buttonTestImages.Click += buttonTestImages_Click;
            // 
            // backPageButton
            // 
            backPageButton.BackColor = SystemColors.ButtonFace;
            backPageButton.FlatAppearance.MouseOverBackColor = Color.Red;
            backPageButton.FlatStyle = FlatStyle.Flat;
            backPageButton.Font = new Font("Arial Nova Cond", 9.75F);
            backPageButton.ForeColor = Color.Black;
            backPageButton.Location = new Point(1210, 12);
            backPageButton.Name = "backPageButton";
            backPageButton.Size = new Size(60, 26);
            backPageButton.TabIndex = 6;
            backPageButton.Text = "Back";
            backPageButton.UseVisualStyleBackColor = false;
            backPageButton.Click += BackPageButton_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.WhiteSmoke;
            pictureBox2.Location = new Point(26, 95);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(470, 553);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.WhiteSmoke;
            pictureBox3.Location = new Point(516, 95);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(470, 553);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // NextPageForm
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");

            ClientSize = new Size(1280, 749);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
            Controls.Add(buttonLoadImages);
            Controls.Add(buttonTestImages);
            Controls.Add(backPageButton);
            Name = "NextPageForm";
            WindowState = FormWindowState.Maximized;
            Load += NextPageForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        private void buttonLoadImages_Click(object ? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                Title = "Select First Image"
            };
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                Title = "Select Second Image"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox3.Image = Image.FromFile(openFileDialog2.FileName);

                buttonLoadImages.Text = "Reload Images";
            }
        }

        private void buttonTestImages_Click(object ? sender, EventArgs e)
        {

            AnalyzeImages();


        }

        private void AnalyzeImages()
        {
            Bitmap bmp1 = (Bitmap)pictureBox2.Image;
            Bitmap bmp2 = (Bitmap)pictureBox3.Image;

            int whitePixelsCount1 = CountWhitePixels(bmp1);
            int whitePixelsCount2 = CountWhitePixels(bmp2);

            if (whitePixelsCount1 > whitePixelsCount2)
            {
                MessageBox.Show("There is an improvement in the patient's condition.");
            }
            else if (whitePixelsCount1 < whitePixelsCount2)
            {
                MessageBox.Show("There is a regression in the patient's condition.");
            }
            else
            {
                MessageBox.Show("No significant developments.");
            }
        }

        private int CountWhitePixels(Bitmap bmp)
        {
            int whitePixelsCount = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    if (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255)
                    {
                        whitePixelsCount++;
                    }
                }
            }
            return whitePixelsCount;
        }

        private void BackPageButton_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Hide();
        }

        private void NextPageForm_Load(object ? sender, EventArgs e)
        {

        }
    }
}