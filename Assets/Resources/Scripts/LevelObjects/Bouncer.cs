﻿using FlipFall;
using FlipFall.Levels;
using FlipFall.Theme;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipFall.LevelObjects
{
    public class Bouncer : LevelObject
    {
        // value between 0 and 5 => 0 = material with bouncines of 0.5; 1 = 1; 2 = 1.5; 3 = 2; 4 = 2.5; 5 = 3
        public int bounciness = 0;
        public int width = 4;
        public PhysicsMaterial2D bounceMaterial;

        private float colorFadeInDuration = 0.1F; //add sound
        private float colorFadeBackDuration = 0.5F;

        // when the player doesn't touch the strip anymore, we blend back to the first color.
        private bool autoFadeBack = true;

        private Rigidbody2D playerRb;
        private bool colliding;
        private float blend;

        private Material mat;

        private BoxCollider2D boxColl;

        private void Start()
        {
            objectType = ObjectType.bouncer;
            mat = GetComponent<MeshRenderer>().material;
            boxColl = GetComponent<BoxCollider2D>();

            boxColl.enabled = false;
            SetBounciness(bounciness);
            SetWidth(width);
            boxColl.enabled = true;

            Debug.Log(bounceMaterial);

            if (mat != null)
            {
                mat.SetColor("_Color", ThemeManager.theme.speedstripUnactiveColor);
                mat.SetColor("_ColorTouch", ThemeManager.theme.speedstripColor);
            }

            if (Player._instance != null)
            {
                playerRb = Player._instance.GetComponent<Rigidbody2D>();
            }
            colliding = false;
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.collider.tag == Constants.playerTag && playerRb != null)
            {
                colliding = true;
                StartCoroutine(cFadeIn());
                //Quaternion speedStripAngle = transform.rotation;
                //Vector2 forceDirection = speedStripAngle * Vector2.up;
                //playerRb.AddForce(forceDirection * Time.fixedDeltaTime * accelSpeed * accelMulti);
            }
        }

        private void OnCollisionExit2D(Collision2D coll)
        {
            if (coll.collider.tag == Constants.playerTag && playerRb != null)
            {
                Debug.Log("this");
                StartCoroutine(cFadeOut());
                colliding = false;
            }
        }

        private IEnumerator cFadeIn()
        {
            while ((blend < 1) && mat != null)
            {
                blend += Time.deltaTime * (Time.timeScale / colorFadeInDuration);
                mat.SetFloat("_Blend", blend);
                yield return new WaitForFixedUpdate();
            }
            yield break;
        }

        private IEnumerator cFadeOut()
        {
            while (autoFadeBack && blend > 0 && mat != null)
            {
                blend -= Time.deltaTime * (Time.timeScale / colorFadeBackDuration);
                if (blend < 0)
                {
                    blend = 0;
                    mat.SetFloat("_Blend", blend);
                    yield break;
                }
                mat.SetFloat("_Blend", blend);
                yield return 0;
            }
            yield break;
        }

        // sets the physicsmaterial depending on an input integer of 0-5, which gets set by the bouncer preferences window slider
        public void SetBounciness(int b)
        {
            bounciness = b;
            switch (b)
            {
                case 0:
                    boxColl.sharedMaterial = LevelPlacer._instance.bounce05;
                    break;

                case 1:
                    boxColl.sharedMaterial = LevelPlacer._instance.bounce1;
                    break;

                case 2:
                    boxColl.sharedMaterial = LevelPlacer._instance.bounce15;
                    break;

                case 3:
                    boxColl.sharedMaterial = LevelPlacer._instance.bounce2;
                    break;

                case 4:
                    boxColl.sharedMaterial = LevelPlacer._instance.bounce25;
                    break;

                case 5:
                    boxColl.sharedMaterial = LevelPlacer._instance.bounce3;
                    break;
            }
        }

        // sets the objects width (default 50) depending on an integer value set by the width slider in the bouncer preferences
        public void SetWidth(int b)
        {
            width = b;
            switch (b)
            {
                case 0:
                    transform.localScale = new Vector3(10, transform.localScale.y, transform.localScale.x);
                    break;

                case 1:
                    transform.localScale = new Vector3(20, transform.localScale.y, transform.localScale.x);
                    break;

                case 2:
                    transform.localScale = new Vector3(40, transform.localScale.y, transform.localScale.x);
                    break;

                case 3:
                    transform.localScale = new Vector3(60, transform.localScale.y, transform.localScale.x);
                    break;

                case 4:
                    transform.localScale = new Vector3(80, transform.localScale.y, transform.localScale.x);
                    break;

                case 5:
                    transform.localScale = new Vector3(100, transform.localScale.y, transform.localScale.x);
                    break;

                case 6:
                    transform.localScale = new Vector3(120, transform.localScale.y, transform.localScale.x);
                    break;

                case 7:
                    transform.localScale = new Vector3(140, transform.localScale.y, transform.localScale.x);
                    break;

                case 8:
                    transform.localScale = new Vector3(160, transform.localScale.y, transform.localScale.x);
                    break;

                case 9:
                    transform.localScale = new Vector3(180, transform.localScale.y, transform.localScale.x);
                    break;

                case 10:
                    transform.localScale = new Vector3(200, transform.localScale.y, transform.localScale.x);
                    break;

                case 11:
                    transform.localScale = new Vector3(220, transform.localScale.y, transform.localScale.x);
                    break;

                case 12:
                    transform.localScale = new Vector3(240, transform.localScale.y, transform.localScale.x);
                    break;

                case 13:
                    transform.localScale = new Vector3(260, transform.localScale.y, transform.localScale.x);
                    break;

                case 14:
                    transform.localScale = new Vector3(280, transform.localScale.y, transform.localScale.x);
                    break;

                case 15:
                    transform.localScale = new Vector3(300, transform.localScale.y, transform.localScale.x);
                    break;
            }
        }
    }
}