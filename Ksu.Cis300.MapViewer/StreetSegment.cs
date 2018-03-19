/* StreetSegment.cs
 * Author: Cole Robertson
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// A portion of a street represented by a straight line.
    /// </summary>
    public struct StreetSegment
    {
        /// <summary>
        /// The starting point.
        /// </summary>
        private PointF _start;

        /// <summary>
        /// Gets or sets the starting point.
        /// </summary>
        public PointF Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        /// <summary>
        /// The ending point.
        /// </summary>
        private PointF _end;

        /// <summary>
        /// Gets or sets the ending point.
        /// </summary>
        public PointF End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        /// <summary>
        /// The pen used to draw this street.
        /// </summary>
        private Pen _pen;

         /// <summary>
        /// The number of zoom levels at which this street is visible.
        /// </summary>
        private int _visibleLevels;

        /// <summary>
        /// Gets the number of zoom levels at which this street is visible.
        /// </summary>
        public int VisibleLevels
        {
            get
            {
                return _visibleLevels;
            }
        }

        /// <summary>
        /// Constructs a street segment with the given starting point, ending point,
        /// color, width, and number of visible levels.
        /// </summary>
        /// <param name="start">The starting point.</param>
        /// <param name="end">The ending point.</param>
        /// <param name="col">The street's color.</param>
        /// <param name="width">The street's width in pixels</param>
        /// <param name="n">The number of zoom levels at which the street is visible.</param>
        public StreetSegment(PointF start, PointF end, Color col, float width, int n)
        {
            _start = start;
            _end = end;
            _pen = new Pen(col, width);
            _visibleLevels = n;
        }

        /// <summary>
        /// Draws this street on the given graphics context at the given scale factor.
        /// </summary>
        /// <param name="g">The graphics context on which to draw.</param>
        /// <param name="scale">The scale factor.</param>
        public void Draw(Graphics g, int scale)
        {
            g.DrawLine(_pen, _start.X * scale, _start.Y * scale, _end.X * scale, _end.Y * scale);
        }
    }
}
