using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaconBuilder.Component
{
    public sealed partial class MapBox : UserControl
    {
        private const int Radius = 5;
        private readonly Font _font;
        private readonly DbPanel _panel = new DbPanel();
        private Bitmap _bmp;
        private Rectangle _marker = new Rectangle(0, 0, Radius*2, Radius*2);
        private string _markerText;
        private Size _markerTextSize;
        private bool _mouseDown;
        private Point _pt;

        private float _scale = 100.00f;
        private Point _textpt;

        public MapBox()
        {
            InitializeComponent();

            btnZoomIn.Click += ZoomIn;
            btnZoomOut.Click += ZoomOut;

            _panel.Dock = DockStyle.Fill;
            _panel.Paint += PanelPaint;
            _panel.MouseDown += PanelMouseDown;
            _panel.MouseMove += PanelMouseMove;
            _panel.MouseUp += PanelMouseUp;

            Controls.Add(_panel);

            _font = new Font(Font.FontFamily, 10);
            MarkerText = "You are here";
        }


        public Image Image
        {
            get { return _bmp; }
            set
            {
                _bmp = value as Bitmap;
                if (_bmp != null)
                    ClientSize = new Size(_bmp.Width, _bmp.Height);
            }
        }

        public int X
        {
            get { return _pt.X; }
            private set
            {
                _pt.X = value;
                _marker.X = value - Radius;
                _textpt.X = value + Radius;
            }
        }

        public int Y
        {
            get { return _pt.Y; }
            private set
            {
                _pt.Y = value;
                _marker.Y = value - Radius;
                _textpt.Y = value - (_markerTextSize.Height/2);
            }
        }

        public string MarkerText
        {
            get { return _markerText; }
            set
            {
                _markerText = value;
                _markerTextSize = TextRenderer.MeasureText(value, _font);
            }
        }

        private void ZoomIn(object sender, EventArgs e)
        {
        }

        private void ZoomOut(object sender, EventArgs e)
        {
        }

        public event MapCoordinateChangedHandler MapCoordinateChanged;

        public void OnMapCoordinateChanged(MouseEventArgs e)
        {
            MapCoordinateChangedHandler handler = MapCoordinateChanged;
            if (handler != null) handler(this, e);
        }

        public void MoveTo(int x, int y)
        {
            X = Math.Min(Math.Max(0, x), _panel.ClientSize.Width);
            Y = Math.Min(Math.Max(0, y), _panel.ClientSize.Height - panel1.Height - Radius);

            if (MapCoordinateChanged != null)
            {
                var e = new MouseEventArgs(MouseButtons.Right, 0, X, Y, 0);
                MapCoordinateChanged(this, e);
            }
        }

        private void PanelPaint(object sender, PaintEventArgs e)
        {
            if (_bmp != null)
                e.Graphics.DrawImage(_bmp, 0, 0);

            // Don't bother drawing anything outside the box.
            if (X < 0 || X < 0 || X > _panel.Width || Y > _panel.Height || (X == 0 && Y == 0)) return;

            // Draw marker ellipse.
            e.Graphics.FillEllipse(Brushes.Red, _marker);

            // Change brush colour and draw 'you are here' text.
            TextRenderer.DrawText(e.Graphics, MarkerText, _font, _textpt, Color.Black, Color.White);
        }

        private void PanelMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;

            MoveTo(e.X, e.Y);
            _panel.Invalidate();
        }

        private void PanelMouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                MoveTo(e.X, e.Y);
                // Redraw and invalidate the picturebox.
                _panel.Invalidate();
            }
        }

        private void PanelMouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }
    }
        
    public delegate void MapCoordinateChangedHandler(object sender, MouseEventArgs eventArgs);
}