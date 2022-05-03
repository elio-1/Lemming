using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    
    private LemmingStateMachine selectedLem;
    private GameObject selectedLemOutline;

    void Update()
    {
       // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(_ray, Mathf.Infinity, _layerMask);
       
        Debug.DrawRay(_ray.origin, _ray.direction * 150f, Color.red);

        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.CompareTag("Lemming"))
            { 
                selectedLemOutline = hit.transform.GetChild(0).gameObject;
                selectedLem = hit.collider.gameObject.GetComponent<LemmingStateMachine>();
                selectedLemOutline.SetActive(!selectedLemOutline.activeInHierarchy);
            }
        }
        else if (selectedLemOutline != null && Input.GetMouseButtonDown(0))
        {          
                selectedLemOutline.SetActive(false);
    
        }
        
        

    }

    public void StopLemming()
    {
        selectedLem.Stop();
        selectedLemOutline.SetActive(false);
    }
    
}
