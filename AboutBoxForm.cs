using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AudioConverter.Helpers;

namespace AudioConverter
{
    public sealed class AboutBoxForm : Form
    {
        public AboutBoxForm()
        {
            Text = LocalizationManager.GetString("AboutBox_Title");
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScaleDimensions = new SizeF(96F, 96F);
            ClientSize = new Size(404, 231);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.FromArgb(2, 48, 71);

            CargarIconoFormulario();

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.Transparent,
                Padding = new Padding(6)
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));

            Controls.Add(mainLayout);

            var infoPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(18, 12, 10, 12), BackColor = Color.Transparent };
            var infoPicture = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent, Image = CargarImagenSegura("informacion.png") };
            infoPanel.Controls.Add(infoPicture);
            mainLayout.Controls.Add(infoPanel, 0, 0);

            var robotPicture = new PictureBox { Size = new Size(150, 150), Anchor = AnchorStyles.None, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent, Margin = new Padding(6, 6, 8, 0), Image = CargarImagenSegura("robot.png") };
            mainLayout.Controls.Add(robotPicture, 1, 0);

            var btnCerrar = new Button
            {
                Text = LocalizationManager.GetString("AboutBox_CloseButton"),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(96, 34),
                FlatStyle = FlatStyle.Flat,
                TabStop = false,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 0, 0, 6)
            };

            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCerrar.FlatAppearance.MouseDownBackColor = Color.Transparent;

            Image? botonImg = CargarImagenSegura("boton.png");
            if (botonImg != null)
            {
                btnCerrar.BackgroundImage = ImageUtils.EscalarImagenAltaCalidad(botonImg, btnCerrar.Width, btnCerrar.Height);
            }
            else
            {
                btnCerrar.BackColor = Color.FromArgb(0, 158, 166);
            }

            btnCerrar.MouseEnter += (_, _) => btnCerrar.ForeColor = Color.Gold;
            btnCerrar.MouseLeave += (_, _) => btnCerrar.ForeColor = Color.White;
            btnCerrar.Click += (_, _) => Close();

            mainLayout.Controls.Add(btnCerrar, 0, 1);
            mainLayout.SetColumnSpan(btnCerrar, 2);
        }

        private void CargarIconoFormulario()
        {
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                const string resourceName = "AudioConverter.images.icon.ico";

                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null) { Logger.Log($"Embedded icon not found: {resourceName}", true); return; }
                Icon = new Icon(stream);
            }
            catch (Exception ex) { Logger.Log($"Error loading AboutBox icon: {ex.Message}", true); }
        }

        private static Image? CargarImagenSegura(string nombre)
        {
            try
            {
                var img = ImageUtils.CargarImagenDesdeRecurso(nombre);
                return img != null ? (Image)img.Clone() : null;
            }
            catch (Exception ex) { Logger.Log($"Error loading image {nombre}: {ex.Message}", true); return null; }
        }
    }
}
