using Sirenix.OdinInspector;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private MeshRenderer mesh;

    [SerializeField, ReadOnly]
    private float lenghtUnits;
    [SerializeField, ReadOnly]
    private int roadUnits;

    #endregion

    #region Propeties

    public MeshRenderer Mesh {
        get => mesh; }

    public float LenghtUnits {
        get => lenghtUnits; 
        private set => lenghtUnits = value; 
    }
    public int RoadUnits { 
        get => roadUnits; 
        private set => roadUnits = value; 
    }

    #endregion

    #region Methods

    [Button(ButtonSizes.Large)]
    private void RefreshLenghtUnits()
    {
        LenghtUnits = Mesh.bounds.size.x;
        RoadUnits = (int)(LenghtUnits / 80f);
    }

    #endregion

    #region Enums



    #endregion
}