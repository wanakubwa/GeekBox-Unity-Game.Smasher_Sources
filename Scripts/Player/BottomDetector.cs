using UnityEngine;

public class BottomDetector : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float depth;
    [SerializeField]
    private LayerMask layer;

    #endregion

    #region Propeties

    public float Depth { get => depth; }
    public LayerMask Layer { get => layer; }

    // Variables.
    private RaycastHit[] _hitsBuffer = new RaycastHit[1]; 

    #endregion

    #region Methods

    public void CheckHit(PlayerBall source)
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * Depth, Color.green, 1f);
        if (Physics.RaycastNonAlloc(ray, _hitsBuffer, Depth, Layer.value) > 0)
        {
            IPlayerInteractable interactable = _hitsBuffer[0].collider.GetComponent<IPlayerInteractable>();
            if (interactable != null)
            {
                interactable.Interact(source);
            }
        }
    }

    #endregion

    #region Enums



    #endregion
}
