//-----------------------------------------------------------------------
//  This file is part of the Microsoft Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
// 
//  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//-----------------------------------------------------------------------

namespace Microsoft.Samples.ReportingServices
{
    using System;
    using System.Drawing;
    using Microsoft.ReportDesigner;
    using Microsoft.ReportingServices.Interfaces;
    using Microsoft.ReportingServices.RdlObjectModel;

    [LocalizedName("Polygons")]
    [ToolboxBitmap(typeof(PolygonsDesigner), "Polygons.ico")]
    // this CRI-specific attribute sets the name of the 
    // custom report item which is referenced by the config
    // files and saved in the report definition language 
    [CustomReportItem("Polygons")]
    // the main class for our CRI design-time component
    [System.CLSCompliant(false)]
    public class PolygonsDesigner : CustomReportItemDesigner
    {
        public override void Draw(Graphics gr, ReportItemDrawParams dp)
        {
            if (gr == null)
            {
                throw new ArgumentNullException("gr");
            }

            int pixelWidth = (int)Math.Round(Width);
            int pixelHeight = (int)Math.Round(Height);
            if (pixelWidth > pixelHeight)
            {
                pixelWidth = pixelHeight;
            }
            else
            {
                pixelHeight = pixelWidth;
            }

            int alpha = 255;

            Color color = Color.FromArgb(alpha, Style.Color.Value.Color);
            Color borderColor = Style.Border.Color.Value.Color;
            Pen borderPen = new Pen(borderColor);
            SolidBrush colorBrush = new SolidBrush(color);
            SolidBrush backgroundColorBrush = new SolidBrush(Style.BackgroundColor.Value.Color);
            gr.FillRectangle(backgroundColorBrush, 0, 0, pixelWidth, pixelHeight);
            gr.FillRectangle(colorBrush, 3 * pixelWidth / 8, 3 * pixelHeight / 8, pixelWidth / 2, pixelHeight / 2);
            gr.DrawRectangle(borderPen, 3 * pixelWidth / 8, 3 * pixelHeight / 8, pixelWidth / 2, pixelHeight / 2);
            Point[] points = new Point[3];
            points[0] = new Point(3 * pixelWidth / 8, pixelHeight / 8);
            points[1] = new Point(pixelWidth / 8, 3 * pixelHeight / 5);
            points[2] = new Point(5 * pixelWidth / 8, 3 * pixelHeight / 5);
            gr.FillPolygon(colorBrush, points);
            gr.DrawPolygon(borderPen, points);
            borderPen.Dispose();
            colorBrush.Dispose();
            backgroundColorBrush.Dispose();
        }

        // initialize our CustomData structure with default values
        public override void InitializeNewComponent()
        {

        }
    }
}
