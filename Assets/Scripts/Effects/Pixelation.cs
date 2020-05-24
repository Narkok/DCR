﻿using UnityEngine;


[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Pixelation")]
public class Pixelation: MonoBehaviour {

    [Range(1, 920)] 
    public int BlocksCount = 800;


    private Camera cameraComponent;
    private RenderTexture texture;


    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 40;

        cameraComponent = GetComponent<Camera>();
        CreateTexture();
    }


    #if UNITY_EDITOR

    private void Update() {
        CreateTexture();
    }

    #endif


    private void CreateTexture() {
        float scale = Screen.width / (float)BlocksCount;
        int width = Mathf.RoundToInt(Screen.width / scale);
        int height = Mathf.RoundToInt(Screen.height / scale);

        texture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
        texture.antiAliasing = 1;
    }


    private void OnPreRender() {
        cameraComponent.targetTexture = texture;
    }


    private void OnPostRender() {
        cameraComponent.targetTexture = null;
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        source.filterMode = FilterMode.Point;
        Graphics.Blit(source, destination);
    }
}
