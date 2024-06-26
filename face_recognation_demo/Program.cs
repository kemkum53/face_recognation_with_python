using Newtonsoft.Json;
using static face_recognation_demo.Form1;

namespace face_recognation_demo
{
    internal static class Program
    {
        public static Form1 form;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.ApplicationExit += Application_ApplicationExit;
            form = new Form1();
            form.processedPredictions = JsonConvert.DeserializeObject<List<ProcessedPredictions>>(File.ReadAllText("C:\\Users\\kemkum\\Desktop\\detections.txt"));
            if (form.processedPredictions == null) form.processedPredictions = new();
            Application.Run(form);
        }

        private static void Application_ApplicationExit(object? sender, EventArgs e)
        {
            List<ProcessedPredictions> CleanList = new List<ProcessedPredictions>();
            foreach (ProcessedPredictions prediction in form.processedPredictions)
            {
                if (!CleanList.Any(x => x.Name == prediction.Name && x.time.ToString() == prediction.time.ToString()))
                    CleanList.Add(prediction);
            }
            using (StreamWriter w = new StreamWriter("C:\\Users\\kemkum\\Desktop\\detections.txt"))
                w.WriteLine(JsonConvert.SerializeObject(CleanList));
            //File.WriteAllText($"C:\\Users\\kemkum\\Desktop\\detections.txt", JsonConvert.SerializeObject(form.processedPredictions));
        }
    }
}