using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

public class Bootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void AfterScene()
    {
        Debug.Log("after scene load");

        EntityManager em = World.Active.GetOrCreateManager<EntityManager>();
        EntityArchetype archetype = em.CreateArchetype(typeof(Position));

        MeshInstanceRendererComponent[] mirc = GameObject.FindObjectsOfType<MeshInstanceRendererComponent>();

        foreach (var m in mirc)
        {
            MeshInstanceRenderer renderer = m.Value;

            var entity = em.CreateEntity(archetype);
            var pos = m.transform.position;
            em.SetComponentData(entity, new Position { Value = new float3(pos.x, pos.y, pos.z) });
            em.AddSharedComponentData<MeshInstanceRenderer>(entity, renderer);
        }
    }
}