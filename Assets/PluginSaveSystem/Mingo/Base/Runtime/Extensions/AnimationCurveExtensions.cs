﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mingo.Base.Runtime.Extensions
{
  public static class AnimationCurveExtensions
  {
    /// <summary>
    /// Find first derivative of curve at point x
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="x"></param>
    /// <returns>Slope of curve at point x as float</returns>
    public static float Differentiate(this AnimationCurve curve, float x)
    {
      return curve.Differentiate(x, curve.keys.First().time, curve.keys.Last().time);
    }

    const float Delta = .000001f;

    public static float Differentiate(this AnimationCurve curve, float x, float xMin, float xMax)
    {
      var x1 = Mathf.Max(xMin, x - Delta);
      var x2 = Mathf.Min(xMax, x + Delta);
      var y1 = curve.Evaluate(x1);
      var y2 = curve.Evaluate(x2);

      return (y2 - y1) / (x2 - x1);
    }


    static IEnumerable<float> GetPointSlopes(AnimationCurve curve, int resolution)
    {
      for (var i = 0; i < resolution; i++)
      {
        yield return curve.Differentiate((float) i / resolution);
      }
    }

    public static AnimationCurve Derivative(this AnimationCurve curve, int resolution = 100, float smoothing = .05f)
    {
      var slopes = GetPointSlopes(curve, resolution).ToArray();

      var curvePoints = slopes
        .Select((s, i) => new Vector2((float) i / resolution, s))
        .ToList();

      var simplifiedCurvePoints = new List<Vector2>();

      if (smoothing > 0)
      {
        LineUtility.Simplify(curvePoints, smoothing, simplifiedCurvePoints);
      }
      else
      {
        simplifiedCurvePoints = curvePoints;
      }

      var derivative = new AnimationCurve(
        simplifiedCurvePoints.Select(v => new Keyframe(v.x, v.y)).ToArray());

      for (int i = 0, len = derivative.keys.Length; i < len; i++)
      {
        derivative.SmoothTangents(i, 0);
      }

      return derivative;
    }

    /// <summary>
    /// Find integral of curve between xStart and xEnd using the trapezoidal rule
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="xStart"></param>
    /// <param name="xEnd"></param>
    /// <param name="sliceCount">Resolution of calculation. Increase for better
    /// precision, at cost of computation</param>
    /// <returns>Area under the curve between xStart and xEnd as float</returns>
    public static float Integrate(this AnimationCurve curve, float xStart = 0f, float xEnd = 1f, int sliceCount = 100)
    {
      var sliceWidth = (xEnd - xStart) / sliceCount;
      var accumulatedTotal = (curve.Evaluate(xStart) + curve.Evaluate(xEnd)) / 2;

      for (var i = 1; i < sliceCount; i++)
      {
        accumulatedTotal += curve.Evaluate(xStart + i * sliceWidth);
      }

      return sliceWidth * accumulatedTotal;
    }
  }
}