using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serial_FFT_plotter
{
    public partial class aiexplainview : Form
    {
        static int bio_nFeatures = Bio_ModelWeights.Layer0_Weights.GetLength(0);
        static int bio_nHidden = Bio_ModelWeights.Layer0_Weights.GetLength(1);
        static int fall_nFeatures = Fall_ModelWeights.Layer0_Weights.GetLength(0);
        static int fall_nHidden = Fall_ModelWeights.Layer0_Weights.GetLength(1);

        double[] bio_featureImportance = new double[bio_nFeatures];
        double[] bio_positiveContrib = new double[bio_nFeatures];
        double[] bio_negativeContrib = new double[bio_nFeatures];

        double[] fall_featureImportance = new double[fall_nFeatures];
        double[] fall_positiveContrib = new double[fall_nFeatures];
        double[] fall_negativeContrib = new double[fall_nFeatures];

        double[] bio_inference_contibute = new double[bio_nFeatures];
        double[] fall_inference_contibute = new double[fall_nFeatures];

        string[] global_show_mode_name = new string[] { "Magnitude Contribution (w/positive weight)", "Magnitude Contribution (w/negative weight)", "Positive Contribution", "Negative Contribution" };

        int global_show_mode = 0;
        int local_show_mode = 0;

        public int valid_local_fall = 0;
        public int valid_local_bio = 0;

        public aiexplainview()
        {
            InitializeComponent();
        }

        public void set_bio_local_explain(double[] _contibute)
        {
            Array.Copy(_contibute, bio_inference_contibute, bio_inference_contibute.Length);
            valid_local_bio = 1;

            local_view.Invalidate();
        }

        public void set_fall_local_explain(double[] _contibute)
        {
            Array.Copy(_contibute, fall_inference_contibute, fall_inference_contibute.Length);
            valid_local_fall = 1;

            local_view.Invalidate();
        }

        private void aiexplainview_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < bio_nFeatures; i++)
            {
                double sum = 0;
                for (int j = 0; j < bio_nHidden; j++)
                {
                    sum += Bio_ModelWeights.Layer0_Weights[i, j] * Bio_ModelWeights.Layer1_Weights[j, 0];
                }

                if (sum > 0)
                {
                    bio_positiveContrib[i] += sum;
                }
                else
                {
                    bio_negativeContrib[i] -= sum;
                }
                bio_featureImportance[i] += Math.Abs(sum);
            }

            for (int i = 0; i < fall_nFeatures; i++)
            {
                double sum = 0;
                for (int j = 0; j < fall_nHidden; j++)
                {
                    sum += Fall_ModelWeights.Layer0_Weights[i, j] * Fall_ModelWeights.Layer1_Weights[j, 0];
                }

                if (sum > 0)
                {
                    fall_positiveContrib[i] += sum;
                }
                else
                {
                    fall_negativeContrib[i] -= sum;
                }
                fall_featureImportance[i] += Math.Abs(sum);
            }

            global_view.Invalidate();
            local_view.Invalidate();
        }

        public class IndexedValue
        {
            public int Index { get; set; }
            public double Value { get; set; }
        }

        private void global_view_Paint(object sender, PaintEventArgs e)
        {
            Pen base_line_pen = new Pen(Brushes.DarkGreen, 1.0F);
            Pen bio_line_pen = new Pen(Brushes.LightGreen, 1.0F);

            int bio_start_draw_x = 20;
            int bio_start_draw_y = global_view.Height - 20;
            float bio_draw_height = global_view.Height - 40;
            int bio_draw_width = 258 * 2;

            // bio
            double[] selected_draw = bio_featureImportance;
            if (global_show_mode == 2)
            {
                selected_draw = bio_positiveContrib;
            }
            else if (global_show_mode == 3)
            {
                selected_draw = bio_negativeContrib;
            }

            List<IndexedValue> indexed;
            if (global_show_mode == 0)
            {
                indexed = bio_positiveContrib
                .Select((value, index) => new IndexedValue { Index = index, Value = value })
                .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                .ToList();
            }
            else
            {
                indexed = bio_negativeContrib
                .Select((value, index) => new IndexedValue { Index = index, Value = value })
                .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                .ToList();
            }


            double bio_max = selected_draw.Max();
            double bio_min = selected_draw.Min();

            e.Graphics.DrawLine(base_line_pen, bio_start_draw_x - 1, bio_start_draw_y + 1, bio_start_draw_x - 1, bio_start_draw_y - bio_draw_height + 1);
            e.Graphics.DrawLine(base_line_pen, bio_start_draw_x - 1, bio_start_draw_y + 1, bio_start_draw_x + bio_draw_width - 1, bio_start_draw_y + 1);
            for(int i = 0; i < bio_nFeatures; i++)
            {
                float draw_height = (float)(((selected_draw[indexed[i].Index] - bio_min) / bio_max) * bio_draw_height);
                int draw_x = bio_start_draw_x + indexed[i].Index * 2;
                int color_value = 255 - (i);
                int inv_color = (i);

                if(color_value < 0)
                {
                    color_value = 0;
                    inv_color = 255;
                }
                Pen line_pen = new Pen(Color.FromArgb(255, 255, 255), 1);

                if (global_show_mode == 0)
                {
                    line_pen = new Pen(Color.FromArgb(255, inv_color, inv_color), 1);
                }
                else if (global_show_mode == 1)
                {
                    line_pen = new Pen(Color.FromArgb(inv_color, inv_color, 255), 1);
                }
                else if (global_show_mode == 2)
                {
                    line_pen = new Pen(Color.FromArgb(255, 0, 0), 1);
                }
                else if (global_show_mode == 3)
                {
                    line_pen = new Pen(Color.FromArgb(0, 0, 255), 1);
                }
                e.Graphics.DrawLine(line_pen, draw_x, bio_start_draw_y, draw_x, bio_start_draw_y - draw_height);
            }

            e.Graphics.DrawString("Bio Model [non Bio -> red] [Bio -> blue]", new Font("Arial", 10), Brushes.White, bio_start_draw_x, bio_start_draw_y - bio_draw_height - 20);

            int fall_start_draw_x = 600;
            int fall_start_draw_y = global_view.Height - 20;
            float fall_draw_height = global_view.Height - 40;
            int fall_draw_width = 257 * 2;

            // fall
            selected_draw = fall_featureImportance;
            if (global_show_mode == 2)
            {
                selected_draw = fall_positiveContrib;
            }
            else if (global_show_mode == 3)
            {
                selected_draw = fall_negativeContrib;
            }

            if (global_show_mode == 0)
            {
                indexed = fall_positiveContrib
                .Select((value, index) => new IndexedValue { Index = index, Value = value })
                .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                .ToList();
            }
            else
            {
                indexed = fall_negativeContrib
                .Select((value, index) => new IndexedValue { Index = index, Value = value })
                .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                .ToList();
            }

            double fall_max = selected_draw.Max();
            double fall_min = selected_draw.Min();

            e.Graphics.DrawLine(base_line_pen, fall_start_draw_x - 1, fall_start_draw_y + 1, fall_start_draw_x - 1, fall_start_draw_y - fall_draw_height + 1);
            e.Graphics.DrawLine(base_line_pen, fall_start_draw_x - 1, fall_start_draw_y + 1, fall_start_draw_x + fall_draw_width - 1, fall_start_draw_y + 1);
            for (int i = 0; i < fall_nFeatures; i++)
            {
                float draw_height = (float)(((selected_draw[indexed[i].Index] - fall_min) / fall_max) * fall_draw_height);
                int draw_x = fall_start_draw_x + indexed[i].Index * 2;
                int color_value = 255 - (i);
                int inv_color = (i);

                if (color_value < 0)
                {
                    color_value = 0;
                    inv_color = 255;
                }
                Pen line_pen = new Pen(Color.FromArgb(255, 255, 255), 1);

                if (global_show_mode == 0)
                {
                    line_pen = new Pen(Color.FromArgb(255, inv_color, inv_color), 1);
                }
                else if (global_show_mode == 1)
                {
                    line_pen = new Pen(Color.FromArgb(inv_color, inv_color, 255), 1);
                }
                else if (global_show_mode == 2)
                {
                    line_pen = new Pen(Color.FromArgb(255, 0, 0), 1);
                }
                else if (global_show_mode == 3)
                {
                    line_pen = new Pen(Color.FromArgb(0, 0, 255), 1);
                }
                e.Graphics.DrawLine(line_pen, draw_x, fall_start_draw_y, draw_x, fall_start_draw_y - draw_height);
            }

            StringFormat right_aligment_format = new StringFormat
            {
                Alignment = StringAlignment.Far, // Align text to the right
            };

            e.Graphics.DrawString("Fall Model [non fall -> red] [fall -> blue]", new Font("Arial", 10), Brushes.White, fall_start_draw_x, fall_start_draw_y - fall_draw_height - 20);
            e.Graphics.DrawString(global_show_mode_name[global_show_mode], new Font("Arial", 10), Brushes.White, fall_start_draw_x + fall_draw_width, fall_start_draw_y - fall_draw_height - 20 , right_aligment_format);

        }

        private void local_view_Paint(object sender, PaintEventArgs e)
        {
            Pen base_line_pen = new Pen(Brushes.DarkGreen, 1.0F);
            Pen bio_line_pen = new Pen(Brushes.LightGreen, 1.0F);

            if (valid_local_bio == 1)
            {
                int bio_start_draw_x = 20;
                int bio_start_draw_y = local_view.Height - 20;
                float bio_draw_height = local_view.Height - 40;
                int bio_draw_width = 258 * 2;

                // bio
                double[] selected_draw = bio_inference_contibute;

                List<IndexedValue> indexed;
                if (global_show_mode == 0)
                {
                    indexed = selected_draw
                    .Select((value, index) => new IndexedValue { Index = index, Value = value })
                    .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                    .ToList();
                }
                else
                {
                    indexed = selected_draw
                    .Select((value, index) => new IndexedValue { Index = index, Value = value })
                    .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                    .ToList();
                }


                double bio_max = selected_draw.Max();
                double bio_min = selected_draw.Min();
                double bio_range = bio_max - bio_min;
                float bio_zero_y_draw = (float)(((0 - bio_min) / bio_range) * bio_draw_height);


                e.Graphics.DrawLine(base_line_pen, bio_start_draw_x - 1, bio_start_draw_y + 1, bio_start_draw_x - 1, bio_start_draw_y - bio_draw_height + 1);
                e.Graphics.DrawLine(base_line_pen, bio_start_draw_x - 1, bio_start_draw_y + 1, bio_start_draw_x + bio_draw_width - 1, bio_start_draw_y + 1);
                for (int i = 0; i < bio_nFeatures; i++)
                {
                    float draw_height = (float)(((selected_draw[indexed[i].Index] - bio_min) / bio_range) * bio_draw_height);
                    int draw_x = bio_start_draw_x + indexed[i].Index * 2;
                    int color_value = 255 - (i);
                    int inv_color = (i);

                    if (color_value < 0)
                    {
                        color_value = 0;
                        inv_color = 255;
                    }
                    Pen line_pen = new Pen(Color.FromArgb(50, 50, 255), 1);
                    if(selected_draw[indexed[i].Index] < 0)
                    {
                        line_pen = new Pen(Color.FromArgb(255, 50, 50), 1);
                    }

                    e.Graphics.DrawLine(line_pen, draw_x, bio_start_draw_y - bio_zero_y_draw, draw_x, bio_start_draw_y - draw_height);
                }

                e.Graphics.DrawString("Bio Model [non Bio -> red] [Bio -> blue]", new Font("Arial", 10), Brushes.White, bio_start_draw_x, bio_start_draw_y - bio_draw_height - 20);
            }

            if (valid_local_fall == 1)
            {
                int fall_start_draw_x = 600;
                int fall_start_draw_y = local_view.Height - 20;
                float fall_draw_height = local_view.Height - 40;
                int fall_draw_width = 257 * 2;

                // fall
                double[] selected_draw = fall_inference_contibute;

                List<IndexedValue> indexed;
                if (global_show_mode == 0)
                {
                    indexed = selected_draw
                    .Select((value, index) => new IndexedValue { Index = index, Value = value })
                    .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                    .ToList();
                }
                else
                {
                    indexed = selected_draw
                    .Select((value, index) => new IndexedValue { Index = index, Value = value })
                    .OrderByDescending(x => x.Value) // or .OrderBy(x => x.Value) for ascending
                    .ToList();
                }


                double fall_max = selected_draw.Max();
                double fall_min = selected_draw.Min();
                double fall_range = fall_max - fall_min;
                float fall_zero_y_draw = (float)(((0 - fall_min) / fall_range) * fall_draw_height);


                e.Graphics.DrawLine(base_line_pen, fall_start_draw_x - 1, fall_start_draw_y + 1, fall_start_draw_x - 1, fall_start_draw_y - fall_draw_height + 1);
                e.Graphics.DrawLine(base_line_pen, fall_start_draw_x - 1, fall_start_draw_y + 1, fall_start_draw_x + fall_draw_width - 1, fall_start_draw_y + 1);
                for (int i = 0; i < fall_nFeatures; i++)
                {
                    float draw_height = (float)(((selected_draw[indexed[i].Index] - fall_min) / fall_range) * fall_draw_height);
                    int draw_x = fall_start_draw_x + indexed[i].Index * 2;
                    int color_value = 255 - (i);
                    int inv_color = (i);

                    if (color_value < 0)
                    {
                        color_value = 0;
                        inv_color = 255;
                    }
                    Pen line_pen = new Pen(Color.FromArgb(50, 50, 255), 1);
                    if (selected_draw[indexed[i].Index] < 0)
                    {
                        line_pen = new Pen(Color.FromArgb(255, 50, 50), 1);
                    }

                    e.Graphics.DrawLine(line_pen, draw_x, fall_start_draw_y - fall_zero_y_draw, draw_x, fall_start_draw_y - draw_height);
                }

                e.Graphics.DrawString("fall Model [non fall -> red] [fall -> blue]", new Font("Arial", 10), Brushes.White, fall_start_draw_x, fall_start_draw_y - fall_draw_height - 20);
            }
        }

        private void showPositiveContibuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global_show_mode = 2;
            global_view.Invalidate();
        }

        private void showNegativeContibuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global_show_mode = 3;
            global_view.Invalidate();
        }

        private void showMagnitudeContibuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global_show_mode = 1;
            global_view.Invalidate();
        }

        private void postiveMagnitudeContibuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            global_show_mode = 0;
            global_view.Invalidate();
        }

        private void aiexplainview_SizeChanged(object sender, EventArgs e)
        {
            global_view.Invalidate();
            local_view.Invalidate();
        }
    }
}
