using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    public float speed;
    Vector3 direction = Vector3.right;
    ICommand command;
    int bodySize = 0;
    [SerializeField] GameObject snakeBodyPrefab;

    private void Update()
    {
        if (LevelManager.Instance.IsGameOver()) {
            return;
        }

        HandleInput();

        // Creates a new command, has the head execute it, then save it on the command list
        command = new MoveCommand(transform, direction, speed);
        command.Execute();
        CommandManager.Instance.AddCommand(command);

        // deletes the commands that are too far gone compared to the snake body.
        var maxBufferSize = (bodySize + 1) * 300;
        if (CommandManager.Instance.GetCommandBufferSize() > maxBufferSize) {
            CommandManager.Instance.DeleteLastCommand();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.back) {
            direction = Vector3.forward;
        } else if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right) {
            direction = Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.forward) {
            direction = Vector3.back;
        } else if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left) {
            direction = Vector3.right;
        }
    }

    /// <summary>
    /// Handles Collision with apple (good); and also with body parts or wall (bad)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeBody") && other.gameObject.GetComponent<SnakeBodyController>().bodyPosition != 1) {
            GameOver();
        } else if (other.CompareTag("Apple")) {
            EatApple(other);
        }
    }

    /// <summary>
    /// If the snake eats an apple, a sound effect plays, the snake's body grows, the score is updated and the snake's speed increases.
    /// </summary>
    /// <param name="other"></param>
    private void EatApple(Collider other)
    {
        AppleEatenEffects(other);
        GrowSnakeBody();

        LevelManager.Instance.AddScore(50);
        speed += 0.5f;
    }

    /// <summary>
    /// Play sounds and moves the apple elsewhere
    /// </summary>
    /// <param name="other"></param>
    private static void AppleEatenEffects(Collider other)
    {
        var apple = other.gameObject.GetComponent<AppleController>();
        apple.PlaySound();
        apple.MoveApple();
    }
    
    private void GrowSnakeBody()
    {
        bodySize++;
        var snakeBody = Instantiate(snakeBodyPrefab);
        snakeBody.GetComponent<SnakeBodyController>().bodyPosition = bodySize;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameOver();
    }

    private void GameOver()
    {
        LevelManager.Instance.GameOver();
    }
}
