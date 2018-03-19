/* Map.cs
 * Author: Cole Robertson
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// A GUI control that displays a map.
    /// </summary>
    public partial class Map : UserControl
    {
        /// <summary>
        /// The maximum number of times the map can be zoomed in from the initial level.
        /// </summary>
        private const int _maxZoom = 6;

        /// <summary>
        /// The streets.
        /// </summary>
        private QuadTree _streets;

        /// <summary>
        /// The current scale factor.
        /// </summary>
        private int _scale;

        /// <summary>
        /// The current zoom level.
        /// </summary>
        private int _zoom = 0;

        /// <summary>
        /// Constructs the control with the given streets, bounds, initial scale factor, and maximum zoom.
        /// </summary>
        /// <param name="streets">The streets in the map.</param>
        /// <param name="bounds">The rectangle bounding the map.</param>
        /// <param name="scale">The scale factor at which to display the map.</param>
        public Map(List<StreetSegment> streets, RectangleF bounds, int scale)
        {
            for (int i = 0; i < streets.Count; i++)
            {
                if (!IsWithinBounds(streets[i].Start, bounds) || !IsWithinBounds(streets[i].End, bounds))
                {
                    throw new ArgumentException("Street " + i + " is not within the given bounds.");
                }
            }
            InitializeComponent();
            _streets = new QuadTree(streets, bounds, _maxZoom);
            _scale = scale;
            Size = new Size((int)(scale * bounds.Width), (int)(scale * bounds.Height));
        }

        /// <summary>
        /// Determines whether the given point is within the given rectangle (or on an edge).
        /// </summary>
        /// <param name="p">The point.</param>
        /// <param name="bounds">The rectangle.</param>
        /// <returns>Whether p is within bounds.</returns>
        private static bool IsWithinBounds(PointF p, RectangleF bounds)
        {
            return p.X >= bounds.Left && p.X <= bounds.Right && p.Y >= bounds.Top && p.Y <= bounds.Bottom;
        }

        /// <summary>
        /// Draws portions of the control as necessary.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle clip = e.ClipRectangle;
            e.Graphics.Clip = new Region(clip);
            _streets.Draw(e.Graphics, _scale, _zoom);
        }

        /// <summary>
        /// Zooms in one level, if possible.
        /// </summary>
        public void ZoomIn()
        {
            if (CanZoomIn)
            {
                _scale *= 2;
                _zoom++;
                Size = new Size(Size.Width * 2, Size.Height * 2);
                Invalidate();
            }
        }

        /// <summary>
        /// Zooms out one level, if possible.
        /// </summary>
        public void ZoomOut()
        {
            if (CanZoomOut)
            {
                _scale /= 2;
                _zoom--;
                Size = new Size(Size.Width / 2, Size.Height / 2);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets whether the map can zoom in.
        /// </summary>
        /// <returns>Whether the map can zoom in.</returns>
        public bool CanZoomIn
        {
            get
            {
                return _zoom < _maxZoom;
            }
        }

        /// <summary>
        /// Gets whether the map can zoom out.
        /// </summary>
        /// <returns>Whether the map can zoom out.</returns>
        public bool CanZoomOut
        {
            get
            {
                return _zoom > 0;
            }
        }
    }
}
