using UnityEngine;

// Draws a catmull-rom spline in the scene view,
// along the child objects of the transform of this component
public class CatmullRomSpline : MonoBehaviour
{
    [Range(0, 1)] public float alpha = 0.5f;
    int PointCount => transform.childCount;
    int SegmentCount => PointCount - 3;

    Vector3 GetPoint(int i)
    {
        var pos = transform.GetChild(i).position;
        pos.y = 1;
        return pos;  
    } 

    CatmullRomCurve GetCurve(int i)
    {
        return new CatmullRomCurve(GetPoint(i), GetPoint(i + 1), GetPoint(i + 2), GetPoint(i + 3), alpha);
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < SegmentCount; i++)
            DrawCurveSegment(GetCurve(i));
    }

    void DrawCurveSegment(CatmullRomCurve curve)
    {
        const int detail = 32;
        Vector3 prev = curve.p1;

        for (int i = 1; i < detail; i++)
        {
            float t = i / (detail - 1f);
            Vector3 pt = curve.GetPoint(t);
            Gizmos.DrawLine(prev, pt);
            prev = pt;
        }
    }
}

public struct CatmullRomCurve
{
    public Vector3 p0, p1, p2, p3;
    public float alpha;

    public CatmullRomCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float alpha)
    {
        (this.p0, this.p1, this.p2, this.p3) = (p0, p1, p2, p3);
        this.alpha = alpha;
    }

    // Evaluates a point at the given t-value from 0 to 1
    public Vector3 GetPoint(float t)
    {
        // calculate knots
        const float k0 = 0;
        float k1 = GetKnotInterval(p0, p1);
        float k2 = GetKnotInterval(p1, p2) + k1;
        float k3 = GetKnotInterval(p2, p3) + k2;

        // evaluate the point
        float u = Mathf.LerpUnclamped(k1, k2, t);
        Vector3 A1 = Remap(k0, k1, p0, p1, u);
        Vector3 A2 = Remap(k1, k2, p1, p2, u);
        Vector3 A3 = Remap(k2, k3, p2, p3, u);
        Vector3 B1 = Remap(k0, k2, A1, A2, u);
        Vector3 B2 = Remap(k1, k3, A2, A3, u);
        return Remap(k1, k2, B1, B2, u);
    }

    static Vector3 Remap(float a, float b, Vector3 c, Vector3 d, float u)
    {
        return Vector3.LerpUnclamped(c, d, (u - a) / (b - a));
    }

    float GetKnotInterval(Vector3 a, Vector3 b)
    {
        return Mathf.Pow(Vector3.SqrMagnitude(a - b), 0.5f * alpha);
    }
}