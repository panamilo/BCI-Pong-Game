using UnityEngine;

public class ComputerPaddle : Paddle
{
    public Rigidbody2D ball;
    public float ComputerPaddleSpeed = 8.0f;
    public float reactionDelay;
    private float reactionTimer = 0.0f;


    private void FixedUpdate() 
    {


        reactionTimer += Time.fixedDeltaTime;
        if(reactionTimer >= reactionDelay){
            if (this.ball.velocity.y > 0.0f)
            {
                if(this.ball.position.x > this.transform.position.x){
                    _rigidbody.AddForce(Vector2.right * ComputerPaddleSpeed);
                }else if (this.ball.position.x < this.transform.position.x){
                    _rigidbody.AddForce(Vector2.left * ComputerPaddleSpeed);
                }
            }
            else
            {
                if (this.transform.position.x > 0.0f){
                    _rigidbody.AddForce(Vector2.left * ComputerPaddleSpeed);
                }else if (this.transform.position.x < 0.0f){
                    _rigidbody.AddForce(Vector2.right * ComputerPaddleSpeed);
                }
            }
        }
    }
}
