using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AudioConverter
{
    // A custom column type for displaying a progress bar in a DataGridView.
    public class DataGridViewProgressColumn : DataGridViewColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }

    // A custom cell type for the progress bar column.
    public class DataGridViewProgressCell : DataGridViewTextBoxCell
    {
        // Override the Paint method to draw the progress bar.
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            // Set a default value of 0 for progress if the cell value is null or invalid.
            double progressVal = 0;
            if (value is double)
            {
                progressVal = (double)value;
            }
            else if (value is int)
            {
                progressVal = (int)value;
            }
            
            float percentage = (float)(progressVal / 100.0);

            // Let the base class paint the background and border.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "", errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            
            // Define colors for the progress bar.
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            
            // Use a specific color for the progress bar itself.
            Brush progressBrush = new SolidBrush(Color.FromArgb(0, 122, 204)); // A nice blue color

            // Draw the progress bar.
            graphics.FillRectangle(progressBrush, cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * (cellBounds.Width - 4))), cellBounds.Height - 4);
            
            // Draw the percentage text over the progress bar.
            string text = $"{progressVal:F1} %";
            SizeF textSize = graphics.MeasureString(text, cellStyle.Font);
            PointF textLocation = new PointF(cellBounds.X + cellBounds.Width / 2 - textSize.Width / 2, cellBounds.Y + cellBounds.Height / 2 - textSize.Height / 2);
            graphics.DrawString(text, cellStyle.Font, foreColorBrush, textLocation);

            // Clean up resources.
            foreColorBrush.Dispose();
            backColorBrush.Dispose();
            progressBrush.Dispose();
        }
    }
}
