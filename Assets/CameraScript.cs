using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviourWithPause{

    [SerializeField] LayerMask mask;

    Camera cameraMain;
    ChangeTransparency lastTransparencyObject;
    ChangeTransparency currentTransparencyObject;

    private void Start(){
        cameraMain= GetComponent<Camera>();
    }

    protected override void FixedUpdateWithPause(){

        Ray cameraRay = cameraMain.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        if (Physics.Raycast(cameraRay, out hit, float.MaxValue, mask))
        {
            if (hit.collider.CompareTag("Player")) {
                currentTransparencyObject = null;
                if (lastTransparencyObject != null) {
                    lastTransparencyObject.SetTransparency();
                    lastTransparencyObject = currentTransparencyObject;
                }
                return;
            }

            currentTransparencyObject = hit.collider.transform.root.gameObject.GetComponent<ChangeTransparency>();
            if (lastTransparencyObject == null)
                lastTransparencyObject = currentTransparencyObject;

            if (!currentTransparencyObject.isTransparent)
                currentTransparencyObject.SetTransparency();

        }
        else {
            currentTransparencyObject = null;
        }

        if (lastTransparencyObject != currentTransparencyObject) {
            lastTransparencyObject.SetTransparency();
            lastTransparencyObject = currentTransparencyObject;
        }

    }
}
