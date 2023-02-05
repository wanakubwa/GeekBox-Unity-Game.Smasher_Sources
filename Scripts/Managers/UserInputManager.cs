using UnityEngine;
using System.Collections;
using System;

public class UserInputManager : ManagerSingletonBase<UserInputManager>
{
    #region Fields

    private const int LEFT_BUTTON = 0;

    #endregion

    #region Propeties

    /// <summary>
    /// vector2 - movedelta.
    /// </summary>
    public event Action<Vector2> OnMouseHold = delegate { };
    public event Action OnMouseRelease = delegate { };

    // Variables.
    private bool IsLeftMousePressed { get; set; } = false;
    private Vector2 PressAnchorPosition { get; set; } = Vector2.positiveInfinity;
    private Vector2 LastMousePosition { get; set; } = Vector2.zero;


    #endregion

    #region Methods

    private void Update()
    {
        if (Input.GetMouseButtonDown(LEFT_BUTTON))
        {
            OnLeftButtonPress();
        }

        if (Input.GetMouseButtonUp(LEFT_BUTTON))
        {
            OnLeftButtonRelease();
        }

        UpdateMouseEvents();
    }

    private void OnLeftButtonPress()
    {
        IsLeftMousePressed = true;
        PressAnchorPosition = Input.mousePosition;

        LastMousePosition = PressAnchorPosition;
    }

    private void OnLeftButtonRelease()
    {
        IsLeftMousePressed = false;
        PressAnchorPosition = Vector2.positiveInfinity;
        OnMouseRelease();
    }

    private void UpdateMouseEvents()
    {
        if(IsLeftMousePressed == true)
        {
            Vector2 currentPosition = Input.mousePosition;
            Vector2 delta = currentPosition - LastMousePosition;

            OnMouseHold(delta);

            LastMousePosition = Input.mousePosition;
        }
    }

    #endregion

    #region Enums



    #endregion
}
