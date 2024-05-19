using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    [SerializeField] private GameObject connectedFloor;
    [SerializeField] private Transform ikController;
    [SerializeField] private float animationStartOffset;

    private float minX, maxX, originalX;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        originalX = ikController.transform.localPosition.x;
        minX = originalX < 0 ? -12f : 0.0f;
        maxX = originalX < 0 ? 0.0f : 12f;

        animator.speed = 0.25f;
        animator.Play("PlatformAnimation", 0, animationStartOffset);
    }

    public void RandomizePath()
    {
        StartCoroutine(MoveIK(Random.Range(minX, maxX), Random.Range(0f,2f)));
    }

    public void ResetPath()
    {
        StartCoroutine(MoveIK(originalX, 0));
    }

    public void CycleFloorActive()
    {
        connectedFloor.SetActive(true);
        connectedFloor.GetComponent<ChangePlatformLights>().ChangeToBlue();
    }

    public void CycleFloorInactive()
    {
        connectedFloor.SetActive(false);
    }

    IEnumerator MoveIK(float xOffset, float yOffset)
    {
        // Define start and end positions
        Vector3 startPosition = ikController.localPosition;
        Vector3 endPosition = new Vector3(xOffset, yOffset, startPosition.z);

        // Time taken for interpolation
        float duration = 1.0f;
        float elapsedTime = 0.0f;

        // Interpolation loop
        while (elapsedTime < duration)
        {
            // Interpolate position
            ikController.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);

            // Increment timer
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final position is accurate
        ikController.localPosition = endPosition;
    }
}
