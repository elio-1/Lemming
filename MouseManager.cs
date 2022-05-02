using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * 150f, Color.red);

        RaycastHit2D hit = Physics2D.GetRayIntersection(_ray, Mathf.Infinity, _layerMask);

        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Lemming"))
            {
                
                LemmingStateMachine lemmingState = hit.collider.gameObject.GetComponent<LemmingStateMachine>();
                lemmingState.TransitionToState(LemmingState.STOP);
            }
            
        }
    }


}
