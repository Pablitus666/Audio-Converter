using System.Drawing;
using System.Windows.Forms;

namespace AudioConverter
{
    public class NoFocusButton : Button
    {
        private bool _isHovered = false; // Track hover state

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            this.Invalidate(); // Request repaint
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            this.Invalidate(); // Request repaint
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Draw the background image if available
            if (this.BackgroundImage != null)
            {
                pevent.Graphics.DrawImage(this.BackgroundImage, this.ClientRectangle);
            }

            // Draw the text in gold if hovered, otherwise white
            Color textColor = _isHovered ? System.Drawing.ColorTranslator.FromHtml("#FFD700") : Color.White;
            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
