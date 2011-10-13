using System;
using System.Drawing;
using System.Windows.Forms;

namespace Components
{
	public partial class MapBox : UserControl
	{
		private const int Radius = 5;
		private const double Epsilon = 0;
		private readonly Font _font;
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

			_font = new Font(Font.FontFamily, 10);
			MarkerText = "You are here";
		}

		public Image Image
		{
			get { return canvas.BackgroundImage; }
			set
			{
				canvas.BackgroundImage = value;
				canvas.ClientSize = new Size(Image.Width, Image.Height);
			}
		}

		public int X
		{
			get { return _pt.X; }
			set
			{
				if (Math.Abs(_pt.X - value) <= Epsilon) return;
				_pt.X = value;
				_marker.X = value - Radius;
				_textpt.X = value + Radius;
				OnMapCoordinateChanged(EventArgs.Empty);
			}
		}

		public int Y
		{
			get { return _pt.Y; }
			set
			{
				if (Math.Abs(_pt.Y - value) <= Epsilon) return;
				_pt.Y = value;
				_marker.Y = value - Radius;
				_textpt.Y = value - (_markerTextSize.Height/2);
				OnMapCoordinateChanged(EventArgs.Empty);
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
				if (Math.Abs(_zoom - value) <= Epsilon) return;
				_zoom = value;
				OnZoomChanged(EventArgs.Empty);
			}
		}

		private void ZoomIn(object sender, EventArgs e)
		{
			Zoom *= 2f;
			canvas.Invalidate();
		}

		private void ZoomReset(object sender, EventArgs e)
		{
			Zoom = 100.0f;
			canvas.Invalidate();
		}

		private void ZoomOut(object sender, EventArgs e)
		{
			Zoom /= 2f;
			canvas.Invalidate();
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
			X = Math.Min(Math.Max(0, x), Image.Width);
			Y = Math.Min(Math.Max(0, y), Image.Height);
		}

		private void CanvasPaint(object sender, PaintEventArgs e)
		{
			//float scale = _zoom/100;
			if (Image != null)
				e.Graphics.DrawImage(Image, 0, 0, Image.Width, Image.Height);

			// Don't bother drawing anything outside the box.
			if (X < 0 || X < 0 || X > canvas.Width || Y > canvas.Height || (X == 0 && Y == 0)) return;

			// Draw marker ellipse.
			e.Graphics.FillEllipse(Brushes.Red, _marker.X, _marker.Y, _marker.Width, _marker.Height);

			// Change brush colour and draw 'you are here' text.
			//var p = new Point((int) (_textpt.X * scale), (int) (_textpt.Y * scale));
			TextRenderer.DrawText(e.Graphics, MarkerText, _font, _textpt, Color.Black, Color.White);
		}

		private void CanvasMouseDown(object sender, MouseEventArgs e)
		{
			_mouseDown = true;

			MoveTo(e.X, e.Y);
			canvas.Invalidate();
		}

		private void CanvasMouseMove(object sender, MouseEventArgs e)
		{
			if (!_mouseDown) return;
			MoveTo(e.X, e.Y);
			// Redraw and invalidate the picturebox.
			canvas.Invalidate();
		}

		private void CanvasMouseUp(object sender, MouseEventArgs e)
		{
			_mouseDown = false;
		}
	}
}