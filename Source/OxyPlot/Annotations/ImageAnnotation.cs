﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageAnnotation.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Represents an annotation that shows an image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Annotations
{
    using System;

    /// <summary>
    /// Represents an annotation that shows an image.
    /// </summary>
    public class ImageAnnotation : Annotation
    {
        /// <summary>
        /// The actual bounds of the rendered image.
        /// </summary>
        private OxyRect actualBounds;

        /// <summary>
        /// The bounds defined by the X, Y, Width, and Height properties.
        /// </summary>
        private OxyRect fullBounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAnnotation" /> class.
        /// </summary>
        public ImageAnnotation()
        {
            this.X = new PlotLength(0.5, PlotLengthUnit.RelativeToPlotArea);
            this.Y = new PlotLength(0.5, PlotLengthUnit.RelativeToPlotArea);
            this.OffsetX = new PlotLength(0, PlotLengthUnit.ScreenUnits);
            this.OffsetY = new PlotLength(0, PlotLengthUnit.ScreenUnits);
            this.Width = new PlotLength(double.NaN, PlotLengthUnit.ScreenUnits);
            this.Height = new PlotLength(double.NaN, PlotLengthUnit.ScreenUnits);
            this.Opacity = 1.0;
            this.Interpolate = true;
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Middle;
            this.StretchToBoundingBox = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAnnotation" /> class.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="position">The position in screen coordinates.</param>
        /// <param name="horizontalAlignment">The horizontal alignment.</param>
        /// <param name="verticalAlignment">The vertical alignment.</param>
        [Obsolete]
        public ImageAnnotation(
            OxyImage image,
            ScreenPoint position,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Middle)
            : this()
        {
            this.ImageSource = image;
            this.X = new PlotLength(position.X, PlotLengthUnit.ScreenUnits);
            this.Y = new PlotLength(position.Y, PlotLengthUnit.ScreenUnits);
            this.HorizontalAlignment = horizontalAlignment;
            this.VerticalAlignment = verticalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAnnotation" /> class.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="position">The position in data coordinates.</param>
        /// <param name="horizontalAlignment">The horizontal alignment.</param>
        /// <param name="verticalAlignment">The vertical alignment.</param>
        [Obsolete]
        public ImageAnnotation(
            OxyImage image,
            DataPoint position,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Middle)
            : this()
        {
            this.ImageSource = image;
            this.X = new PlotLength(position.X, PlotLengthUnit.Data);
            this.Y = new PlotLength(position.Y, PlotLengthUnit.Data);
            this.HorizontalAlignment = horizontalAlignment;
            this.VerticalAlignment = verticalAlignment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAnnotation" /> class.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="relativeX">The x-coordinate relative to the plot area (0-1).</param>
        /// <param name="relativeY">The y-coordinate relative to the plot area (0-1).</param>
        /// <param name="horizontalAlignment">The horizontal alignment.</param>
        /// <param name="verticalAlignment">The vertical alignment.</param>
        [Obsolete]
        public ImageAnnotation(
            OxyImage image,
            double relativeX,
            double relativeY,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Middle)
            : this()
        {
            this.ImageSource = image;
            this.X = new PlotLength(relativeX, PlotLengthUnit.RelativeToPlotArea);
            this.Y = new PlotLength(relativeY, PlotLengthUnit.RelativeToPlotArea);
            this.HorizontalAlignment = horizontalAlignment;
            this.VerticalAlignment = verticalAlignment;
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>The image source.</value>
        public OxyImage ImageSource { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment.
        /// </summary>
        /// <value>The horizontal alignment.</value>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the X position of the image.
        /// </summary>
        /// <value>The X.</value>
        public PlotLength X { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the image.
        /// </summary>
        /// <value>The Y.</value>
        public PlotLength Y { get; set; }

        /// <summary>
        /// Gets or sets the X offset.
        /// </summary>
        /// <value>The offset X.</value>
        public PlotLength OffsetX { get; set; }

        /// <summary>
        /// Gets or sets the Y offset.
        /// </summary>
        /// <value>The offset Y.</value>
        public PlotLength OffsetY { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public PlotLength Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public PlotLength Height { get; set; }

        /// <summary>
        /// Gets or sets the opacity (0-1).
        /// </summary>
        /// <value>The opacity value.</value>
        public double Opacity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to apply smooth interpolation to the image.
        /// </summary>
        /// <value><c>true</c> if the image should be interpolated (using a high-quality bi-cubic interpolation); <c>false</c> if the nearest neighbor should be used.</value>
        public bool Interpolate { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment.
        /// </summary>
        /// <value>The vertical alignment.</value>
        public VerticalAlignment VerticalAlignment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image should be stretch to fit
        /// into the bounding box defined by the Width and Height properties.
        /// </summary>
        public bool StretchToBoundingBox { get; set; }

        /// <summary>
        /// Gets the bounds of the rendered image's rectangle.
        /// </summary>
        protected OxyRect ImageBounds
        {
            get { return this.actualBounds; }
        }

        /// <summary>
        /// Gets the bounds full bounds of the annotation (without stretch logic applied).
        /// </summary>
        protected OxyRect FullBounds
        {
            get { return this.fullBounds; }
        }

        /// <summary>
        /// Renders the image annotation.
        /// </summary>
        /// <param name="rc">The render context.</param>
        /// <param name="model">The plot model.</param>
        public override void Render(IRenderContext rc, PlotModel model)
        {
            base.Render(rc, model);

            var p = this.GetPoint(this.X, this.Y, rc, model);
            var o = this.GetVector(this.OffsetX, this.OffsetY, rc, model);
            var position = p + o;

            var clippingRectangle = this.GetClippingRect();

            var s = this.GetVector(this.Width, this.Height, rc, model);

            var width = s.X;
            var height = s.Y;

            if (double.IsNaN(width) && double.IsNaN(height))
            {
                width = this.ImageSource.Width;
                height = this.ImageSource.Height;
            }

            if (double.IsNaN(width))
            {
                width = height / this.ImageSource.Height * this.ImageSource.Width;
            }

            if (double.IsNaN(height))
            {
                height = width / this.ImageSource.Width * this.ImageSource.Height;
            }

            width = Math.Abs(width);
            height = Math.Abs(height);

            double x = position.X;
            double y = position.Y;

            if (this.HorizontalAlignment == HorizontalAlignment.Center)
            {
                x -= width * 0.5;
            }

            if (this.HorizontalAlignment == HorizontalAlignment.Right)
            {
                x -= width;
            }

            if (this.VerticalAlignment == VerticalAlignment.Middle)
            {
                y -= height * 0.5;
            }

            if (this.VerticalAlignment == VerticalAlignment.Bottom)
            {
                y -= height;
            }

            this.fullBounds = new OxyRect(x, y, width, height);

            if (!this.StretchToBoundingBox)
            {
                double scaleX = width / this.ImageSource.Width;
                double scaleY = height / this.ImageSource.Height;
                double minScale = scaleX < scaleY ? scaleX : scaleY;

                double newWidth = minScale * this.ImageSource.Width;
                double newHeight = minScale * this.ImageSource.Height;

                double widthDiff = width - newWidth;
                double heightDiff = height - newHeight;

                width = newWidth;
                height = newHeight;
                x += widthDiff / 2;
                y += heightDiff / 2;
            }

            this.actualBounds = new OxyRect(x, y, width, height);

            if (this.X.Unit == PlotLengthUnit.Data || this.Y.Unit == PlotLengthUnit.Data)
            {
                rc.DrawClippedImage(clippingRectangle, this.ImageSource, x, y, width, height, this.Opacity, this.Interpolate);
            }
            else
            {
                rc.DrawImage(this.ImageSource, x, y, width, height, this.Opacity, this.Interpolate);
            }
        }

        /// <summary>
        /// When overridden in a derived class, tests if the plot element is hit by the specified point.
        /// </summary>
        /// <param name="args">The hit test arguments.</param>
        /// <returns>
        /// The result of the hit test.
        /// </returns>
        protected override HitTestResult HitTestOverride(HitTestArguments args)
        {
            if (this.actualBounds.Contains(args.Point))
            {
                return new HitTestResult(this, args.Point);
            }

            return null;
        }

        /// <summary>
        /// Gets the point.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="rc">The render context.</param>
        /// <param name="model">The model.</param>
        /// <returns>The point in screen coordinates.</returns>
        protected ScreenPoint GetPoint(PlotLength x, PlotLength y, IRenderContext rc, PlotModel model)
        {
            if (x.Unit == PlotLengthUnit.Data || y.Unit == PlotLengthUnit.Data)
            {
                return this.XAxis.Transform(x.Value, y.Value, this.YAxis);
            }

            double sx;
            double sy;
            switch (x.Unit)
            {
                case PlotLengthUnit.RelativeToPlotArea:
                    sx = model.PlotArea.Left + (model.PlotArea.Width * x.Value);
                    break;
                case PlotLengthUnit.RelativeToViewport:
                    sx = model.Width * x.Value;
                    break;
                default:
                    sx = x.Value;
                    break;
            }

            switch (y.Unit)
            {
                case PlotLengthUnit.RelativeToPlotArea:
                    sy = model.PlotArea.Top + (model.PlotArea.Height * y.Value);
                    break;
                case PlotLengthUnit.RelativeToViewport:
                    sy = model.Height * y.Value;
                    break;
                default:
                    sy = y.Value;
                    break;
            }

            return new ScreenPoint(sx, sy);
        }

        /// <summary>
        /// Gets the vector.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="rc">The render context.</param>
        /// <param name="model">The model.</param>
        /// <returns>The vector in screen coordinates.</returns>
        protected ScreenVector GetVector(PlotLength x, PlotLength y, IRenderContext rc, PlotModel model)
        {
            double sx;
            double sy;

            switch (x.Unit)
            {
                case PlotLengthUnit.Data:
                    sx = this.XAxis.Transform(x.Value) - this.XAxis.Transform(0);
                    break;
                case PlotLengthUnit.RelativeToPlotArea:
                    sx = model.PlotArea.Width * x.Value;
                    break;
                case PlotLengthUnit.RelativeToViewport:
                    sx = model.Width * x.Value;
                    break;
                default:
                    sx = x.Value;
                    break;
            }

            switch (y.Unit)
            {
                case PlotLengthUnit.Data:
                    sy = -this.YAxis.Transform(y.Value) + this.YAxis.Transform(0);
                    break;
                case PlotLengthUnit.RelativeToPlotArea:
                    sy = model.PlotArea.Height * y.Value;
                    break;
                case PlotLengthUnit.RelativeToViewport:
                    sy = model.Height * y.Value;
                    break;
                default:
                    sy = y.Value;
                    break;
            }

            return new ScreenVector(sx, sy);
        }
    }
}