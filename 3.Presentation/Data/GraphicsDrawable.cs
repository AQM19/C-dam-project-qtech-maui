using _4.Entities;
using LinqToDB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Presentation.Data
{
    public class GraphicsDrawable : IDrawable
    {
        public List<Lectura> Lecturas { get; set; } = new List<Lectura>();

        public Color LineColor { get; set; } = Colors.Red;
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Lecturas.IsNullOrEmpty()) return;

            canvas.StrokeColor = LineColor;
            canvas.StrokeSize = 4;
            canvas.StrokeDashPattern = new float[] { 2, 2 };

            DateTime maxX = Lecturas.Max(l => l.Fecha);
            DateTime minX = Lecturas.Min(l => l.Fecha);
            float maxY = 100;
            float minY = 0;

            for (int i = 0; i < Lecturas.Count - 1; i++)
            {
                float x1 = (float)((Lecturas[i].Fecha - minX).TotalMilliseconds / (maxX - minX).TotalMilliseconds * dirtyRect.Width);
                float y1Temp = (float)((1 - (Lecturas[i].Temperatura - minY) / (maxY - minY)) * dirtyRect.Height);
                float y1Hum = (float)((1 - (Lecturas[i].Humedad - minY) / (maxY - minY)) * dirtyRect.Height);
                float y1Luz = (float)((1 - (Lecturas[i].Luz - minY) / (maxY - minY)) * dirtyRect.Height);

                float x2 = (float)((Lecturas[i + 1].Fecha - minX).TotalMilliseconds / (maxX - minX).TotalMilliseconds * dirtyRect.Width);
                float y2Temp = Lecturas[i].Temperatura;
                float y2Hum = Lecturas[i].Humedad;
                float y2Luz = Lecturas[i].Luz;

                canvas.StrokeColor = Colors.Red;
                canvas.StrokeSize = 1;
                canvas.DrawLine(x1, y1Temp, x2, y2Temp);

                canvas.StrokeColor = Colors.Green;
                canvas.StrokeSize = 1;
                canvas.DrawLine(x1, y1Hum, x2, y2Hum);

                canvas.StrokeColor = Colors.Blue;
                canvas.StrokeSize = 1;
                canvas.DrawLine(x1, y1Luz, x2, y2Luz);
            }
        }
    }
}
