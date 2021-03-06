﻿using FlipFall.Levels;
using FlipFall.Theme;
using System.Collections;
using UnityEngine;

namespace FlipFall.LevelObjects
{
    public class MoveArea : LevelObject
    {
        public MeshFilter meshFilter;
        private PolygonCollider2D poly2D;
        private MeshRenderer mr;

        private void Awake()
        {
            objectType = ObjectType.moveArea;
            meshFilter = GetComponent<MeshFilter>();
            mr = GetComponent<MeshRenderer>();
            mr.material.SetColor("_Color", ThemeManager.theme.moveZoneColor);
            mr.material.SetFloat("_SliceAmount", 1F);

            Main.onSceneChange.AddListener(SceneChanged);
            StartCoroutine(cReverseDissolveLevel(LevelManager._instance.DissolveLevelDuration));
        }

        private void SceneChanged(Main.ActiveScene s)
        {
            DissolveLevel();
        }

        private void UpdateColliders()
        {
        }

        public void DissolveLevel()
        {
            mr = GetComponent<MeshRenderer>();
            StartCoroutine(cDissolveLevel(LevelManager._instance.DissolveLevelDuration));
        }

        // updates the shader's slice amount each frame to fade out the level. Also fades the alpha color value.
        private IEnumerator cDissolveLevel(float duration)
        {
            if (mr != null)
            {
                yield return new WaitForSeconds(0.03F);
                Material m = mr.material;

                // begin color - full alpha
                Color c = ThemeManager.theme.moveZoneColor;

                // end color - no alpha
                Color ca = new Color(c.r, c.g, c.b, 0F);

                float t = 0F;
                while (t < 1.0f)
                {
                    //float alpha = Mathf.Lerp(0.0, 1.0, lerp);
                    t += Time.deltaTime * (Time.timeScale / duration);
                    if (t > 0.3F)
                        m.SetColor("_Color", Color.Lerp(c, ca, (t - 0.3F) * 2));
                    m.SetFloat("_SliceAmount", t);
                    yield return 0;
                }
                m.SetColor("_Color", ca);
            }
            else
                Debug.Log("Dissolving Level failed, moveAreaGo MeshRenderer not found.");
            yield break;
        }

        private IEnumerator cReverseDissolveLevel(float duration)
        {
            if (mr != null)
            {
                yield return new WaitForSeconds(0.05F);
                Material m = mr.material;

                // begin color - no alpha
                Color c = ThemeManager.theme.moveZoneColor;

                // end color - full alpha
                Color ca = new Color(c.r, c.g, c.b, 0F);

                float t = 0F;
                while (t < 1.0f)
                {
                    t += Time.deltaTime * (Time.timeScale / duration);
                    if (t < 0.5F)
                        m.SetColor("_Color", Color.Lerp(ca, c, (t * 2)));
                    m.SetFloat("_SliceAmount", 1 - t);
                    yield return 0;
                }

                m.SetColor("_Color", ThemeManager.theme.moveZoneColor);
            }
            else
                Debug.Log("Dissolving Level failed, moveAreaGo MeshRenderer not found.");
            yield break;
        }
    }
}