using NUnit.Framework;
using UnityEngine;

// tests made with the help of chatgpt
public class PlayerMovementEditModeTests
{
    private GameObject playerObject;
    private PlayerMovement playerMovement;
    private CharacterController characterController;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject for the player and add necessary components
        playerObject = new GameObject();
        characterController = playerObject.AddComponent<CharacterController>();
        playerMovement = playerObject.AddComponent<PlayerMovement>();

        // Initialize CharacterController to prevent errors
        characterController.height = 2.0f;
        characterController.center = new Vector3(0, 1, 0);

        // Disable gravity and physics for isolated testing
        characterController.detectCollisions = false;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the player GameObject after each test
        GameObject.DestroyImmediate(playerObject);
    }

    [Test]
    public void PlayerMovement_Can_Set_Speed()
    {
        // Arrange
        float newSpeed = 12.0f;

        // Act
        playerMovement.setSpeed(newSpeed);

        // Assert
        Assert.AreEqual(newSpeed, playerMovement.runningSpeed);
    }

    [Test]
    public void PlayerMovement_Slows_Down_And_Resets_Speed()
    {
        // Arrange
        float initialSpeed = 10.0f;
        float slowdownSpeed = 5.0f;
        playerMovement.setSpeed(initialSpeed);
        playerMovement.slowdownSpeed = slowdownSpeed;

        // Act
        playerMovement.slowdown();

        // Assert initial slowdown
        Assert.AreEqual(slowdownSpeed, playerMovement.runningSpeed);

        // Simulate waiting for the slowdown to wear off
        System.Threading.Tasks.Task.Delay(11000).Wait(); // Wait for more than the slowdown period (10 seconds)

        // Assert speed is reset after slowdown period
        Assert.AreEqual(initialSpeed, playerMovement.runningSpeed);
    }

}
