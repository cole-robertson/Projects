/* QuadTree.cs
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
    /// A quad tree storing street data.
    /// </summary>
    public class QuadTree
    {
        /// <summary>
        /// The streets that become visible at this height.
        /// </summary>
        private List<StreetSegment> _streets;

        /// <summary>
        /// The portion of the map represented by this tree.
        /// </summary>
        private RectangleF _bounds;

        /// <summary>
        /// The child representing the northwest quadrant.
        /// </summary>
        private QuadTree _northwestChild;

        /// <summary>
        /// The child representing the northeast quadrant.
        /// </summary>
        private QuadTree _northeastChild;

        /// <summary>
        /// The child representing the southwest quadrant.
        /// </summary>
        private QuadTree _southwestChild;

        /// <summary>
        /// The child representing the southeast quadrant.
        /// </summary>
        private QuadTree _southeastChild;

        /// <summary>
        /// Constructs a QuadTree with the given streets, bounds, and height.
        /// </summary>
        /// <param name="streets">The streets stored in this tree.</param>
        /// <param name="bounds">The portion of the map represented by this tree.</param>
        /// <param name="height">The height of this tree.</param>
        public QuadTree(List<StreetSegment> streets, RectangleF bounds, int height)
        {
            _bounds = bounds;
            if (height == 0)
            {
                _streets = streets;
            }
            else
            {
                _streets = new List<StreetSegment>();
                List<StreetSegment> smallerHeights = new List<StreetSegment>();
                SplitHeights(streets, height, _streets, smallerHeights);
                List<StreetSegment> west = new List<StreetSegment>();
                List<StreetSegment> east = new List<StreetSegment>();
                float halfWidth = _bounds.Width / 2;
                float midX = _bounds.Left + halfWidth;
                SplitEastWest(streets, midX, west, east);
                float halfHeight = _bounds.Height / 2;
                float midY = _bounds.Top + halfHeight;
                List<StreetSegment> northwest = new List<StreetSegment>();
                List<StreetSegment> southwest = new List<StreetSegment>();
                SplitNorthSouth(west, midY, northwest, southwest);
                List<StreetSegment> northeast = new List<StreetSegment>();
                List<StreetSegment> southeast = new List<StreetSegment>();
                SplitNorthSouth(east, midY, northeast, southeast);
                int childHeight = height - 1;
                _northwestChild = new QuadTree(northwest, new RectangleF(_bounds.Left, _bounds.Top, halfWidth, halfHeight), childHeight);
                _southwestChild = new QuadTree(southwest, new RectangleF(_bounds.Left, midY, halfWidth, halfHeight), childHeight);
                _northeastChild = new QuadTree(northeast, new RectangleF(midX, _bounds.Top, halfWidth, halfHeight), childHeight);
                _southeastChild = new QuadTree(southeast, new RectangleF(midX, midY, halfWidth, halfHeight), childHeight);
            }
        }

        /// <summary>
        /// Partitions the given list of all streets into two lists: the streets that do not go to the south of the given y-value, and
        /// the streets that do not go to the north of the given y-value. The remaining streets are split at the point at which they cross
        /// the given y-value, and each part is placed into the appropriate list.
        /// </summary>
        /// <param name="allStreets"></param>
        /// <param name="y"></param>
        /// <param name="north"></param>
        /// <param name="south"></param>
        private static void SplitNorthSouth(List<StreetSegment> allStreets, float y, List<StreetSegment> north, List<StreetSegment> south)
        {
            foreach (StreetSegment s in allStreets)
            {
                if (s.Start.Y <= y && s.End.Y <= y)
                {
                    north.Add(s);
                }
                else if (s.Start.Y >= y && s.End.Y >= y)
                {
                    south.Add(s);
                }
                else
                {
                    float x = (s.End.X - s.Start.X) / (s.End.Y - s.Start.Y) * (y - s.Start.Y) + s.Start.X;
                    PointF mid = new PointF(x, y);
                    StreetSegment s1 = s;
                    StreetSegment s2 = s;
                    s1.End = mid;
                    s2.Start = mid;
                    if (s1.Start.Y <= y)
                    {
                        north.Add(s1);
                        south.Add(s2);
                    }
                    else
                    {
                        north.Add(s2);
                        south.Add(s1);
                    }
                }
            }
        }

        /// <summary>
        /// Partitions the streets in the given list into a list of streets to be displayed at the given tree height
        /// and a list of streets to be displayed only at smaller heights.
        /// </summary>
        /// <param name="allStreets">The list of streets to be split.</param>
        /// <param name="height">The tree height.</param>
        /// <param name="thisHeight">The list of streets to be displayed at the given height.</param>
        /// <param name="smallerHeights">The list of streets to be displayed only at smaller heights.</param>
        private static void SplitHeights(List<StreetSegment> allStreets, int height, List<StreetSegment> thisHeight, List<StreetSegment> smallerHeights)
        {
            foreach (StreetSegment s in allStreets)
            {
                if (s.VisibleLevels > height)
                {
                    thisHeight.Add(s);
                }
                else
                {
                    smallerHeights.Add(s);
                }
            }
        }

            /// <summary>
            /// Partitions the given list of streets into the streets that do not go to the east of the given x-value, and the streets that do not go west of the
            /// given x-value. All other streets are split at the point at which they cross the given x-value, and each part is placed into
            /// the appropriate list.
            /// </summary>
            /// <param name="allStreets">The streets to partition.</param>
            /// <param name="x">The x value.</param>
            /// <param name="west">The streets to the west of x.</param>
            /// <param name="east">The streets to the east of x.</param>
            private static void SplitEastWest(List<StreetSegment> allStreets, float x, List<StreetSegment> west, List<StreetSegment> east)
        {
            foreach (StreetSegment s in allStreets)
            {
                if (s.Start.X <= x && s.End.X <= x)
                {
                    west.Add(s);
                }
                else if (s.Start.X >= x && s.End.X >= x)
                {
                    east.Add(s);
                }
                else
                {
                    float y = (s.End.Y - s.Start.Y) / (s.End.X - s.Start.X) * (x - s.Start.X) + s.Start.Y;
                    PointF mid = new PointF(x, y);
                    StreetSegment s1 = s;
                    StreetSegment s2 = s;
                    s1.End = mid;
                    s2.Start = mid;
                    if (s1.Start.X <= x)
                    {
                        west.Add(s1);
                        east.Add(s2);
                    }
                    else
                    {
                        west.Add(s2);
                        east.Add(s1);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the portion of the map represented by this tree on the given graphics context at
        /// the given scale to the given depth.
        /// </summary>
        /// <param name="g">The graphics context.</param>
        /// <param name="scale">The scale factor at which to draw.</param>
        /// <param name="depth">The depth of the tree to use.</param>
        public void Draw(Graphics g, int scale, int depth)
        {
            RectangleF scaledBounds = new RectangleF(g.ClipBounds.X / scale, g.ClipBounds.Y / scale, g.ClipBounds.Width / scale, g.ClipBounds.Height / scale);
            if (_bounds.IntersectsWith(scaledBounds))
            {
                foreach (StreetSegment line in _streets)
                {
                    line.Draw(g, scale);
                }
                if (depth > 0)
                {
                    int childDepth = depth - 1;
                    _northwestChild.Draw(g, scale, childDepth);
                    _northeastChild.Draw(g, scale, childDepth);
                    _southwestChild.Draw(g, scale, childDepth);
                    _southeastChild.Draw(g, scale, childDepth);
                }
            }
        }
    }
}
