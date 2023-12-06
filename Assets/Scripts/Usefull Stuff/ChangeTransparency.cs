using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeTransparency : MonoBehaviourWithPause{

    
    [SerializeField] Shader transparentShader;
    [SerializeField] float alphaValue;
    [SerializeField] bool changeAllChildrenTransparency;

    Shader originalShader;
    public bool isTransparent { get; private set; }

    float timeSinceStart;

    bool action1Complete = false;
    bool action2Complete = false;

    private void Awake(){
        isTransparent = false;
    }

    protected override void UpdateWithPause(){

        //timeSinceStart += Time.deltaTime;

        //if (timeSinceStart > 5 && timeSinceStart < 10 && !action1Complete) {
        //    action1Complete = true;
        //    SetTransparency();
        //}

        //if (timeSinceStart > 10 && !action2Complete) {
        //    action2Complete = true;
        //    Debug.Log("ambatugenocide");
        //    SetTransparency();
        //}

    }

    public void SetTransparency() {

        FindMaterialsAndChangeTransparency(GetComponent<Renderer>(), alphaValue);

        if (!changeAllChildrenTransparency)
            isTransparent = !isTransparent;

        if (!changeAllChildrenTransparency)
            return;


        List<Transform> allChildrenTransforms=new List<Transform>();

        foreach (Transform r in transform.GetComponentsInChildren<Transform>()) 
            allChildrenTransforms.Add(r);

        Debug.Log(allChildrenTransforms.Count);

        for (int i = 0; i < allChildrenTransforms.Count; i++)
            FindMaterialsAndChangeTransparency(allChildrenTransforms[i].GetComponent<Renderer>(), alphaValue);

        isTransparent = !isTransparent;

    }

    void FindMaterialsAndChangeTransparency(Renderer r,float pTransparency) {
        if (r == null)
            return;


        Material[] materials = r.materials;

        if (!isTransparent)
            originalShader = materials[0].shader;

        //Debug.Log(originalShader);

        foreach (Material m in materials){
            Color color = Color.white;

            if (!isTransparent){
                color = m.color;
                m.shader = transparentShader;
                m.SetFloat("_Opacity", pTransparency);
                m.SetTexture("_AlbedoTexture", m.mainTexture);
                if (color != Color.white)
                    m.SetColor("_AlbedoColor", color);

            }
            else {
                m.shader = originalShader;
            }



        }
    }
    
}
