
using AForge.Imaging;
using AForge.Imaging.Filters;
using NAudio.Wave;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO.Compression;

using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        bool isPressed = false , isDrawing = false;
        private NextPageForm nextPageForm;

        private string currentShape = "Rectangle";
        Point startPoint, endPoint;
        Point startPoint1, endPoint1;
        Color selectedColor;
        private Button openButton = new Button();
        private Button coloringButton = new Button();
        private Button goToNextPageButton = new Button();
        private Button testButton = new Button();
        private Button SearchButton = new Button();
        private Button button1 = new Button();
        private Button recordButton = new Button();
        private Button compressButton = new Button();
        private Button exportButton = new Button();
        private Button SelectAreass = new Button();
        private List<System.Drawing.Rectangle> selectedAreas;
        private List<DrawnShape> shapes = new List<DrawnShape>();
        private PictureBox pictureBox = new PictureBox();
        private PictureBox pictureBox1 = new PictureBox();
        private TextBox textBox = new TextBox();
        private TextBox txtModifiedDate = new TextBox();
        private RichTextBox noteBox = new RichTextBox();
        private Button addnote = new Button();
        private ListBox lstResults = new ListBox();
        private WaveInEvent waveIn = new WaveInEvent();
        private WaveFileWriter ? waveFileWriter;

        private string tempAudioFile = Path.Combine(Path.GetTempPath(), "tempAudio.wav");

        public MainForm()
        {
            InitializeComponent();
            selectedAreas = new List<System.Drawing.Rectangle>();
            
        }

        private void MainForm_Load(object ? sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // openButton
            // 
            openButton.BackColor = SystemColors.ButtonFace;
            openButton.FlatAppearance.MouseOverBackColor = Color.Red;
            openButton.FlatStyle = FlatStyle.Flat;
            openButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            openButton.ForeColor = Color.Black;
            openButton.Location = new Point(12, 12);
            openButton.Name = "openButton";
            openButton.Size = new Size(60, 25);
            openButton.TabIndex = 1;
            openButton.Text = "File";
            openButton.UseVisualStyleBackColor = false;
            openButton.Click += openButton_Click;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = Color.WhiteSmoke;
            pictureBox.Location = new Point(10, 100);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(400, 550);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            pictureBox.Paint += PictureBox_Paint1;
            // 
            // coloringButton
            // 
            coloringButton.FlatAppearance.MouseOverBackColor = Color.Red;
            coloringButton.FlatStyle = FlatStyle.Flat;
            coloringButton.Location = new Point(78, 12);
            coloringButton.Name = "coloringButton";
            coloringButton.Size = new Size(69, 25);
            coloringButton.TabIndex = 9;
            coloringButton.Text = "Coloring";
            coloringButton.Click += coloringButton_Click;
            // 
            // testButton
            // 
            testButton.BackColor = SystemColors.ButtonFace;
            testButton.FlatAppearance.MouseOverBackColor = Color.Red;
            testButton.FlatStyle = FlatStyle.Flat;
            testButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            testButton.ForeColor = Color.Black;
            testButton.Location = new Point(153, 12);
            testButton.Name = "testButton";
            testButton.Size = new Size(60, 25);
            testButton.TabIndex = 4;
            testButton.Text = "Test";
            testButton.UseVisualStyleBackColor = false;
            testButton.Click += buttonTest_Click;
            // 
            // goToNextPageButton
            // 
            goToNextPageButton.BackColor = SystemColors.ButtonFace;
            goToNextPageButton.FlatAppearance.MouseOverBackColor = Color.Red;
            goToNextPageButton.FlatStyle = FlatStyle.Flat;
            goToNextPageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            goToNextPageButton.ForeColor = Color.Black;
            goToNextPageButton.Location = new Point(1210, 12);
            goToNextPageButton.Name = "goToNextPageButton";
            goToNextPageButton.Size = new Size(60, 26);
            goToNextPageButton.TabIndex = 6;
            goToNextPageButton.Text = "Next";
            goToNextPageButton.UseVisualStyleBackColor = false;
            goToNextPageButton.Click += goToNextPageButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.WhiteSmoke;
            pictureBox1.Location = new Point(420, 100);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 550);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // textBox
            // 
            textBox.BackColor = Color.Bisque;
            textBox.Cursor = Cursors.IBeam;
            textBox.ForeColor = SystemColors.InfoText;
            textBox.Location = new Point(912, 502);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Size = new Size(336, 165);
            textBox.TabIndex = 7;
            textBox.Text = "Test Result";
            textBox.TextAlign = HorizontalAlignment.Center;
            // 
            // txtModifiedDate
            // 
            txtModifiedDate.BackColor = SystemColors.InactiveCaption;
            txtModifiedDate.Cursor = Cursors.IBeam;
            txtModifiedDate.Font = new System.Drawing.Font("Dubai", 9.749999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtModifiedDate.Location = new Point(750, 8);
            txtModifiedDate.Name = "txtModifiedDate";
            txtModifiedDate.Size = new Size(200, 29);
            txtModifiedDate.TabIndex = 1;
            // 
            // SearchButton
            // 
            SearchButton.Location = new Point(967, 11);
            SearchButton.Name = "SearchButton";
            SearchButton.Size = new Size(75, 27);
            SearchButton.TabIndex = 2;
            SearchButton.Text = "Search";
            SearchButton.UseVisualStyleBackColor = true;
            SearchButton.Click += SearchButton_Click;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.Location = new Point(750, 43);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(200, 52);
            lstResults.TabIndex = 3;
            lstResults.SelectedIndexChanged += lstResults_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.FlatAppearance.MouseOverBackColor = Color.Red;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(219, 12);
            button1.Name = "button1";
            button1.Size = new Size(60, 25);
            button1.TabIndex = 8;
            button1.Text = "Share";
            button1.UseVisualStyleBackColor = false;
            button1.Click += shareButton_Click;
            // 
            // recordButton
            // 
            recordButton.FlatAppearance.MouseOverBackColor = Color.Red;
            recordButton.FlatStyle = FlatStyle.Flat;
            recordButton.Location = new Point(285, 12);
            recordButton.Name = "recordButton";
            recordButton.Size = new Size(69, 25);
            recordButton.TabIndex = 0;
            recordButton.Text = "Record";
            recordButton.Click += recordButton_Click;
            // 
            // noteBox
            // 
            noteBox.BackColor = SystemColors.ButtonHighlight;
            noteBox.BorderStyle = BorderStyle.FixedSingle;
            noteBox.Cursor = Cursors.IBeam;
            noteBox.Font = new System.Drawing.Font("Dubai", 9.7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            noteBox.ForeColor = SystemColors.InfoText;
            noteBox.Location = new Point(1022, 395);
            noteBox.Name = "noteBox";
            noteBox.Size = new Size(246, 91);
            noteBox.TabIndex = 2;
            noteBox.Text = "Patient Name :\nDate :\nResult :\n";
            // 
            // addnote
            // 
            addnote.BackColor = SystemColors.ButtonFace;
            addnote.FlatAppearance.MouseOverBackColor = Color.Red;
            addnote.FlatStyle = FlatStyle.Flat;
            addnote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            addnote.ForeColor = Color.Black;
            addnote.Location = new Point(912, 394);
            addnote.Name = "addnote";
            addnote.Size = new Size(96, 26);
            addnote.TabIndex = 10;
            addnote.Text = "Add Note";
            addnote.UseVisualStyleBackColor = false;
            addnote.Click += add_noteButton_Click;
            // 
            // exportButton
            // 
            exportButton.FlatAppearance.MouseOverBackColor = Color.Red;
            exportButton.FlatStyle = FlatStyle.Flat;
            exportButton.Location = new Point(912, 426);
            exportButton.Name = "exportButton";
            exportButton.Size = new Size(96, 27);
            exportButton.TabIndex = 11;
            exportButton.Text = "PDF";
            exportButton.Click += ExportButton_Click;
            // 
 
            // compressButton
            // 
            compressButton.FlatAppearance.MouseOverBackColor = Color.Red;
            compressButton.FlatStyle = FlatStyle.Flat;
            compressButton.Location = new Point(912, 459);
            compressButton.Name = "compressButton";
            compressButton.Size = new Size(96, 27);
            compressButton.TabIndex = 12;
            compressButton.Text = "Compress";
            compressButton.Click += compressButton_Click;
            // 
            // button2
            // 
            SelectAreass.BackColor = SystemColors.ButtonFace;
            SelectAreass.FlatAppearance.MouseOverBackColor = Color.Red;
            SelectAreass.FlatStyle = FlatStyle.Flat;
            SelectAreass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            SelectAreass.ForeColor = Color.Black;
            SelectAreass.Location = new Point(360, 12);
            SelectAreass.Name = "button2";
            SelectAreass.Size = new Size(93, 25);
            SelectAreass.TabIndex = 13;
            SelectAreass.Text = "SelectAreass";
            SelectAreass.UseVisualStyleBackColor = false;
            SelectAreass.Click += selectAreassButton_Click;
            // 
            // MainForm
            // 
            BackColor = SystemColors.ButtonFace;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1280, 749);
            Controls.Add(SelectAreass);
            Controls.Add(compressButton);
            Controls.Add(exportButton);
            Controls.Add(addnote);
            Controls.Add(noteBox);
            Controls.Add(recordButton);
            Controls.Add(button1);
            Controls.Add(goToNextPageButton);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox);
            Controls.Add(textBox);
            Controls.Add(openButton);
            Controls.Add(coloringButton);
            Controls.Add(testButton);
            Controls.Add(lstResults);
            Controls.Add(SearchButton);
            Controls.Add(txtModifiedDate);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            ForeColor = SystemColors.InfoText;
            Name = "MainForm";
            Text = "Image Processing App";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeUI()
        {
        }
        private void openButton_Click(object ? sender, EventArgs e)
        {
            ContextMenuStrip fileMenu = new ContextMenuStrip();

            ToolStripMenuItem openfileItem = new ToolStripMenuItem("open");
            openfileItem.Click += (s, ev) => openfileButton_Click();
            fileMenu.Items.Add(openfileItem);

            ToolStripMenuItem savefileItem = new ToolStripMenuItem("save");
            savefileItem.Click += (s, ev) => buttonSave_Click();
            fileMenu.Items.Add(savefileItem);

            ToolStripMenuItem selectareaItem = new ToolStripMenuItem("Select_Area");
            selectareaItem.Click += (s, ev) => selectAreaButton_Click();
            fileMenu.Items.Add(selectareaItem);

            ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut");
            cutItem.Click += (s, ev) => cutButton_Click();
            fileMenu.Items.Add(cutItem);

            ToolStripMenuItem resetItem = new ToolStripMenuItem("Reset");
            resetItem.Click += (s, ev) => resetButton_Click();
            fileMenu.Items.Add(resetItem);

            fileMenu.Show(Cursor.Position);

        }
        private void openfileButton_Click()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;*.tiff|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
               
                pictureBox.Image = null;
                pictureBox.Invalidate();

                string imagePath = openFileDialog.FileName;
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
                pictureBox.Image = ResizeImage(image, pictureBox.Width, pictureBox.Height);
            }
        }

        private System.Drawing.Image ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private void resetButton_Click()
        {
            Form nextForm = new MainForm();
            nextForm.Show();
            this.Hide();
 

        }
        private void pictureBox_Paint(object ? sender, PaintEventArgs e)
        {
            foreach (System.Drawing.Rectangle selectedArea in selectedAreas)
            {
                using (var pen = new Pen(Color.Red, 4))
                {
                    e.Graphics.DrawRectangle(pen, selectedArea);
                }
            }
        }

        private void selectAreaButton_Click()
        {

            if (pictureBox.Image != null)
            {
                isPressed = true;
                isDrawing = false;

                pictureBox.Cursor = Cursors.Cross;

            }
        }

        private void cutButton_Click()
        {
            if (pictureBox.Image != null && selectedAreas.Count != 0)
            {
                pictureBox.Cursor = Cursors.Cross;

                CropImage(selectedAreas[selectedAreas.Count - 1]);


            }

        }

        private void CropImage(System.Drawing.Rectangle area)
        {
            Bitmap bmp = new Bitmap(area.Width, area.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(pictureBox.Image, new System.Drawing.Rectangle(0, 0, area.Width, area.Height), area, GraphicsUnit.Pixel);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(saveFileDialog.FileName);
            }
        }

        private void coloringButton_Click(object ? sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                
                ContextMenuStrip colorMenu = new ContextMenuStrip();
                // Add items for each colormap
                ToolStripMenuItem colorItem = new ToolStripMenuItem("Color_Area");
                colorItem.Click += (s, ev) => colorButton_Click();
                colorMenu.Items.Add(colorItem);

                ToolStripMenuItem colormapItem = new ToolStripMenuItem("Colormap");
                colormapItem.Click += (s, ev) => colormapButton_Click();
                colorMenu.Items.Add(colormapItem);

                ToolStripMenuItem fourierItem = new ToolStripMenuItem("Fourier_high");
                fourierItem.Click += (s, ev) => FourierTransform();
                colorMenu.Items.Add(fourierItem);

                ToolStripMenuItem fourierlowItem = new ToolStripMenuItem("Fourier_low");
                fourierlowItem.Click += (s, ev) => FourierTransformLowPassFilter();
                colorMenu.Items.Add(fourierlowItem);

                colorMenu.Show(Cursor.Position);
            }
        }

        private void colorButton_Click()
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color;
                ApplyGradientColorToSelectedArea(selectedColor);
            }
        }

        private void ApplyGradientColorToSelectedArea(Color selectedColor)
        {
            if (pictureBox.Image != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
                foreach (var area in selectedAreas)
                {
                    for (int x = area.Left; x < area.Right; x++)
                    {
                        for (int y = area.Top; y < area.Bottom; y++)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);
                            int brightness = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3);
                            int grayValue = 255 - brightness;
                            int newRed = selectedColor.R * grayValue / 255;
                            int newGreen = selectedColor.G * grayValue / 255;
                            int newBlue = selectedColor.B * grayValue / 255;

                            Color newColor = Color.FromArgb(pixelColor.A, newRed, newGreen, newBlue);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
                pictureBox.Image = bitmap;
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object ? sender, MouseEventArgs e)
        {
            if (isPressed == true)
            {
                endPoint = e.Location;
                isPressed = false;
                if (pictureBox.Cursor == Cursors.Cross)
                {
                    pictureBox.Cursor = Cursors.Default;
                }

            }
            if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;
                endPoint1 = e.Location;
                shapes.Add(new DrawnShape(currentShape, startPoint1, endPoint1)); 
               

               
                if (pictureBox.Image != null)
                {
                    using (Graphics g = Graphics.FromImage(pictureBox.Image))
                    {
                        Pen pen = new Pen(Color.Blue, 2);
                        DrawShape(g, pen, currentShape, startPoint1, endPoint1);
                    }
                    pictureBox.Invalidate();
                }
            }
        }

        private void pictureBox_MouseDown(object ? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                if (pictureBox.Cursor == Cursors.Cross)
                {
                    selectedAreas.Add(new System.Drawing.Rectangle(startPoint, new Size()));
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                isDrawing = true;
                startPoint1 = e.Location;
            }
        }

        private void pictureBox_MouseMove(object ? sender, MouseEventArgs e)
        {
            if (isPressed == true)
            {
                endPoint = e.Location;
                Refresh();
                if (pictureBox.Cursor == Cursors.Cross && e.Button == MouseButtons.Left)
                {
                    int lastIndex = selectedAreas.Count - 1;
                    System.Drawing.Rectangle lastArea = selectedAreas[lastIndex];
                    selectedAreas[lastIndex] = new System.Drawing.Rectangle(
                        lastArea.Left,
                        lastArea.Top,
                        endPoint.X - lastArea.Left,
                        endPoint.Y - lastArea.Top);

                    pictureBox.Invalidate();
                }
            }
            if (isDrawing)
            {
                endPoint1 = e.Location;
                pictureBox.Invalidate();
            }
        }

        //---------saving images---------------
        private void buttonSave_Click()
        {
            if (pictureBox.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|BMP Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bool saveBothImages = MessageBox.Show("Do you want to save both images?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;


                    Bitmap image = new Bitmap(pictureBox.Image);
                    string filePath = saveFileDialog.FileName;
                    image.Save(filePath);

                    if (saveBothImages)
                    {
                        if (pictureBox1.Image != null)
                        {

                            string filePath1 = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + "_1" + Path.GetExtension(filePath));

                         
                            Bitmap image1 = new Bitmap(pictureBox1.Image);
                            image1.Save(filePath1);
                        }
                        else
                        {
                            MessageBox.Show("the second image is empty");

                        }
                    }
                }
            }
        }

        //-----------colormap---------------------
        private void colormapButton_Click()
        {
            if (pictureBox.Image != null)
            {
             
                ContextMenuStrip colormapMenu = new ContextMenuStrip();
               
                ToolStripMenuItem jetItem = new ToolStripMenuItem("Jet");
                jetItem.Click += (s, ev) => ApplyJetColormapToSelectedArea();
                colormapMenu.Items.Add(jetItem);

                ToolStripMenuItem rainbowItem = new ToolStripMenuItem("Rainbow");
                rainbowItem.Click += (s, ev) => ApplyRainbowColormapToSelectedArea();
                colormapMenu.Items.Add(rainbowItem);

                ToolStripMenuItem infernoItem = new ToolStripMenuItem("Inferno");
                infernoItem.Click += (s, ev) => ApplyInfernoColormapToSelectedArea();
                colormapMenu.Items.Add(infernoItem);

                colormapMenu.Show(Cursor.Position);
            }
        }

        private void ApplyRainbowColormapToSelectedArea()
        {
            Bitmap bitmap = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color pixelColor = bitmap.GetPixel(j, i);
                    
                    int brightness = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3.0);
                    double normalizedBrightness = brightness / 255.0;
                    Color rainbowColor = GetRainbowColor(normalizedBrightness);
                    bitmap.SetPixel(j, i, rainbowColor);
                }
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Invalidate();
        }
        private Color GetRainbowColor(double position)
        {
          
            if (position < 0.2) // Red to Orange
            {
                return Color.FromArgb(255, (int)(255 * position / 0.2), 0, 0);
            }
            else if (position < 0.4) // Orange to Yellow
            {
                return Color.FromArgb(255, 255, (int)(255 * (position - 0.2) / 0.2), 0);
            }
            else if (position < 0.6) // Yellow to Green
            {
                return Color.FromArgb(255, 255 - (int)(255 * (position - 0.4) / 0.2), 255, 0);
            }
            else if (position < 0.8) // Green to Blue
            {
                return Color.FromArgb(255, 0, 255, (int)(255 * (position - 0.6) / 0.2));
            }
            else // Blue to Violet
            {
                return Color.FromArgb(255, (int)(255 * (position - 0.8) / 0.2), 0, 255);
            }
        }

        private void ApplyInfernoColormapToSelectedArea()
        {
            Bitmap bitmap = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                
                    Color pixelColor = bitmap.GetPixel(j, i);
                  
                    int brightness = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3.0);
                  
                    double normalizedBrightness = brightness / 255.0;
                 
                    Color infernoColor = GetInfernoColor(normalizedBrightness);
                
                    bitmap.SetPixel(j, i, infernoColor);
                }
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Invalidate();
        }
        private Color GetInfernoColor(double position)
        {
            // This method maps a position (between 0 and 1) to a color in the Inferno spectrum
            // You can adjust the logic here to change how the colors are mapped
            if (position < 0.25) // Black to Red
            {
                return Color.FromArgb(255, (int)(255 * position / 0.25), 0, 0);
            }
            else if (position < 0.5) // Red to Orange
            {
                return Color.FromArgb(255, 255, (int)(255 * (position - 0.25) / 0.25), 0);
            }
            else if (position < 0.75) // Orange to Yellow
            {
                return Color.FromArgb(255, 255, 255, (int)(255 * (position - 0.5) / 0.25));
            }
            else // Yellow to White
            {
                return Color.FromArgb(255, 255, 255, (int)(255 * (position - 0.75) / 0.25));
            }
        }

        private void ApplyJetColormapToSelectedArea()
        {
            Bitmap bitmap = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    // Get RGB color of current pixel
                    Color pixelColor = bitmap.GetPixel(j, i);
                    // Calculate brightness
                    int brightness = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3.0);
                    // Normalize brightness to a value between 0 and 1
                    double normalizedBrightness = brightness / 255.0;
                    // Map normalized brightness to a position in the Jet spectrum
                    Color jetColor = GetJetColor(normalizedBrightness);
                    // Set the corresponding pixel in the Jet image
                    bitmap.SetPixel(j, i, jetColor);
                }
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Invalidate();
        }
        private Color GetJetColor(double position)
        {
            // This method maps a position (between 0 and 1) to a color in the Jet spectrum
            // You can adjust the logic here to change how the colors are mapped
            if (position < 0.25) // Blue to Green
            {
                return Color.FromArgb(0, (int)(255 * position / 0.25), (int)(255 * (position / 0.25)));
            }
            else if (position < 0.5) // Green to Yellow
            {
                return Color.FromArgb(0, 255, (int)(255 * (position - 0.25) / 0.25));
            }
            else if (position < 0.75) // Yellow to Red
            {
                return Color.FromArgb((int)(255 * (position - 0.5) / 0.25), 255, 0);
            }
            else // Red to Dark Red
            {
                return Color.FromArgb(255, (int)(255 * (position - 0.75) / 0.25), 0);
            }
        }

        //----fourier-------------------------------
        private void FourierTransform()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox.Image);
                Bitmap resizedImage = ResizeToPowerOf2(bitmap);

                Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
                Bitmap grayImage = filter.Apply(resizedImage);

                ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
                complexImage.ForwardFourierTransform();

                ApplyHighPassFilter(complexImage, 1);

                Bitmap magnitudeImage = complexImage.ToBitmap();

                complexImage.BackwardFourierTransform();
                Bitmap processedImage = complexImage.ToBitmap();
                pictureBox1.Image = processedImage;
                pictureBox1.Invalidate();
            }
        }

        private Bitmap ResizeToPowerOf2(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            int newWidth = NearestPowerOf2(width);
            int newHeight = NearestPowerOf2(height);

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(bitmap, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }

        private int NearestPowerOf2(int value)
        {
            int power = 1;
            while (power < value)
            {
                power *= 2;
            }
            return power;
        }

        private void ApplyHighPassFilter(ComplexImage complexImage, int radius)
        {
            int width = complexImage.Width;
            int height = complexImage.Height;
            int centerX = width / 2;
            int centerY = height / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                    if (distance < radius)
                    {
                        complexImage.Data[y, x] = new AForge.Math.Complex(0, 0);
                    }
                }
            }
        }

        private void FourierTransformLowPassFilter()
        {
            if (pictureBox.Image != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox.Image);
                Bitmap resizedImage = ResizeToPowerOf2(bitmap);

                Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
                Bitmap grayImage = filter.Apply(resizedImage);

                ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
                complexImage.ForwardFourierTransform();

    
                ApplyLowPassFilter(complexImage, 50); 

                Bitmap magnitudeImage = complexImage.ToBitmap();

                complexImage.BackwardFourierTransform();
                Bitmap processedImage = complexImage.ToBitmap();
                pictureBox1.Image = processedImage;
                pictureBox1.Invalidate();
            }
        }

        private void ApplyLowPassFilter(ComplexImage complexImage, int radius)
        {
            int width = complexImage.Width;
            int height = complexImage.Height;
            int centerX = width / 2;
            int centerY = height / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
                    if (distance > radius)
                    {
                        complexImage.Data[y, x] = new AForge.Math.Complex(0, 0);
                    }
                }
            }
        }

        //------------test the image---------------------
        private void buttonTest_Click(object ? sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                string severity = DetermineImageSeverity();
                UpdateResultTextBox(severity);
            }

        }
        public string DetermineImageSeverity()
        {
            Bitmap image = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);

            int totalWhiteOrNearWhitePixels = 0;

            for (int y = 100; y < image.Height - 100; y++)
            {
                for (int x = 50; x < image.Width - 50; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    if ((pixelColor.R >= 171 && pixelColor.R <= 255) &&
                        (pixelColor.G >= 172 && pixelColor.G <= 255) &&
                        (pixelColor.B >= 176 && pixelColor.B <= 255))
                    {
                        totalWhiteOrNearWhitePixels++;

                    }
                }
            }

            if (totalWhiteOrNearWhitePixels > 30000 && totalWhiteOrNearWhitePixels < 100000000)
            {
                return "The result is : Severe";
            }
            else if (totalWhiteOrNearWhitePixels > 20000 && totalWhiteOrNearWhitePixels < 30000)
            {
                return "The result is : Moderate";
            }
            else if (totalWhiteOrNearWhitePixels > 0 && totalWhiteOrNearWhitePixels < 20000)
            {
                return "The result is : Mild";
            }
            else
            {
                return "No Result";
            }
        }
        public void UpdateResultTextBox(string resultText)
        {
            textBox.Text = resultText;
            textBox.SelectionStart = resultText.Length;
            textBox.SelectionLength = 0;
        }

        //-------next_page------------
        private void goToNextPageButton_Click(object sender, EventArgs e)
        {
            if (nextPageForm == null)
            {
                nextPageForm = new NextPageForm(this); 
            }
            nextPageForm.Show();
            this.Hide();
        }

        //------search(date/size)-----------------
        private void SearchButton_Click(object ? sender, EventArgs e)
        {
            ContextMenuStrip searchMenu = new ContextMenuStrip();

            ToolStripMenuItem openfileItem = new ToolStripMenuItem("Date");
            openfileItem.Click += (s, ev) => dateSearch_Click();
            searchMenu.Items.Add(openfileItem);

            ToolStripMenuItem savefileItem = new ToolStripMenuItem("Size");
            savefileItem.Click += (s, ev) => sizeSearch_Click();
            searchMenu.Items.Add(savefileItem);


            searchMenu.Show(Cursor.Position);

        }

        private void dateSearch_Click()
        {
            DateTime modifiedDate;
            if (!DateTime.TryParse(txtModifiedDate.Text, out modifiedDate))
            {
                MessageBox.Show("Please enter a valid date in the format yyyy-MM-dd.");
                return;
            }
            //this path for a local pc "C:\\Users\\HP\\Downloads\\x-ray image"
            string directoryPath = "C:\\Users\\HP\\Downloads\\x-ray image";
            if (!Directory.Exists(directoryPath))
            {
                MessageBox.Show("The specified directory does not exist.");
                return;
            }

            var results = SearchImages(directoryPath, modifiedDate);
            lstResults.Items.Clear();
            foreach (var result in results)
            {
                lstResults.Items.Add(result);
            }
        }

        private List<string> SearchImages(string directoryPath, DateTime modifiedDate)
        {
            var results = new List<string>();
            var files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                                 .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".bmp") || file.ToLower().EndsWith(".gif"));

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.LastWriteTime.Date == modifiedDate.Date)
                {
                    results.Add(file);
                }
            }
            return results;
        }

        private List<string> SearchImagesBySize(string directoryPath, long minSize, long maxSize)
        {
            var results = new List<string>();
            var files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                               .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".bmp") || file.ToLower().EndsWith(".gif"));

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if ((minSize <= fileInfo.Length) && (maxSize >= fileInfo.Length))
                {
                    results.Add(file);
                }
            }
            return results;
        }

        private void sizeSearch_Click()
        {
            string directoryPath = "C:\\Users\\HP\\Downloads\\x-ray image";
            if (!Directory.Exists(directoryPath))
            {
                MessageBox.Show("The specified directory does not exist.");
                return;
            }

            string userInput = txtModifiedDate.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Please enter a value.");
                return;
            }


            long targetSize;
            if (!long.TryParse(userInput, out targetSize))
            {
                MessageBox.Show("Invalid size entered. Please enter a numeric value.");
                return;
            }
            long minSize = 0;
            long maxSize = targetSize * 1000;
            var results = SearchImagesBySize(directoryPath, minSize, maxSize);
            lstResults.Items.Clear();
            foreach (var result in results)
            {
                lstResults.Items.Add(result);
            }
        }

        private void lstResults_SelectedIndexChanged(object ? sender, EventArgs e)
        {
            // Check if an item is selected
            if (lstResults.SelectedItem != null)
            {
                // Get the selected file path
                string selectedFilePath = lstResults.SelectedItem.ToString();


                // Load the image from the selected file path
                System.Drawing.Image image = System.Drawing.Image.FromFile(selectedFilePath);

                // Set the image to pictureBox
                pictureBox.Image = image;
            }
        }
        //--------add_note----------------
        private void add_noteButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                string noteText = noteBox.Text;

            
                float fontSize = 10; 
                System.Drawing.Font drawFont = new System.Drawing.Font("Dubai", fontSize, FontStyle.Regular);

              
                int xPosition = 10; 
                int yPosition = pictureBox.Height - 70; 

                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    g.DrawString(noteText, drawFont, Brushes.Black, new PointF(xPosition, yPosition));
                }

                pictureBox.Invalidate(); 
            }
        }
        //-------------
        private void recordButton_Click(object ? sender , EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                // Create a context menu strip
                ContextMenuStrip recordMenu = new ContextMenuStrip();

                // Add items for each colormap
                ToolStripMenuItem startrecordItem = new ToolStripMenuItem("Start Recording");
                startrecordItem.Click += (s, ev) => StartRecording();
                recordMenu.Items.Add(startrecordItem);

                ToolStripMenuItem saverecordItem = new ToolStripMenuItem("Save Record");
                saverecordItem.Click += (s, ev) => SaveRecording();
                recordMenu.Items.Add(saverecordItem);

                recordMenu.Show(Cursor.Position);
            }
        }

        public void StartRecording()
        {
            try
            {
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(44100, 1); 

                waveIn.DataAvailable += OnDataAvailable;
                waveIn.RecordingStopped += OnRecordingStopped;

                waveFileWriter = new WaveFileWriter(tempAudioFile, waveIn.WaveFormat);

                waveIn.StartRecording();
                MessageBox.Show("Recording started. Press OK to stop.");

               
                StopRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting recording: " + ex.Message);
            }
        }

        private void OnDataAvailable(object ? sender, WaveInEventArgs e)
        {
            if (waveFileWriter != null)
            {
                waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveFileWriter.Flush();
            }
        }

        private void OnRecordingStopped(object ? sender, StoppedEventArgs e)
        {
            waveFileWriter?.Dispose();
            MessageBox.Show("Recording stopped. Audio saved temporarily.");
        }

        public void StopRecording()
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
            }
        }

        public void SaveRecording()
        {
            if (File.Exists(tempAudioFile))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "WAV files (*.wav)|*.wav";
                    saveFileDialog.Title = "Save Audio File";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string outputAudioFile = saveFileDialog.FileName;
                        File.Copy(tempAudioFile, outputAudioFile, true);
                        MessageBox.Show("Recording saved to: " + outputAudioFile);
                    }
                }
            }
            else
            {
                MessageBox.Show("No recording found. Please record audio first.");
            }
        }

        //------------------pdf----------------------

        private void ExportButton_Click(object ? sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save PDF File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    ExportToPdf(filePath, textBox.Text, noteBox.Text);
                    MessageBox.Show("PDF file saved successfully!");
                }
            }
        }

        private void ExportToPdf(string filePath, string textBoxContent, string noteBoxContent)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            document.Open();

            //noteBox
            document.Add(new Paragraph(noteBoxContent));
            document.Add(new Paragraph("\n"));

            //  textBox
            document.Add(new Paragraph(textBoxContent));
            document.Add(new Paragraph("\n"));
            if (pictureBox.Image != null)
            {
                var pdfImage1 = iTextSharp.text.Image.GetInstance(pictureBox.Image, System.Drawing.Imaging.ImageFormat.Png);
                document.Add(pdfImage1);
                document.Add(new Paragraph("\n"));
            }

            if (pictureBox1.Image != null)
            {
                var pdfImage2 = iTextSharp.text.Image.GetInstance(pictureBox1.Image, System.Drawing.Imaging.ImageFormat.Png);
                document.Add(pdfImage2);
            }

            document.Close();
        }
        //------------------share-----------------------
        private async void shareButton_Click(object ? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.Title = "Select a File to Share";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    ContextMenuStrip shareMenu = new ContextMenuStrip();

                    ToolStripMenuItem shareViaWhatsApp = new ToolStripMenuItem("Share via WhatsApp");
                    shareViaWhatsApp.Click += async (s, ev) => await ShareViaWhatsApp(filePath);
                    shareMenu.Items.Add(shareViaWhatsApp);

                    ToolStripMenuItem shareViaTelegram = new ToolStripMenuItem("Share via Telegram");
                    shareViaTelegram.Click += async (s, ev) => await ShareViaTelegram(filePath);
                    shareMenu.Items.Add(shareViaTelegram);

                    shareMenu.Show(Cursor.Position);
                }
            }
        }

        private async Task ShareViaWhatsApp(string filePath)
        {
            try
            {
                string fileUrl = await UploadFile(filePath);
                string whatsappUrl = $"https://api.whatsapp.com/send?text={Uri.EscapeDataString(fileUrl)}";
                Process.Start(new ProcessStartInfo("cmd", $"/c start {whatsappUrl}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sharing via WhatsApp: {ex.Message}");
            }
        }

        private async Task ShareViaTelegram(string filePath)
        {
            try
            {
                string fileUrl = await UploadFile(filePath);
                string telegramUrl = $"https://t.me/share/url?url={Uri.EscapeDataString(fileUrl)}";
                Process.Start(new ProcessStartInfo("cmd", $"/c start {telegramUrl}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sharing via Telegram: {ex.Message}");
            }
        }

        private async Task<string> UploadFile(string filePath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (MultipartFormDataContent content = new MultipartFormDataContent())
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    content.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), "file", Path.GetFileName(filePath));

                    HttpResponseMessage response = await client.PostAsync("https://file.io", content);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    return jsonResponse.link;
                }
            }
        }

        //------------------------compress----------------------------

        private void compressButton_Click(object ? sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Compressed Files (*.zip)|*.zip";
                saveFileDialog.Title = "Save Compressed File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string compressedFilePath = saveFileDialog.FileName;
                    string tempImageFilePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                    string tempTextFilePath = Path.Combine(Path.GetTempPath(), "tempReport.txt");
                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Save(tempImageFilePath);
                    }

                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        File.WriteAllText(tempTextFilePath, textBox.Text);
                    }

                    if (!File.Exists(tempAudioFile))
                    {
                        MessageBox.Show("Audio file not found. Please record the audio first.");
                        return;
                    }

                    using (FileStream zipToOpen = new FileStream(compressedFilePath, FileMode.Create))
                    {
                        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                        {
                            if (File.Exists(tempImageFilePath))
                            {
                                archive.CreateEntryFromFile(tempImageFilePath, "image.jpg");
                            }

                            if (File.Exists(tempTextFilePath))
                            {
                                archive.CreateEntryFromFile(tempTextFilePath, "report.txt");
                            }

                            if (File.Exists(tempAudioFile))
                            {
                                archive.CreateEntryFromFile(tempAudioFile, "audio.wav");
                            }
                        }
                    }

                    MessageBox.Show("Compress succeeded");
                }
            }
        }
       // ------------select areas*--------------------
        private void selectAreassButton_Click(object sender, EventArgs e)
        {
            isDrawing = true;
            if (pictureBox.Image != null)
            {
                pictureBox.Cursor = Cursors.Cross;

                ContextMenuStrip fileMenu = new ContextMenuStrip();

                ToolStripMenuItem drawRectangle = new ToolStripMenuItem("Draw Rectangle");
                drawRectangle.Click += (s, ev) => currentShape = "Rectangle";
                fileMenu.Items.Add(drawRectangle);

                ToolStripMenuItem drawCircle = new ToolStripMenuItem("Draw Circle");
                drawCircle.Click += (s, ev) => currentShape = "Circle";
                fileMenu.Items.Add(drawCircle);

                ToolStripMenuItem drawLine = new ToolStripMenuItem("Draw Line");
                drawLine.Click += (s, ev) => currentShape = "Line";
                fileMenu.Items.Add(drawLine);

                ToolStripMenuItem drawCurve = new ToolStripMenuItem("Draw Curve");
                drawCurve.Click += (s, ev) => currentShape = "Curve";
                fileMenu.Items.Add(drawCurve);

                fileMenu.Show(Cursor.Position);
            }
        }

        private void PictureBox_Paint1(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 2);

            // رسم جميع الأشكال المخزنة
            foreach (var shape in shapes)
            {
                DrawShape(e.Graphics, pen, shape.ShapeType, shape.StartPoint, shape.EndPoint);
            }

            if (isDrawing)
            {
                DrawShape(e.Graphics, pen, currentShape, startPoint1, endPoint1);
            }
        }

        private void DrawShape(Graphics g, Pen pen, string shapeType, Point startPoint, Point endPoint)
        {
            switch (shapeType)
            {
                case "Rectangle":
                    g.DrawRectangle(pen, GetRectangle(startPoint, endPoint));
                    break;
                case "Circle":
                    g.DrawEllipse(pen, GetRectangle(startPoint, endPoint));
                    break;
                case "Line":
                    g.DrawLine(pen, startPoint, endPoint);
                    break;
                case "Curve":
                    g.DrawBezier(pen, startPoint, new Point((startPoint.X + endPoint.X) / 2, startPoint.Y), new Point((startPoint.X + endPoint.X) / 2, endPoint.Y), endPoint);
                    break;
            }
        }

        private System.Drawing.Rectangle GetRectangle(Point p1, Point p2)
        {
            return new System.Drawing.Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }
    }
}