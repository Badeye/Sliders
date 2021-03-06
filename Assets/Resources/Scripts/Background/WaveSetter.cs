﻿using FlipFall.Audio;
using FlipFall.Progress;
using FlipFall.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the wave Mesh generated through the WaveGenerator to the respective HorizonPart
/// </summary>

namespace FlipFall.Background
{
    public class WaveSetter : MonoBehaviour
    {
        // sorting layer id
        public int id;

        private MeshFilter mf;
        private Mesh mesh;
        private MeshRenderer mr;

        private void Start()
        {
            mr = GetComponent<MeshRenderer>();
            mr.sortingOrder = id;
            mf = GetComponent<MeshFilter>();
            WaveGenerator.onMeshUpdate.AddListener(MeshUpdated);

            transform.localScale = new Vector3(WaveGenerator.bgWidth, transform.localScale.y, transform.localScale.z);

            transform.position = new Vector3(WaveGenerator._instance.backgroundCam.ViewportToWorldPoint(Vector3.zero).x, transform.position.y, transform.position.z);
        }

        private void MeshUpdated(Mesh m)
        {
            if (WaveGenerator.generateWaves)
            {
                MeshFilter filter = GetComponent<MeshFilter>();
                mesh = filter.mesh;
                mesh.Clear();

                mesh.vertices = m.vertices;
                mesh.triangles = m.triangles;
            }
        }
    }
}