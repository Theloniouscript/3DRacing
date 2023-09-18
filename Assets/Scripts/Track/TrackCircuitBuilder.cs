using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackCircuitBuilder : MonoBehaviour
{
    public static TrackPoint[] Build(Transform trackTransform, TrackType type)
    {
        TrackPoint[] points = new TrackPoint[trackTransform.childCount];

       ResetPoints(trackTransform, points);

       MakeLinks(points, type);

       MarkPoints(points, type);

        return points;

    }

    private static void ResetPoints(Transform trackTransform, TrackPoint[] points)
    {
        // проверка
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

            if (points[i] == null)
            {
                Debug.LogError("There's no TrackPoint script on one of the child objects");
                return;
            }

            points[i].Reset(); // сброс массива
        }
    }

    private static void MakeLinks(TrackPoint[] points, TrackType type)
    {
        // автоматизация сборки
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].Next = points[i + 1];
        }

        if (type == TrackType.Circular)
        {
            points[points.Length - 1].Next = points[0];
        }
    }

    private static void MarkPoints(TrackPoint[] points, TrackType type)
    {
        points[0].IsFirst = true;

        // определение последней точки
        if (type == TrackType.Sprint)
            points[points.Length - 1].IsLast = true;

        if (type == TrackType.Circular)
            points[0].IsLast = true;

    }
}
