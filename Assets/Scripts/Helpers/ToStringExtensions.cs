using UnityEngine;
using System.Text;
using System.Collections.Generic;

public static class ToStringExtensions
{
    public static string ToStringExt(this List<ContactPoint2D> contactPoints)
    {
        StringBuilder pointsStr = new StringBuilder("[");
		foreach(var point in contactPoints)
		{
			pointsStr.Append($"{point.ToStringExt()},");
		}
		pointsStr.Length -= 1;
		pointsStr.Append("]");
        return pointsStr.ToString();
    }
    public static string ToStringExt(this ContactPoint2D contactPoint)
    {
        return $"{contactPoint.point}";
    }
}