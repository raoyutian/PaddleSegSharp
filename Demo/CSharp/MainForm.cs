
using PaddleSegSharp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace PaddleSegSharpDemo
{
    /// <summary>
    /// PaddleSegSharp使用示例
    /// </summary>
    public partial class MainForm : Form
    {
        private string[] bmpFilters = new string[] { ".bmp", ".jpg", ".jpeg", ".tiff", ".tif", ".png" };
        private string fileFilter = "*.*|*.bmp;*.jpg;*.jpeg;*.tiff;*.tiff;*.png";
        private string title = "PaddleSeg C# Demo 绿色版 by 饶玉田 QQ群：318860399，商业合作QQ:277784829";
        private PaddleSegMattingEngine engine;
        private Bitmap bmp;
        private  Color bgcolor=Color.White;
        public MainForm()
        {
            InitializeComponent();
            this.Text = title;
            pictureBox1.AllowDrop = true;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            string modelPath = null;
            //参数
            MattingParameter parameter = new MattingParameter();
            //初始化 引擎
            engine = new PaddleSegMattingEngine();
            engine.Init(modelPath, parameter);
        }
        private Bitmap GetClipboardImage()
        {
            bmp = (Bitmap)Clipboard.GetImage();
            if (bmp == null)
            {
                var files = Clipboard.GetFileDropList();

                string[] Filtersarr = new string[files.Count];
                files.CopyTo(Filtersarr, 0);
                Filtersarr = Filtersarr.Where(x => bmpFilters.Contains(Path.GetExtension(x).ToLower())).ToArray();
                if (Filtersarr.Length > 0)
                {
                    var imagebyte = File.ReadAllBytes(Filtersarr[0]);
                    bmp = new Bitmap(new MemoryStream(imagebyte));
                }
            }
            return bmp;
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            if (data == null) return;
            string[] files = data.GetData(DataFormats.FileDrop, autoConvert: true) as string[];

            string[] Filtersarr = new string[files.Count()];
            files.CopyTo(Filtersarr, 0);
            Filtersarr = Filtersarr.Where(x => bmpFilters.Contains(Path.GetExtension(x).ToLower())).ToArray();
            if (Filtersarr.Length > 0)
            {
                var imagebyte = File.ReadAllBytes(Filtersarr[0]);
                bmp = new Bitmap(new MemoryStream(imagebyte));
                pictureBox1.BackgroundImage = bmp;
                if (bmp == null) return;
                string outfile = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + ".bmp";
                engine.Seg(imagebyte, outfile);
                ShowOCRResult(outfile);
            }
        }
        /// <summary>
        /// 拖放文件识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            
                e.Effect = DragDropEffects.Move;
           

        }
        private void pictureBox1_DragOver(object sender, DragEventArgs e)
        {
             
                e.Effect = DragDropEffects.Move;
            
        }
        /// <summary>
        /// 打开本地图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripopenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = fileFilter;
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var imagebyte = File.ReadAllBytes(ofd.FileName);
            bmp = new Bitmap(new MemoryStream(imagebyte));
            pictureBox1.BackgroundImage = bmp;
            if (bmp == null) return;
            string outfile=Path.GetTempPath()+"\\"+ Guid.NewGuid().ToString()+".bmp";
            engine.Seg(imagebyte, outfile);
            ShowOCRResult(outfile);

        }

        /// <summary>
        /// 识别截图文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            this.Hide();
            System.Threading.Thread.Sleep(200);
            Application.DoEvents();
          
            ScreenCapturer.ScreenCapturerTool screenCapturer = new ScreenCapturer.ScreenCapturerTool();
            if (screenCapturer.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmp = (Bitmap)screenCapturer.Image;
                pictureBox1.BackgroundImage = bmp;
                try
                {
                    string outfile = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + ".bmp";
                    engine.Seg(bmp, outfile);
                    ShowOCRResult(outfile);
                }
                catch (Exception ex)
                {
                }
            }
            this.Show();
        }
        /// <summary>
        /// 剪切板识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void toolStripsnapocr_Click(object sender, EventArgs e)
        {
            bmp = GetClipboardImage();
           
            pictureBox1.BackgroundImage = bmp;
            
                try
                {

                string outfile = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + ".bmp";
                engine.Seg(bmp, outfile);
                ShowOCRResult(outfile);

            }
                catch (Exception ex)
                {
                }  
        }

        /// <summary>
        /// 显示结果
        /// </summary>
        private void ShowOCRResult(string result)
        {
            var imagebyte = File.ReadAllBytes(result);
            bmp = new Bitmap(new MemoryStream(imagebyte));
            pictureBox2.BackgroundImage = bmp;

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog=new ColorDialog();
            colorDialog.Color=bgcolor;
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            bgcolor = colorDialog.Color;
            engine.Setbackground(bgcolor.R, bgcolor.G, bgcolor.B);
            if(bmp!= null)
            {
                string outfile = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString() + ".bmp";
                engine.Seg(bmp, outfile);
                ShowOCRResult(outfile);
            }
        }
    }
}
