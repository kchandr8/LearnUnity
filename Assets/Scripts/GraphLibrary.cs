

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class GraphLibrary
{
    public static float ma;
    public static float mi;

    public delegate Vector3 Function(float x, float z, float t);

    public enum functionNames { wave, multiwave, sphere, flowysphere, torus }

    static Function[] functionList = { wave, multiwave, Sphere, FlowySphere, Torus };

    public static Function getIndex(functionNames name) {
        return functionList[(int)name];
    }
    
    public static Vector3 wave(float x, float z, float time) {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin((x + z + (time)) * Mathf.PI);
        p.z = z;
        return p;
    }

    public static Vector3 multiwave (float u, float v, float t) {
		Vector3 p;
		p.x = u;
		p.y = Mathf.Sin(Mathf.PI * (u + 0.5f * t));
		p.y += 0.5f * Mathf.Sin(2f * Mathf.PI * (v + t));
		p.y += Mathf.Sin(Mathf.PI * (u + v + 0.25f * t));
		p.y *= 1f / 2.5f;
        p.z = v;
		return p;
	}

    public static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float scale = 0.5f + 0.5f * Sin(PI * t);
        float r = scale * Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = r * Mathf.Sin( Mathf.PI * u );
        p.y = scale * Mathf.Sin(Mathf.PI * 0.5f * v); ;
        p.z = r * Mathf.Cos(Mathf.PI * u);
        return p;

    }

    public static Vector3 FlowySphere(float u, float v, float t)
    {
        Vector3 p;
        float scale = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float r = scale * Mathf.Cos(0.5f * Mathf.PI * v);
        p.x = r * Mathf.Sin(Mathf.PI * u);
        p.y = scale * Mathf.Sin(Mathf.PI * 0.5f * v); ;
        p.z = r * Mathf.Cos(Mathf.PI * u);
        return p;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        Vector3 p;
        //float scale = 1f;
        float major = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float minor = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        // major = ma;
        // minor = mi;
        float r = major + minor * Mathf.Cos(Mathf.PI * v);
        p.x = r * Mathf.Sin(Mathf.PI * u);
        p.y = minor * Mathf.Sin(Mathf.PI * v); ;
        p.z = r * Mathf.Cos(Mathf.PI * u);
        return p;

    }



}

