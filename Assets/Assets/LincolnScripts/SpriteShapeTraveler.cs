using UnityEngine;
using UnityEngine.U2D;

public class SpriteShapeTraveler : MonoBehaviour
{
    public SpriteShapeController shapeController;
    public float traversalSpeed = 2f;

    public int currentAnchorIndex = 0;
    public float currentTraversalTime = 0f;
    public bool isTraversing = false;

    public Color TraversingColor;
    public Color StoppedColor;
    private void Start()
    {
        if (shapeController == null)
        {
            Debug.LogError("Sprite Shape Controller is not assigned!");
            return;
        }

        // Set the initial position of the GameObject to the first anchor point
        if (shapeController.spline.GetPointCount() > 0)
        {
            transform.position = new Vector3(shapeController.spline.GetPosition(0).x, shapeController.gameObject.transform.position.y, shapeController.spline.GetPosition(0).y);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTraversing = !isTraversing;
            if(isTraversing)
            {
                this.GetComponent<MeshRenderer>().material.color = TraversingColor;
            }
            else
            {
                this.GetComponent<MeshRenderer>().material.color = StoppedColor;
            }
        }    
        if (!isTraversing || shapeController == null || shapeController.spline.GetPointCount() <= 1)
        {
            return;
        }

        // Calculate the position between the current and next anchor points based on traversal time
        Vector3 startPosition = new Vector3(shapeController.spline.GetPosition(currentAnchorIndex).x, shapeController.gameObject.transform.position.y, shapeController.spline.GetPosition(currentAnchorIndex).z);
        Vector3 endPosition = new Vector3(shapeController.spline.GetPosition(currentAnchorIndex +1 % shapeController.spline.GetPointCount() ).x, shapeController.gameObject.transform.position.y, shapeController.spline.GetPosition(currentAnchorIndex+1).y % shapeController.spline.GetPointCount());  //shapeController.spline.GetPosition((currentAnchorIndex + 1) % shapeController.spline.GetPointCount());
        Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, currentTraversalTime);

        // Move the GameObject towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, traversalSpeed * Time.deltaTime);

        // Update the traversal time
        currentTraversalTime += Time.deltaTime * traversalSpeed;

        // Check if the traversal time exceeds 1, indicating that the current anchor point has been reached
        if (currentTraversalTime >= 1f)
        {
            currentAnchorIndex = currentAnchorIndex + 1;
            currentTraversalTime = 0f;
            if (currentAnchorIndex == (shapeController.spline.GetPointCount()))
            {
                shapeController = null;
                StopTraversal();
                //currentAnchorIndex = (currentAnchorIndex + 1) % shapeController.spline.GetPointCount();
            }
          
        }
    }

    public void StartTraversal()
    {
        isTraversing = true;
    }

    public void StopTraversal()
    {
        isTraversing = false;
    }
}