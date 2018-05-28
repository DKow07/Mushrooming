using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle  
{
    public Vector3 position;
    public float radius;

    public Circle(Vector3 pos, float r)
    {
        this.position = pos;
        this.radius = r;
    }
	
    public bool IntersectCircles(Circle other)
    {

        float x1 = position.x;
        float z1 = position.z;
        float x2 = other.position.x;
        float z2 = other.position.z;

        float squareDistance = (x2 - x1) * (x2 - x1) + (z2 - z1) * (z2 - z1); 
        float distance = Mathf.Sqrt(squareDistance);

        if(distance == this.radius + other.radius)
        {
            return true;
        }


        if(radius + other.radius > distance && distance > Mathf.Abs(this.radius - other.radius))
        {

            float delta = (float) 0.25 * Mathf.Sqrt((distance + this.radius + other.radius)*(distance + this.radius - other.radius)*(distance - this.radius + other.radius)*(-distance + this.radius + other.radius));

            float x11 = ((x2 + x1) / 2) + ((x2 - x1) * ((this.radius * this.radius - other.radius * other.radius)) / (2 * distance * distance)) + 2 * ((z1 - z2) / (distance * distance)) * delta;
            float x22 = ((x2 + x1) / 2) + ((x2 - x1) * ((this.radius * this.radius - other.radius * other.radius)) / (2 * distance * distance)) - 2 * ((z1 - z2) / (distance * distance)) * delta;
            float z11 = ((z1 + z2) / 2) + ((z2 - z1) * ((this.radius * this.radius - other.radius * other.radius)) / (2 * distance * distance)) - 2 * ((x1 - x2) / (distance * distance)) * delta;
            float z22 = ((z1 + z2) / 2) + ((z2 - z1) * ((this.radius * this.radius - other.radius * other.radius)) / (2 * distance * distance)) + 2 * ((x1 - x2) / (distance * distance)) * delta;
            
            return true;
        }

        if ((position - other.position).magnitude < radius || (position - other.position).magnitude < other.radius)
            return true;



        return false;
    }


}
