using UnityEngine;
using UnityEditor;
using Unity.Rendering;

public class ModelImporter : AssetPostprocessor
{
    void OnPostprocessModel(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        if (renderers != null)
        {
            foreach (Renderer renderer in renderers)
            {
                MeshInstanceRendererComponent r = renderer.gameObject.AddComponent<MeshInstanceRendererComponent>();

                MeshFilter mf = r.GetComponent<MeshFilter>();

                if (mf == null)
                    continue;

                MeshInstanceRenderer meshInstance = new MeshInstanceRenderer
                {
                    mesh = mf.sharedMesh,
                    subMesh = 0,
                    receiveShadows = true,
                    castShadows = UnityEngine.Rendering.ShadowCastingMode.On,
                    material = renderer.sharedMaterial
                };

                r.Value = meshInstance;
            }
        }
    }
}