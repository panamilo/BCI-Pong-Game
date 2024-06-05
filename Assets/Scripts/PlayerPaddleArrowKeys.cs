using UnityEngine;

public class PlayerPaddleArrowKeys : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Detect arrow key input
        float moveInput = Input.GetAxis("Horizontal");

        // Move the paddle horizontally
        MovePaddle(moveInput);
    }

    void MovePaddle(float moveInput)
    {
        // Calculate movement amount based on input and speed
        float moveAmount = moveInput * moveSpeed * Time.deltaTime;

        // Move the paddle horizontally along the x-axis
        transform.Translate(new Vector2(moveAmount, 0f));
    }
}
