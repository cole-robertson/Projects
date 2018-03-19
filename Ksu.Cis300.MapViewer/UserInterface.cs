/* UserInterface.cs
 * Author: Cole Robertson
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// A GUI for a program for viewing maps.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// The initial scale factor.
        /// </summary>
        private const int _initalScale = 10;

        /// <summary>
        /// The current map.
        /// </summary>
        private Map uxMap;

        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles a Click event on the "Open Map" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpenMap_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RectangleF bounds;
                    List<StreetSegment> streets = ReadFile(uxOpenDialog.FileName, out bounds);
                    uxMap = new Map(streets, bounds, _initalScale);
                    uxMapContainer.Controls.Clear();
                    uxMapContainer.Controls.Add(uxMap);
                    uxZoomIn.Enabled = true;
                    uxZoomOut.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Reads the given input file.
        /// </summary>
        /// <param name="fileName">The file to read.</param>
        /// <param name="bounds">The map bounds.</param>
        /// <returns>The streets in the map.</returns>
        private static List<StreetSegment> ReadFile(string fileName, out RectangleF bounds)
        {
            using (StreamReader input = new StreamReader(fileName))
            {
                string line = input.ReadLine();
                string[] fields = line.Split(',');
                bounds = new RectangleF(0, 0, Convert.ToSingle(fields[0]), Convert.ToSingle(fields[1]));
                List<StreetSegment> streets = new List<StreetSegment>();
                while (!input.EndOfStream)
                {
                    line = input.ReadLine();
                    fields = line.Split(',');
                    PointF start = new PointF(Convert.ToSingle(fields[0]), Convert.ToSingle(fields[1]));
                    PointF end = new PointF(Convert.ToSingle(fields[2]), Convert.ToSingle(fields[3]));
                    streets.Add(new StreetSegment(start, end, Color.FromArgb(Convert.ToInt32(fields[4])), Convert.ToSingle(fields[5]), Convert.ToInt32(fields[6])));
                }
                return streets;
            }
        }

        /// <summary>
        /// Handles a Click event on the "Zoom In" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            Point scroll = uxMapContainer.AutoScrollPosition;
            scroll = new Point(-scroll.X, -scroll.Y);
            uxMap.ZoomIn();
            uxZoomIn.Enabled = uxMap.CanZoomIn;
            uxZoomOut.Enabled = true;
            Size viewSize = uxMapContainer.ClientSize;
            scroll = new Point(scroll.X * 2 + viewSize.Width / 2, scroll.Y * 2 + viewSize.Height / 2);
            uxMapContainer.AutoScrollPosition = scroll;
        }

        /// <summary>
        /// Handles a Click event on the "Zoom Out" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            Point scroll = uxMapContainer.AutoScrollPosition;
            scroll = new Point(-scroll.X, -scroll.Y);
            uxMap.ZoomOut();
            uxZoomIn.Enabled = true;
            uxZoomOut.Enabled = uxMap.CanZoomOut;
            Size viewSize = uxMapContainer.ClientSize;
            scroll = new Point(scroll.X / 2 - viewSize.Width / 4, scroll.Y / 2 - viewSize.Height / 4);
            uxMapContainer.AutoScrollPosition = scroll;
        }
    }
}
