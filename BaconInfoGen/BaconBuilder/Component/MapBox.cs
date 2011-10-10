using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BaconBuilder.Component
{
    public partial class MapBox : UserControl
    {
        private const int Radius = 5;
        private const double Epsilon = 0;
        private readonly Font _font;
        private readonly DbPanel _panel = new DbPanel();
        private Bitmap _bmp;
        private Rectangle _marker = new Rectangle(0, 0, Radius*2, Radius*2);
        private string _markerText;
        private Size _markerTextSize;
        private bool _mouseDown;
        private Point _pt;
        private Point _textpt;
        private float _zoom = 100.00f;

        public MapBox()
        {
            InitializeComponent();

            btnZoomIn.Click += ZoomIn;
            btnZoomReset.Click += ZoomReset;
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
                Debug.Assert(_bmp != null, "_bmp != null");
                ClientSize = new Size(_bmp.Width, _bmp.Height + panel1.Height);
            }
        }

        public int X
        {
            get { return _pt.X; }
            set
            {
                if (Math.Abs(_pt.X - value) > Epsilon)
                {
                    _pt.X = value;
                    _marker.X = value - Radius;
                    _textpt.X = value + Radius;
                    OnMapCoordinateChanged(EventArgs.Empty);
                }
            }
        }

        public int Y
        {
            get { return _pt.Y; }
            set
            {
                if (Math.Abs(_pt.Y - value) > Epsilon)
                {
                    _pt.Y = value;
                    _marker.Y = value - Radius;
                    _textpt.Y = value - (_markerTextSize.Height/2);
                    OnMapCoordinateChanged(EventArgs.Empty);
                }
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

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (Math.Abs(_zoom - value) > Epsilon)
                {
                    _zoom = value;
                    OnZoomChanged(EventArgs.Empty);
                }
            }
        }

        private void ZoomIn(object sender, EventArgs e)
        {
            Zoom *= 2f;
            _panel.Invalidate();
        }

        private void ZoomReset(object sender, EventArgs e)
        {
            Zoom = 100.0f;
            _panel.Invalidate();
        }

        private void ZoomOut(object sender, EventArgs e)
        {
            Zoom /= 2f;
            _panel.Invalidate();
        }

        public event EventHandler MapCoordinateChanged;

        public event EventHandler ZoomChanged;

        private void OnMapCoordinateChanged(EventArgs e)
        {
            //this.AdjustLayout();
            if (MapCoordinateChanged != null)
                MapCoordinateChanged(this, e);
        }

        private void OnZoomChanged(EventArgs e)
        {
            //this.AdjustLayout();
            if (ZoomChanged != null)
                ZoomChanged(this, e);
        }

        public void MoveTo(int x, int y)
        {
            X = Math.Min(Math.Max(0, x), _bmp.Width);
            Y = Math.Min(Math.Max(0, y), _bmp.Height);
        }

        private void PanelPaint(object sender, PaintEventArgs e)
        {
            //float scale = _zoom/100;
            if (_bmp != null)
                e.Graphics.DrawImage(_bmp, 0, 0, _bmp.Width, _bmp.Height);

            // Don't bother drawing anything outside the box.
            if (X < 0 || X < 0 || X > _panel.Width || Y > _panel.Height || (X == 0 && Y == 0)) return;

            // Draw marker ellipse.
            e.Graphics.FillEllipse(Brushes.Red, _marker.X, _marker.Y, _marker.Width, _marker.Height);

            // Change brush colour and draw 'you are here' text.
            //var p = new Point((int) (_textpt.X * scale), (int) (_textpt.Y * scale));
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
}