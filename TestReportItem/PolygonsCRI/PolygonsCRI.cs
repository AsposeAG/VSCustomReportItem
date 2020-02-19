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
    using System.Drawing.Imaging;
    using System.IO;
    using Microsoft.ReportingServices.OnDemandReportRendering;

    /// <summary>
    /// The main class for the custom report item design-time component.
    /// The report processor first calls the GenerateReportItemDefinition method 
    /// and then calls the EvaluateReportItemInstance method to get the rendered 
    /// report item.
    /// </summary>
    [System.CLSCompliant(false)]
    public class PolygonsCustomReportItem : ICustomReportItem
    {
        //internal static OnCreateLogger lLog = new OnCreateLogger(@"d:\save\info.txt", "PolygonsCustomReportItem was initialized!");

        #region ICustomReportItem Members

        public void GenerateReportItemDefinition(CustomReportItem cri)
        {
            // Create the Image Definition object that will be 
            // used to render the custom report item
            cri.CreateCriImageDefinition();
            Image polygonImage = (Image)cri.GeneratedReportItem;
        }

        public void EvaluateReportItemInstance(CustomReportItem cri)
        {
            // Get the Image definition
            Image polygonImage = (Image)cri.GeneratedReportItem;

            // Render the image for the custom report item
            polygonImage.ImageInstance.ImageData = DrawImage(cri);
        }
        #endregion

        /// <summary>  
        /// Creates an image of the CustomReportItem's name  
        /// </summary>  
        private byte[] DrawImage(CustomReportItem customReportItem)
        {
            int width = 1;          // pixels  
            int height = 1;         // pixels  
            int resolution = 75;    // dpi  

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
            bitmap.SetResolution(resolution, resolution);

            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;

            // Get the Font for the Text  
            System.Drawing.Font font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace,
                12, System.Drawing.FontStyle.Regular);

            // Get the Brush for drawing the Text  
            System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);

            // Get the measurements for the image  
            System.Drawing.SizeF maxStringSize = graphics.MeasureString(customReportItem.Name, font);
            width = (int)(maxStringSize.Width + 2 * font.GetHeight(resolution));
            height = (int)(maxStringSize.Height + 2 * font.GetHeight(resolution));

            bitmap.Dispose();
            bitmap = new System.Drawing.Bitmap(width, height);
            bitmap.SetResolution(resolution, resolution);

            graphics.Dispose();
            graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;

            // Draw the text  
            graphics.DrawString(customReportItem.Name, font, brush, font.GetHeight(resolution),
                font.GetHeight(resolution));

            // Create the byte array of the image data  
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Bmp);
            memoryStream.Position = 0;
            byte[] imageData = new byte[memoryStream.Length];
            memoryStream.Read(imageData, 0, imageData.Length);

            return imageData;
        }
    }
}