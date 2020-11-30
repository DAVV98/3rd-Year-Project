using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlanZucconi.AI.Steering
{
    public abstract class Steering
    {
        // Velocity towards t
        // speed = max speed
        public static Vector3 Seek (Vector3 p, Vector3 t, float speed = 1f)
        {
            Vector3 d = (t - p).normalized; // direction
            return d * speed;
        }
        public static Vector3 Flee (Vector3 p, Vector3 t, float speed = 1f)
        {
            Vector3 d = (p - t).normalized; // direction
            return d * speed;
        }

        // radius = deceleration radius
        public static Vector3 Arrive (Vector3 p, Vector3 t, float speed = 1f, float radius = 1f)
        {
            Vector3 d = (t - p).normalized; // direction

            // distance:    [0, radius]
            // coefficient: [0, 1     ] clamped
            float c = Mathf.Clamp01(Vector3.Distance(t, p) / radius);
            return d * speed * c;
        }

        // Predict position in advance
        // assuming linear movement at constant speed
        public static Vector3 Predict (Vector3 p, Vector3 v, float time = 1f)
        {
            return p + v * time;
        }

        // Seek
        // With prediction
        public static Vector3 Pursue (Vector3 p, Vector3 t, Vector3 v, float speed = 1f, float time = 1f)
        {
            Vector3 x = Predict(t, v, time); // Predicted position
            return Seek(p, x, speed);
        }

        // Force for target velocity
        public static Vector3 Force(Vector3 targetVelocity, Vector3 currentVelocity, float maxForce = 1f)
        {
            Vector3 deltaV = targetVelocity - currentVelocity;
            return Vector3.ClampMagnitude(deltaV, maxForce);
        }
    }
}