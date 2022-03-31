# Snake 3D

In this project, I wanted to use the command pattern to create a more 'fluid' snake game.
The result ended up rather smooth.

## Command Pattern

In each frame, the snake head stores its movement in a list of commands. Then, each body segment searches for the appropriate command to immitate, then does it.

### LevelManager.cs

A singleton monobehaviour that is responsible for 

- instantiating the first apple
- keeping track of the score
- keeping the game status (if the game is over)

### SnakeHeadController.cs

- Handles user input, storing in each frame the head position in a command list
- Deals with collision with the apple by triggering an SFX and growing 1 body segment
- Handles collision with one of its segments or the walls, prompting the game over

### SnakeBodyController.cs

- Knowing its position in the snake body, the segment uses that information to search for the appropriate head position in the command list, then immitates it
