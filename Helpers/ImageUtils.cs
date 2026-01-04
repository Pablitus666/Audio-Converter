using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;

namespace AudioConverter.Helpers
{
    public static class ImageUtils
    {
        public static Image? CargarImagenDesdeRecurso(string imageName)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            
            // Robustly find the resource name, ignoring case and potential weird spacing from the compiler.
            string? resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith(imageName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
            {
                Logger.Log($"Error: Embedded resource ending with '{imageName}' not found.", true);
                return null;
            }

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    // This should theoretically not happen if resourceName was found, but for safety:
                    Logger.Log($"Error: Could not get stream for resource '{resourceName}'.", true);
                    return null;
                }

                try
                {
                    using (Image tempImage = Image.FromStream(stream))
                    {
                        Bitmap bmp = new Bitmap(tempImage.Width, tempImage.Height);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.DrawImage(tempImage, 0, 0, tempImage.Width, tempImage.Height);
                        }
                        return bmp;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"Error loading embedded image '{resourceName}': {ex.Message}", true);
                    return null;
                }
            }
        }

        public static Image EscalarImagenAltaCalidad(Image originalImage, int width, int height)
        {
            if (originalImage == null) return new Bitmap(1, 1);

            Bitmap newImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(originalImage, 0, 0, width, height);
            }
            return newImage;
        }
    }
}
