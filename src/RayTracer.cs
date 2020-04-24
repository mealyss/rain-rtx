using System;
using System.Drawing;
using System.Numerics;
using RainRTX;

public enum IntersectionType : byte
{
    None = 0,
    Ground = 1,
    Sphere = 2,
}

public static partial class GraphicCore
{
    private static RayHit TraceRay(Ray ray)
    {
        float closest_t = float.PositiveInfinity;
        Sphere closest_sphere = Sphere.Null;
        var intersection = IntersectionType.None;

        if (Scene.Spheres == null) goto SkipShereIntersection;

        for (var i = 0; i < Scene.Spheres.Length; i++)
        {
            var ts = IntersectRaySphere(ray, i);
            if (ts.X < closest_t && ts.X > 0 && ts.X < float.PositiveInfinity)
            {
                closest_t = ts.X;
                closest_sphere = Scene.Spheres[i];
                intersection = IntersectionType.Sphere;
            }
            if (ts.Y < closest_t && ts.Y > 0 && ts.Y < float.PositiveInfinity)
            {
                closest_t = ts.Y;
                closest_sphere = Scene.Spheres[i];
                intersection = IntersectionType.Sphere;
            }
        }
    SkipShereIntersection:

        if (Scene.Ground == null) goto SkipGroundIntersection;
        float tg = IntersectRayGround(ray);
        if (tg > 0 && tg < closest_t)
        {
            closest_t = tg;
            intersection = IntersectionType.Ground;
        }
    SkipGroundIntersection:

        if (intersection == IntersectionType.Sphere)
        {
            Vector3 point = ray.origin + ray.direction * closest_t;
            Vector3 normal = (point - closest_sphere.center);
            normal /= normal.Length();
            return new RayHit
            {
                intersection = IntersectionType.Sphere,
                normal = normal,
                position = point,
                specular_vec = closest_sphere.specular,
                albedo = closest_sphere.albedo
            };
        }

        if (intersection == IntersectionType.Ground)
        {
            Vector3 point = ray.origin + ray.direction * closest_t;
            return new RayHit
            {
                intersection = IntersectionType.Ground,
                specular_vec = Scene.Ground.specular_vec,
                albedo = Scene.Ground.albedo,
                normal = new Vector3(0f, 1f, 0f),
                position = point
            };
        }
        return new RayHit { intersection = IntersectionType.None };
    }

    private static Vector2 IntersectRaySphere(Ray ray, int sphereIndex)
    {
        var sphere = Scene.Spheres[sphereIndex];
        Vector3 oc = ray.origin - sphere.center;

        float k1 = Vector3.Dot(ray.direction, ray.direction);
        float k2 = 2 * Vector3.Dot(oc, ray.direction);
        float k3 = Vector3.Dot(oc,oc) - sphere.radius * sphere.radius;

        float discriminant = k2 * k2 - 4 * k1 * k3;
        if (discriminant < 0) return new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        float t1 = (float)(-k2 + Math.Sqrt(discriminant)) / (2 * k1);
        float t2 = (float)(-k2 - Math.Sqrt(discriminant)) / (2 * k1);
        return new Vector2(t1, t2);
    }

    private static float IntersectRayGround(Ray ray)
    {
        return -ray.origin.Y / ray.direction.Y;
    }

    private static Color24 Shade(ref Ray ray, RayHit hit)
    {
        if (hit.intersection != IntersectionType.None)
        {
           Color24 specular = hit.specular_vec;

           ray.origin = hit.position + hit.normal * 0.001f;
           ray.direction = Vector3.Reflect(ray.direction, hit.normal);
           ray.energy *= specular;
        
           Ray shadowRay =  new Ray
           {
                origin =  hit.position + hit.normal * 0.001f,
                direction = -Scene.directionalLights[0].direction,
                energy = Color24.Black
           };
           var rayHit = TraceRay(shadowRay);
           if (rayHit.intersection != IntersectionType.None)
                return Color24.Black;

           var sat =  Mathf.Saturate(-Vector3.Dot(hit.normal, Vector3.Normalize(Scene.directionalLights[0].direction)));
           return sat * hit.albedo;
        }
        ray.energy = Color24.Black;
        if (Scene.SkyBox != null)
            return Scene.SkyBox.SampleFromCamera(ray.direction);
        return Scene.BackgroundColor;
    }

}

