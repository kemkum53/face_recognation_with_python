using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Runtime.Intrinsics.X86;
using static face_recognation_demo.Camera;

namespace face_recognation_demo;

public partial class Form1 : Form
{
    Camera cam;
    public CascadeClassifier _faceCascade;
    public LBPHFaceRecognizer _faceRecognizer;
    public Form1()
    {
        InitializeComponent();
        _faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
        cam = new Camera();
    }

    public void LoadImage(Bitmap bitmap, PictureBox pBox = null)
    {
        try
        {
            if (InvokeRequired)
            {
                Invoke(new Action<Bitmap, PictureBox>(LoadImage), new object[] { bitmap, pBox });
                return;
            }
            if (pBox == null)
                pic_main.Image = bitmap;
            else
                pBox.Image = bitmap;
        }
        catch (Exception ex)
        {

        }
    }

    public void LoadText(string text)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<string>(LoadText), new object[] { text });
            return;
        }
        label1.Text = text;
    }

    public List<ProcessedPredictions> processedPredictions = new();

    public void ProcessPredictions(Image<Bgr, byte> frame, List<Model> faces, List<Prediction> predicts)
    {
        Task.Run(() =>
        {
            try
            {
                if (faces.Count != predicts.Count) return;
                for (int i = 0; i < predicts.Count; i++)
                {
                    ProcessedPredictions p = processedPredictions.LastOrDefault(x => x.Name == predicts[i].name);
                    if (p == null || p.time.AddMinutes(1) < DateTime.Now || (p.Name == "Unknown" && p.time.AddSeconds(10) < DateTime.Now))
                        processedPredictions.Add(new ProcessedPredictions { Name = predicts[i].name, Frame = frame.Copy(), time = DateTime.Now, FaceRectangle = faces[i].FaceRectangle });
                    // TODO resimler ayrý kaydedilebilir.
                }
                string names = "";
                for (int i = 0; i < faces.Count; i++)
                {
                    frame.Draw(faces[i].FaceRectangle, new Bgr(Color.Red), 2);
                    CvInvoke.PutText(frame, predicts[i].name, new Point(faces[i].FaceRectangle.X + 10, faces[i].FaceRectangle.Y - 10), FontFace.HersheyPlain, 3.0, new Bgr(Color.Blue).MCvScalar, thickness: 2);
                    names += predicts[i].name + " - ";
                }
                Mat resizedImage = new Mat();
                CvInvoke.Resize(frame.Mat, resizedImage, new Size(914, 540));
                LoadImage(resizedImage.ToBitmap(), pictureBox1);
                LoadText(names.Remove(names.Length - 2));
            }
            catch (Exception ex)
            {

            }
        }).Wait();
    }

    public class ProcessedPredictions
    {
        public string Name { get; set; }
        public Image<Bgr, byte> Frame { get; set; }
        public DateTime time { get; set; }
        public Rectangle FaceRectangle { get; set; }
    }

    public bool isFormOpen = false;
    private void btn_old_Click(object sender, EventArgs e)
    {
        if (isFormOpen || processedPredictions == null || processedPredictions.Count <= 0) return;
        isFormOpen = true;
        EntranceExitFrom f = new EntranceExitFrom();
        f.Show();
    }
}
