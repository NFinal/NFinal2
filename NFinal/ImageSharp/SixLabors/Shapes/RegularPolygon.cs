﻿// <copyright file="RegularPolygon.cs" company="Scott Williams">
// Copyright (c) Scott Williams and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.Shapes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// A shape made up of a single path made up of one of more <see cref="ILineSegment"/>s
    /// </summary>
    public class RegularPolygon : Polygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegularPolygon" /> class.
        /// </summary>
        /// <param name="location">The location the center of the polygon will be placed.</param>
        /// <param name="verticies">The number of verticies the <see cref="RegularPolygon"/> should have.</param>
        /// <param name="radius">The radius of the circle that would touch all verticies.</param>
        /// <param name="angle">The angle of rotation in Radians</param>
        public RegularPolygon(Vector2 location, int verticies, float radius, float angle)
            : base(CreateSegment(location, radius, verticies, angle))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularPolygon" /> class.
        /// </summary>
        /// <param name="location">The location the center of the polygon will be placed.</param>
        /// <param name="radius">The radius of the circle that would touch all verticies.</param>
        /// <param name="verticies">The number of verticies the <see cref="RegularPolygon"/> should have.</param>
        public RegularPolygon(Vector2 location, int verticies, float radius)
            : this(location, verticies, radius, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularPolygon" /> class.
        /// </summary>
        /// <param name="x">The X coordinate of the center of the polygon.</param>
        /// <param name="y">The Y coordinate of the center of the polygon.</param>
        /// <param name="verticies">The number of verticies the <see cref="RegularPolygon" /> should have.</param>
        /// <param name="radius">The radius of the circle that would touch all verticies.</param>
        /// <param name="angle">The angle of rotation in Radians</param>
        public RegularPolygon(float x, float y, int verticies, float radius, float angle)
            : this(new Vector2(x, y), verticies, radius, angle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegularPolygon" /> class.
        /// </summary>
        /// <param name="x">The X coordinate of the center of the polygon.</param>
        /// <param name="y">The Y coordinate of the center of the polygon.</param>
        /// <param name="radius">The radius of the circle that would touch all verticies.</param>
        /// <param name="verticies">The number of verticies the <see cref="RegularPolygon"/> should have.</param>
        public RegularPolygon(float x, float y, int verticies, float radius)
            : this(new Vector2(x, y), verticies, radius)
        {
        }

        private static LinearLineSegment CreateSegment(Vector2 location, float radius, int verticies, float angle)
        {
            Guard.MustBeGreaterThan(verticies, 2, nameof(verticies));
            Guard.MustBeGreaterThan(radius, 0, nameof(radius));

            Vector2 distanceVector = new Vector2(0, radius);

            float anglePerSegemnts = (float)((2 * Math.PI) / verticies);
            float current = angle;
            Vector2[] points = new Vector2[verticies];
            for (var i = 0; i < verticies; i++)
            {
                Vector2 rotated = Vector2.Transform(distanceVector, Matrix3x2.CreateRotation(current));

                points[i] = rotated + location;

                current += anglePerSegemnts;
            }

            return new LinearLineSegment(points);
        }
    }
}
