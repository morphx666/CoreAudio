using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing;

namespace xFXJumpStart {
    public static class GraphicsExtension {
        private const double Rad2Deg = 180.0 / Math.PI;
        private const double Deg2Rad = 1.0 / Rad2Deg;

        public static GraphicsPath GenerateRoundedRectangle(this Graphics graphics, RectangleF rectangle, float radius, RectangleEdgeFilter filter) {
            float diameter;
            GraphicsPath path = new GraphicsPath();
            if(radius <= 0.0F || filter == RectangleEdgeFilter.None) {
                path.AddRectangle(rectangle);
                path.CloseFigure();
                return path;
            } else {
                if(radius >= Math.Min(rectangle.Width, rectangle.Height) / 2.0)
                    return graphics.GenerateCapsule(rectangle);
                diameter = radius * 2.0F;
                SizeF sizeF = new SizeF(diameter, diameter);
                RectangleF arc = new RectangleF(rectangle.Location, sizeF);
                if((RectangleEdgeFilter.TopLeft & filter) == RectangleEdgeFilter.TopLeft)
                    path.AddArc(arc, 180, 90);
                else {
                    path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
                    path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
                }
                arc.X = rectangle.Right - diameter;
                if((RectangleEdgeFilter.TopRight & filter) == RectangleEdgeFilter.TopRight)
                    path.AddArc(arc, 270, 90);
                else {
                    path.AddLine(arc.X, arc.Y, arc.X + arc.Width, arc.Y);
                    path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X + arc.Width, arc.Y);
                }
                arc.Y = rectangle.Bottom - diameter;
                if((RectangleEdgeFilter.BottomRight & filter) == RectangleEdgeFilter.BottomRight)
                    path.AddArc(arc, 0, 90);
                else {
                    path.AddLine(arc.X + arc.Width, arc.Y, arc.X + arc.Width, arc.Y + arc.Height);
                    path.AddLine(arc.X, arc.Y + arc.Height, arc.X + arc.Width, arc.Y + arc.Height);
                }
                arc.X = rectangle.Left;
                if((RectangleEdgeFilter.BottomLeft & filter) == RectangleEdgeFilter.BottomLeft)
                    path.AddArc(arc, 90, 90);
                else {
                    path.AddLine(arc.X + arc.Width, arc.Y + arc.Height, arc.X, arc.Y + arc.Height);
                    path.AddLine(arc.X, arc.Y + arc.Height, arc.X, arc.Y);
                }
                path.CloseFigure();
            }
            return path;
        }

        public static GraphicsPath GenerateCapsule(this Graphics graphics, RectangleF rectangle) {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new GraphicsPath();

            if(rectangle.Width < 0 || rectangle.Height < 0) {
                path.AddEllipse(rectangle);
                path.CloseFigure();
                return path;
            }

            try {
                if(rectangle.Width > rectangle.Height) {
                    diameter = rectangle.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(rectangle.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = rectangle.Right - diameter;
                    path.AddArc(arc, 270, 180);
                } else if(rectangle.Width < rectangle.Height) {
                    diameter = rectangle.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(rectangle.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = rectangle.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                } else
                    path.AddEllipse(rectangle);
            } catch {
                path.AddEllipse(rectangle);
            } finally {
                path.CloseFigure();
            }

            return path;
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, float x, float y, float width, float height, float radius, RectangleEdgeFilter filter) {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            using(GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius, filter)) {
                SmoothingMode old = graphics.SmoothingMode;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawPath(pen, path);
                graphics.SmoothingMode = old;
            }
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, float x, float y, float width, float height, float radius) {
            graphics.DrawRoundedRectangle(pen, x, y, width, height, radius, RectangleEdgeFilter.All);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, int x, int y, int width, int height, int radius) {
            graphics.DrawRoundedRectangle(pen, x, y, width, height, radius);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radius, RectangleEdgeFilter filter) {
            graphics.DrawRoundedRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, filter);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radius) {
            graphics.DrawRoundedRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, RectangleEdgeFilter.All);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, RectangleF rectangle, int radius, RectangleEdgeFilter filter) {
            graphics.DrawRoundedRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, filter);
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, RectangleF rectangle, int radius) {
            graphics.DrawRoundedRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, RectangleEdgeFilter.All);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, float x, float y, float width, float height, float radius, RectangleEdgeFilter filter) {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            using(GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius, filter)) {
                SmoothingMode old = graphics.SmoothingMode;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
                graphics.SmoothingMode = old;
            }
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, float x, float y, float width, float height, float radius) {
            graphics.FillRoundedRectangle(brush, x, y, width, height, radius, RectangleEdgeFilter.All);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, int x, int y, int width, int height, int radius) {
            graphics.FillRoundedRectangle(brush, x, y, width, height, radius);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rectangle, int radius, RectangleEdgeFilter filter) {
            graphics.FillRoundedRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, filter);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rectangle, int radius) {
            graphics.FillRoundedRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, RectangleEdgeFilter.All);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, RectangleF rectangle, int radius, RectangleEdgeFilter filter) {
            graphics.FillRoundedRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, filter);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, RectangleF rectangle, int radius) {
            graphics.FillRoundedRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius, RectangleEdgeFilter.All);
        }

        public static void DrawEllipseFromCenter(this Graphics g, Pen pen, float x, float y, float width, float height) {
            g.DrawEllipse(pen, x - width / 2, y - height / 2, width, height);
        }

        public static void DrawEllipseFromCenter(this Graphics g, Pen pen, int x, int y, int width, int height) {
            g.DrawEllipse(pen, x - width / 2, y - height / 2, width, height);
        }

        public static void DrawEllipseFromCenter(this Graphics g, Pen pen, Rectangle rect) {
            DrawEllipseFromCenter(g, pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static void DrawEllipseFromCenter(this Graphics g, Pen pen, RectangleF rect) {
            DrawEllipseFromCenter(g, pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static void FillEllipseFromCenter(this Graphics g, Brush brush, float x, float y, float width, float height) {
            g.FillEllipse(brush, x - width / 2, y - height / 2, width, height);
        }

        public static void FillEllipseFromCenter(this Graphics g, Brush brush, int x, int y, int width, int height) {
            g.FillEllipse(brush, x - width / 2, y - height / 2, width, height);
        }

        public static void FillEllipseFromCenter(this Graphics g, Brush brush, Rectangle rect) {
            FillEllipseFromCenter(g, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static void FillEllipseFromCenter(this Graphics g, Brush brush, RectangleF rect) {
            FillEllipseFromCenter(g, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static void DrawEllipseFromCenter(this Graphics g, Brush brush, RectangleF rect) {
            FillEllipseFromCenter(g, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static void FillDonut(this Graphics g, Brush backColor, Brush donutColor, Brush highlightColor, Rectangle rect, int donutSize, float startAngle, float sweepAngle, bool drawEndCaps = false) {
            g.FillEllipse(donutColor, rect);
            if(sweepAngle > 0)
                g.FillPie(highlightColor, rect, startAngle, sweepAngle);

            if(drawEndCaps && sweepAngle > 0) {
                RectangleF r = new RectangleF();
                r.X = (float)(rect.X + rect.Width / 2.0 + (rect.Width - donutSize) / 2.0 * Math.Cos(-startAngle * Deg2Rad));
                r.Y = (float)(rect.Y + rect.Height / 2.0 + (rect.Height - donutSize) / 2.0 * Math.Sin(startAngle * Deg2Rad));
                r.Width = donutSize;
                r.Height = donutSize;
                g.FillEllipseFromCenter(highlightColor, r);

                r.X = (float)(rect.X + rect.Width / 2.0 + (rect.Width - donutSize) / 2.0 * Math.Cos(-(startAngle + sweepAngle) * Deg2Rad));
                r.Y = (float)(rect.Y + rect.Height / 2.0 + (rect.Height - donutSize) / 2.0 * Math.Sin((startAngle + sweepAngle) * Deg2Rad));
                g.FillEllipseFromCenter(highlightColor, r);
            }

            rect.Inflate(-donutSize, -donutSize);
            g.FillEllipse(backColor, rect);
        }

        public static void DrawCurvedText(this Graphics g, string text, Point centre, float distanceFromCentreToBaseOfText, float radiansToTextCentre, Font font, Brush brush, bool clockwise = true) {
            // http://stackoverflow.com/a/11151457/518872

            // Circumference for use later
            double circleCircumference = Math.PI * 2 * distanceFromCentreToBaseOfText;

            // Get the width of each character
            float[] characterWidths = GetCharacterWidths(g, text, font).ToArray();

            // The overall height of the string
            float characterHeight = g.MeasureString(text, font).Height;

            float textLength = characterWidths.Sum();

            // The string length above is the arc length we'll use for rendering the string.
            // Work out the starting angle required to center the text across the radiansToTextCentre.
            double fractionOfCircumference = textLength / circleCircumference;

            double currentCharacterRadians = radiansToTextCentre + (clockwise ? -1 : 1) * Math.PI * fractionOfCircumference;

            for(int characterIndex = 0; characterIndex <= text.Length - 1; characterIndex++) {
                char c = text[characterIndex];

                // Polar to Cartesian
                double x = distanceFromCentreToBaseOfText * Math.Sin(currentCharacterRadians);
                double y = -(distanceFromCentreToBaseOfText * Math.Cos(currentCharacterRadians));

                using(GraphicsPath characterPath = new GraphicsPath()) {
                    characterPath.AddString(c.ToString(), font.FontFamily, (int)font.Style, font.Size, Point.Empty, StringFormat.GenericTypographic);

                    RectangleF pathBounds = characterPath.GetBounds();

                    // Transformation matrix to move the character to the correct location. 
                    // Note that all actions on the Matrix class are prepended, so we apply them in reverse.
                    Matrix transform = new Matrix();

                    // Translate to the final position
                    transform.Translate((float)(centre.X + x), (float)(centre.Y + y));

                    // Rotate the character
                    float rotationAngleDegrees = (float)(currentCharacterRadians * 180.0 / Math.PI - (clockwise ? 0 : 180.0));
                    transform.Rotate(rotationAngleDegrees);

                    // Translate the character so the center of its base Is over the origin
                    transform.Translate(-pathBounds.Width / 2.0f, -characterHeight);

                    characterPath.Transform(transform);

                    // Draw the character
                    g.FillPath(brush, characterPath);
                }

                if(characterIndex != text.Length - 1) {
                    // Move "currentCharacterRadians" on to the next character
                    double distanceToNextChar = (characterWidths[characterIndex] + characterWidths[characterIndex + 1]) / 2.0;
                    double charFractionOfCircumference = distanceToNextChar / circleCircumference;
                    if(clockwise)
                        currentCharacterRadians += charFractionOfCircumference * 2.0 * Math.PI;
                    else
                        currentCharacterRadians -= charFractionOfCircumference * 2.0 * Math.PI;
                }
            }
        }

        public static Bitmap Resize(this Bitmap bmp, Size newSize, bool mantainAspectRatio = false) {
            Rectangle srcRect = new Rectangle(Point.Empty, new Size(bmp.Width, bmp.Height));
            Rectangle trgRect;

            if(mantainAspectRatio) {
                double ar = srcRect.Width / (double)srcRect.Height;
                trgRect = new Rectangle(Point.Empty, new Size(newSize.Width, (int)(newSize.Height / ar)));
            } else
                trgRect = new Rectangle(Point.Empty, newSize);

            Bitmap newBmp = new Bitmap(trgRect.Width, trgRect.Height);

            using(Graphics g = Graphics.FromImage(newBmp)) {
                // FIXME: Shouldn't these be set by the caller?
                g.InterpolationMode = InterpolationMode.High;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.DrawImage(bmp, trgRect, srcRect, GraphicsUnit.Pixel);
            }

            return newBmp;
        }

        public static Bitmap Resize(this Bitmap bmp, int width, int height, bool mantainAspectRatio = false) {
            return bmp.Resize(new Size(width, height), mantainAspectRatio);
        }

        private static IEnumerable<float> GetCharacterWidths(Graphics g, string text, Font font) {
            // The length of a space. Necessary because a space measured using StringFormat.GenericTypographic has no width.
            // We can't use StringFormat.GenericDefault for the characters themselves, as it adds unwanted spacing.
            float spaceLength = g.MeasureString(" ", font, Point.Empty, StringFormat.GenericDefault).Width;

            return text.Select(c => c == ' ' ? spaceLength : g.MeasureString(c.ToString(), font, Point.Empty, StringFormat.GenericTypographic).Width);
        }

        public enum TernaryRasterOperations : uint {
            /// <summary>dest = source</summary>
            SrcCopy = 0xCC0020,
            /// <summary>dest = source OR dest</summary>
            SrcPaint = 0xEE0086,
            /// <summary>dest = source AND dest</summary>
            SrcAnd = 0x8800C6,
            /// <summary>dest = source XOR dest</summary>
            SrcInvert = 0x660046,
            /// <summary>dest = source AND (NOT dest)</summary>
            SrcErase = 0x440328,
            /// <summary>dest = (NOT source)</summary>
            NotSrcCopy = 0x330008,
            /// <summary>dest = (NOT src) AND (NOT dest)</summary>
            NotSrcErase = 0x1100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MergeCopy = 0xC000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MergePaint = 0xBB0226,
            /// <summary>dest = pattern</summary>
            PatCopy = 0xF00021,
            /// <summary>dest = DPSnoo</summary>
            PatPaint = 0xFB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PatInvert = 0x5A0049,
            /// <summary>dest = (NOT dest)</summary>
            DstInvert = 0x550009,
            /// <summary>dest = BLACK</summary>
            Blackness = 0x42,
            /// <summary>dest = WHITE</summary>
            Whiteness = 0xFF0062,
            /// <summary>
            /// Capture window as seen on screen. This includes layered windows 
            /// such as WPF windows with AllowsTransparency="true"
            /// </summary>
            CaptureBlt = 0x40000000
        }

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("Gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hRefDC);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, TernaryRasterOperations dwRop);
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public static void DrawImageFast(this Graphics g, Bitmap image, Rectangle dstRect, Rectangle srcRect, TernaryRasterOperations rasterOp = TernaryRasterOperations.SrcCopy) {
            Graphics srcGraphics = Graphics.FromImage(image);
            IntPtr srcHDC = srcGraphics.GetHdc();
            IntPtr pSource = CreateCompatibleDC(srcHDC);
            IntPtr dstHDC = g.GetHdc();
            IntPtr srcHbitmap = image.GetHbitmap();

            SelectObject(pSource, srcHbitmap);

            if(dstRect.Size == srcRect.Size)
                BitBlt(dstHDC, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, pSource, srcRect.X, srcRect.Y, rasterOp);
            else
                StretchBlt(dstHDC, dstRect.X, dstRect.Y, dstRect.Width, dstRect.Height, pSource, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, rasterOp);

            DeleteObject(srcHbitmap);
            DeleteDC(pSource);
            g.ReleaseHdc(dstHDC);
            srcGraphics.ReleaseHdc(srcHDC);
            srcGraphics.Dispose();
        }

        public static void DrawImageFast(this Graphics g, Bitmap image, Rectangle destRect, TernaryRasterOperations rasterOp = TernaryRasterOperations.SrcCopy) {
            g.DrawImageFast(image, destRect, new Rectangle(Point.Empty, image.Size), rasterOp);
        }

        public static void DrawImageFast(this Graphics g, Bitmap image, Point destPoint, TernaryRasterOperations rasterOp = TernaryRasterOperations.SrcCopy) {
            g.DrawImageFast(image, new Rectangle(destPoint, image.Size), new Rectangle(Point.Empty, image.Size), rasterOp);
        }

        public static FontMetrics GetFontMetrics(this Graphics graphics, Font font) {
            return FontMetricsImpl.GetFontMetrics(graphics, font);
        }

        public static Point GetCenter(this Rectangle r) {
            return new Point(r.Left + r.Width / 2, r.Top + r.Height / 2);
        }

        private class FontMetricsImpl : FontMetrics {
            [StructLayout(LayoutKind.Sequential)]
            public struct TEXTMETRIC {
                public int tmHeight;
                public int tmAscent;
                public int tmDescent;
                public int tmInternalLeading;
                public int tmExternalLeading;
                public int tmAveCharWidth;
                public int tmMaxCharWidth;
                public int tmWeight;
                public int tmOverhang;
                public int tmDigitizedAspectX;
                public int tmDigitizedAspectY;
                public char tmFirstChar;
                public char tmLastChar;
                public char tmDefaultChar;
                public char tmBreakChar;
                public byte tmItalic;
                public byte tmUnderlined;
                public byte tmStruckOut;
                public byte tmPitchAndFamily;
                public byte tmCharSet;
            }
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern bool GetTextMetrics(IntPtr hdc, TEXTMETRIC lptm);
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern bool DeleteObject(IntPtr hdc);
            private TEXTMETRIC GenerateTextMetrics(Graphics graphics, Font font) {
                IntPtr hDC = IntPtr.Zero;
                TEXTMETRIC textMetric = new TEXTMETRIC();
                IntPtr hFont = IntPtr.Zero;
                try {
                    hDC = graphics.GetHdc();
                    hFont = font.ToHfont();
                    IntPtr hFontDefault = SelectObject(hDC, hFont);
                    bool result = GetTextMetrics(hDC, textMetric);
                    SelectObject(hDC, hFontDefault);
                } finally {
                    if(hFont != IntPtr.Zero)
                        DeleteObject(hFont);
                    if(hDC != IntPtr.Zero)
                        graphics.ReleaseHdc(hDC);
                }
                return textMetric;
            }
            private TEXTMETRIC metrics;
            public override int Height {
                get {
                    return this.metrics.tmHeight;
                }
            }
            public override int Ascent {
                get {
                    return this.metrics.tmAscent;
                }
            }
            public override int Descent {
                get {
                    return this.metrics.tmDescent;
                }
            }
            public override int InternalLeading {
                get {
                    return this.metrics.tmInternalLeading;
                }
            }
            public override int ExternalLeading {
                get {
                    return this.metrics.tmExternalLeading;
                }
            }
            public override int AverageCharacterWidth {
                get {
                    return this.metrics.tmAveCharWidth;
                }
            }
            public override int MaximumCharacterWidth {
                get {
                    return this.metrics.tmMaxCharWidth;
                }
            }
            public override int Weight {
                get {
                    return this.metrics.tmWeight;
                }
            }
            public override int Overhang {
                get {
                    return this.metrics.tmOverhang;
                }
            }
            public override int DigitizedAspectX {
                get {
                    return this.metrics.tmDigitizedAspectX;
                }
            }
            public override int DigitizedAspectY {
                get {
                    return this.metrics.tmDigitizedAspectY;
                }
            }
            private FontMetricsImpl(Graphics graphics, Font font) {
                this.metrics = this.GenerateTextMetrics(graphics, font);
            }
            public static FontMetrics GetFontMetrics(Graphics graphics, Font font) {
                return new FontMetricsImpl(graphics, font);
            }
        }
    }

    public enum RectangleEdgeFilter {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public abstract class FontMetrics {
        public virtual int Height { get => 0; }
        public virtual int Ascent { get => 0; }
        public virtual int Descent { get => 0; }
        public virtual int InternalLeading { get => 0; }
        public virtual int ExternalLeading { get => 0; }
        public virtual int AverageCharacterWidth { get => 0; }
        public virtual int MaximumCharacterWidth { get => 0; }
        public virtual int Weight { get => 0; }
        public virtual int Overhang { get => 0; }
        public virtual int DigitizedAspectX { get => 0; }
        public virtual int DigitizedAspectY { get => 0; }
    }
}