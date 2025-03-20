using System.Collections;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D body;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public IEnumerator DoDash(float dashDirection)
    {
        if (isDashing) yield break; // Prevents multiple dashes at once
        if (dashDirection == 0) yield break; // No direction? No dash.

        isDashing = true;

        // Apply dash as an impulse
        body.AddForce(new Vector2(dashSpeed, body.linearVelocityY), ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        // Reduce dash effect without affecting vertical movement (if jumping)
        body.linearVelocity = new Vector2(body.linearVelocity.x * 0.5f, body.linearVelocity.y);
        isDashing = false;
    }
}
