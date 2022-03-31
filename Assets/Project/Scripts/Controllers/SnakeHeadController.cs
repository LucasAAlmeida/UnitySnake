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

        command = new MoveCommand(transform, direction, speed);
        command.Execute();
        CommandManager.Instance.AddCommand(command);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeBody") && other.gameObject.GetComponent<SnakeBodyController>().bodyPosition != 1) {
            GameOver();
        } else if (other.CompareTag("Apple")) {
            EatApple(other);
        }
    }

    private void EatApple(Collider other)
    {
        AppleEatenEffects(other);
        GrowSnakeBody();

        LevelManager.Instance.AddScore(50);
        speed += 0.2f;
    }

    private void GrowSnakeBody()
    {
        bodySize++;
        var snakeBody = Instantiate(snakeBodyPrefab);
        snakeBody.GetComponent<SnakeBodyController>().bodyPosition = bodySize;
    }

    private static void AppleEatenEffects(Collider other)
    {
        var apple = other.gameObject.GetComponent<AppleController>();
        apple.PlaySound();
        apple.MoveApple();
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
