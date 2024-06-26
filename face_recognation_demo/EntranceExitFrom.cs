using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static face_recognation_demo.Form1;

namespace face_recognation_demo;

public partial class EntranceExitFrom : Form
{
    List<ProcessedPredictions> Predictions = new();
    public EntranceExitFrom()
    {
        InitializeComponent();
        // Remove duplicated items
        List<ProcessedPredictions> CleanList = new List<ProcessedPredictions>();
        foreach (ProcessedPredictions prediction in Program.form.processedPredictions)
        {
            if (!CleanList.Any(x => x.Name == prediction.Name && x.time.ToString() == prediction.time.ToString()))
                CleanList.Add(prediction);
        }
        Program.form.processedPredictions = CleanList;

        for (int i = 0; i < Program.form.processedPredictions.Count; i++)
        {
            Predictions.Add(Program.form.processedPredictions[i]);
        }

        Predictions.Reverse();
        for (int i = 0; i < Predictions.Count; i++)
        {
            string temp = Predictions[i].time.ToString() + " - " + Predictions[i].Name;
            lbx_list.Items.Add(temp);
        }
        lbx_list.SelectedIndex = 0;
    }

    private void lbx_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        Mat mat = new Mat();
        CvInvoke.Resize(Predictions[lbx_list.SelectedIndex].Frame.Mat, mat, new Size(711, 439));
        pic_image.Image = mat.ToBitmap();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        Program.form.isFormOpen = false;
    }

    private void silToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbx_list.SelectedIndex == -1) return;
            int index = lbx_list.SelectedIndex;
            string item = lbx_list.Items[lbx_list.SelectedIndex].ToString();
            Program.form.processedPredictions.RemoveAt(lbx_list.SelectedIndex);
            Predictions.RemoveAt(lbx_list.SelectedIndex);
            pic_image.Image = null;
            lbx_list.Items.Clear();
            for (int i = 0; i < Predictions.Count; i++)
            {
                string temp = Predictions[i].time.ToString() + " - " + Predictions[i].Name;
                lbx_list.Items.Add(temp);
            }
            lbx_list.SelectedIndex = index == 0 ? 0 : index - 1;
        }
        catch (Exception ex)
        {
        }
    }
}
