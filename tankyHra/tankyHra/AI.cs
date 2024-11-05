namespace tankyHra;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class AI
{
    public Vector2 Position;
    private Vector2 targetPoint;
    private float speed;

    public AI(Vector2 startPosition, float speed)
    {
        this.Position = startPosition;
        this.speed = speed;
        targetPoint = startPosition;
    }

    public void Update(List<Vector2> points)
    {
        // Move towards the target point
        Vector2 direction = targetPoint - Position;
        if (direction.Length() < speed)
        {
            Position = targetPoint;
            targetPoint = GetRandomPoint(points);
        }
        else
        {
            direction.Normalize();
            Position += direction * speed;
        }
    }

    private Vector2 GetRandomPoint(List<Vector2> points)
    {
        var random = new Random();
        int index = random.Next(points.Count);
        return points[index];
    }
}
