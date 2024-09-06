using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float dashSpeed = 25.0f;   // Speed during dash
    private float dashDuration = 0.2f; // Duration of dash in seconds
    private float dashCooldown = 1.0f; // Cooldown between dashes
    private bool isDashing = false;
    private float dashTime = 0f;
    private float dashCooldownTimer = 0f;

    void Start()
    {
    }

    void Update()
    {
        // Handle dash cooldown
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Look at mouse
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;

        // Move...
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        Vector3 inputNormalized = new Vector3(inputHorizontal, inputVertical, 0).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartCoroutine(Dash(inputNormalized));
        }

        if (!isDashing)
        {
            // Regular movement if not dashing
            transform.position += speed * Time.deltaTime * inputNormalized;
        }
    }

    private IEnumerator Dash(Vector3 direction)
    {
        isDashing = true;
        dashTime = dashDuration;
        dashCooldownTimer = dashCooldown; // Start cooldown

        // Dashing movement
        while (dashTime > 0)
        {
            transform.position += dashSpeed * Time.deltaTime * direction;
            dashTime -= Time.deltaTime;
            yield return null; // Wait until next frame
        }

        isDashing = false; // End dash
    }
}
