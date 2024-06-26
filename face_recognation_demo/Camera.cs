using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FFmpeg.AutoGen.Example;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;

namespace face_recognation_demo;

public class Camera
{
    public Camera()
    {
        Initialize_Camera();
    }

    public Framer framer;
    public Image<Bgr, byte> Frame, OldFrame;
    public List<Image<Bgr, byte>> DetectionFrames = new List<Image<Bgr, byte>>();
    private string link;
    public void Initialize_Camera()
    {
        try
        {
            link = $"rtsp://" +
                    $"admin:" +
                    $"admin123@" +
                    $"192.168.1.110" +
                    $"/cam/realmonitor?channel=1&subtype=0&unicast=true&proto=Onvi4";

            framer = new Framer();
            framer.FrameReceived += Framer_FrameReceived;
            Task.Run(() => framer.StartAsync(link));
            //Detection();
        }
        catch (Exception ex)
        {

        }
    }

    List<Model> DetectedFaces = new List<Model>();
    private void Framer_FrameReceived(Bitmap frame)
    {
        try
        {
            if (frame == null) return;

            Frame = frame.ToImage<Bgr, byte>();
            //DetectionFrame = Frame;

            // EmguCV face detection
            var grayFrame = Frame.Convert<Bgr, byte>();
            var constFrame = Frame.Copy();
            var faces = Program.form._faceCascade.DetectMultiScale(grayFrame, 1.1, 20, Size.Empty);
            DetectedFaces.Clear();
            foreach (var face in faces)
            {
                if (face.Width <= 120) continue;
                Image<Bgr, byte> tempFrame = Frame.Copy();
                Rectangle r = new Rectangle(new Point(face.Location.X - 20, face.Location.Y - 20), new Size(face.Size.Width + 40, face.Size.Height + 40));
                tempFrame.ROI = r;
                DetectedFaces.Add(new Model { FaceRectangle = face, Face = tempFrame });
            }

            // Settings for detection frames for other picturebox
            if (DetectedFaces.Count > 0 && !isDetectRunning)
            {
                isDetectRunning = false;
                Detect(constFrame, DetectedFaces);
                int totalWidth = 0;
                int maxHeight = 0;
                foreach (Model img in DetectedFaces)
                {
                    totalWidth += img.FaceRectangle.Width + 40;
                    if (img.FaceRectangle.Height + 40 > maxHeight)
                        maxHeight = img.FaceRectangle.Height + 40;
                }
                Image<Bgr, byte> result = new Image<Bgr, byte>(totalWidth, maxHeight);
                int currentX = 0;
                foreach (var img in DetectedFaces)
                {
                    img.Face.CopyTo(result.GetSubRect(new System.Drawing.Rectangle(currentX, 0, img.FaceRectangle.Width + 40, img.FaceRectangle.Height + 40)));
                    currentX += img.FaceRectangle.Width;
                }
                Program.form.LoadImage(result.ToBitmap(), Program.form.pic_test);
            }

            // Draw rectangle faces
            foreach (var face in faces)
                Frame.Draw(face, new Bgr(Color.Red), 2);
            Mat resizedImage = new Mat();
            CvInvoke.Resize(Frame.Mat, resizedImage, new Size(960, 540));
            Program.form.LoadImage(resizedImage.ToBitmap());
        }
        catch (Exception ex)
        {

        }
    }

    bool isDetectRunning = false;
    void Detect(Image<Bgr, byte> frame, List<Model> faces)
    {
        Task.Run(async () =>
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();

                    for (int i = 0; i < faces.Count; i++)
                    {
                        using (var stream = new MemoryStream())
                        {
                            faces[i].Face.ToBitmap().Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] bitmapBytes = stream.ToArray();
                            content.Add(new ByteArrayContent(bitmapBytes), "images", $"image{i + 1}.jpg");
                        }
                    }

                    string url = "http://192.168.1.25:8080/api/get1";
                    var request = new HttpRequestMessage(HttpMethod.Post, url)
                    {
                        Content = content
                    };

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var reponseString = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<List<Prediction>>(reponseString);
                    Program.form.ProcessPredictions(frame, faces, model);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                isDetectRunning = false;
            }
        });
    }


    void Detection()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    if (DetectionFrames == null)
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                    if (DetectionFrames.Count == 0)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    using (var client = new HttpClient())
                    {
                        var content = new MultipartFormDataContent();

                        for (int i = 0; i < DetectionFrames.Count; i++)
                        {
                            using (var stream = new MemoryStream())
                            {
                                DetectionFrames[i].ToBitmap().Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                byte[] bitmapBytes = stream.ToArray();
                                content.Add(new ByteArrayContent(bitmapBytes), "images", $"image{i + 1}.jpg");
                            }
                        }

                        string url = "http://192.168.1.25:8080/api/get1";
                        var request = new HttpRequestMessage(HttpMethod.Post, url)
                        {
                            Content = content
                        };

                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var asd = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<List<Prediction>>(asd);
                        //Program.form.DrawFaces(model);
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                }
            }
        });
    }

    public class Model
    {
        public Image<Bgr, byte> Face { get; set; }
        public Rectangle FaceRectangle { get; set; }
    }
}
