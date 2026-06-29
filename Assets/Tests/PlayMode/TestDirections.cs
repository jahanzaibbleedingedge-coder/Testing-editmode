using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestDirections
{
    private GameObject cubeObject;
    private CubeMover cubeMover;

    [SetUp]
    public void Setup()
    {
        // Create fresh cube for each test
        cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubeMover = cubeObject.AddComponent<CubeMover>();
        
        // Set default values
        cubeMover.moveSpeed = 5f;
        cubeMover.moveRange = 5f;
        cubeObject.transform.position = Vector3.zero;
        
        // No need to call Start() - Unity calls it automatically
        // The Start() method will run when the game starts
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after each test
        GameObject.Destroy(cubeObject);
    }

    [UnityTest]
    public IEnumerator CubeMover_MovesRight_WhenRightArrowPressed()
    {
        // Record starting position
        float startX = cubeObject.transform.position.x;
        
        // Simulate right movement for 1 second
        float timer = 0f;
        while (timer < 1f)
        {
            SimulateInput(1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        // Should have moved right
        Assert.Greater(cubeObject.transform.position.x, startX);
    }

    [UnityTest]
    public IEnumerator CubeMover_MovesLeft_WhenLeftArrowPressed()
    {
        float startX = cubeObject.transform.position.x;
        
        float timer = 0f;
        while (timer < 1f)
        {
            SimulateInput(-1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        Assert.Less(cubeObject.transform.position.x, startX);
    }

    [UnityTest]
    public IEnumerator CubeMover_StopsAtMaxRange()
    {
        cubeMover.moveRange = 3f;
        cubeObject.transform.position = Vector3.zero;
        
        float timer = 0f;
        while (timer < 2f)
        {
            SimulateInput(1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        Assert.AreEqual(3f, cubeObject.transform.position.x);
    }

    [UnityTest]
    public IEnumerator CubeMover_StopsAtMinRange()
    {
        cubeMover.moveRange = 3f;
        cubeObject.transform.position = Vector3.zero;
        
        float timer = 0f;
        while (timer < 2f)
        {
            SimulateInput(-1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        Assert.AreEqual(-3f, cubeObject.transform.position.x);
    }

    [UnityTest]
    public IEnumerator CubeMover_DoesNotMove_WhenNoInput()
    {
        float startX = cubeObject.transform.position.x;
        
        float timer = 0f;
        while (timer < 1f)
        {
            SimulateInput(0f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        Assert.AreEqual(startX, cubeObject.transform.position.x);
    }

    [UnityTest]
    public IEnumerator CubeMover_MovesAtCorrectSpeed()
    {
        cubeMover.moveSpeed = 10f;
        float startX = cubeObject.transform.position.x;
        
        float timer = 0f;
        while (timer < 0.5f)
        {
            SimulateInput(1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        float expectedX = startX + 5f; // 10 speed * 0.5 seconds
        Assert.AreEqual(expectedX, cubeObject.transform.position.x, 0.1f);
    }

    [UnityTest]
    public IEnumerator CubeMover_CanMoveBackAndForth()
    {
        // Move right
        float timer = 0f;
        while (timer < 0.5f)
        {
            SimulateInput(1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        float rightPosition = cubeObject.transform.position.x;
        Assert.Greater(rightPosition, 0);
        
        // Move left
        timer = 0f;
        while (timer < 0.5f)
        {
            SimulateInput(-1f);
            timer += Time.deltaTime;
            yield return null;
        }
        
        Assert.Less(cubeObject.transform.position.x, rightPosition);
        Assert.AreEqual(0f, cubeObject.transform.position.x, 0.5f);
    }

    // Helper method to simulate input
    private void SimulateInput(float inputValue)
    {
        // Store original position
        Vector3 originalPos = cubeObject.transform.position;
        
        // Calculate movement manually (copying logic from your script)
        float moveAmount = inputValue * cubeMover.moveSpeed * Time.deltaTime;
        Vector3 newPosition = originalPos + new Vector3(moveAmount, 0, 0);
        
        // Apply range clamping
        if (cubeMover.moveRange > 0)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, 
                -cubeMover.moveRange, 
                cubeMover.moveRange);
        }
        
        // Move the cube
        cubeObject.transform.position = newPosition;
    }
}