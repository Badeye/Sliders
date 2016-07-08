﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sliders.Models
{
    [SerializeField]
    public class Scoreboard
    {
        public int levelId;
        public bool finished;
        public bool unlocked;
        public List<Highscore> elements;
        public DateTime created { get; set; }
        public DateTime updated { get; set; }

        public Scoreboard()
        {
            elements = new List<Highscore>();
            created = DateTime.UtcNow;
            updated = DateTime.UtcNow;
        }

        public Highscore Top()
        {
            if (elements.Count > 0)
                return elements[0];
            return null;
        }

        public void AddTime(double t)
        {
            Highscore h = new Highscore();
            h.time = t;
            elements.Add(h);

            Debug.Log("First Highscore Add - Time: " + h.time + " - Placed in Scoreboard of Level: " + levelId + " - At position: " + elements.FindIndex(x => x == h));
        }

        public void AddTimeAt(int location, double t)
        {
            Highscore h = new Highscore();
            h.time = t;
            elements.Insert(location, h);

            Debug.Log("highscore Insertion - Time: " + h.time + " - Placed in Scoreboard of Level: " + levelId + " - At position: " + elements.FindIndex(x => x == h));
        }

        public void TryPlacingTime(double newTime)
        {
            if (elements.Count > 0)
            {
                foreach (Highscore s in elements)
                {
                    if (newTime < s.time)
                    {
                        //Highscore e = elements.Find(s);
                        AddTimeAt(elements.FindIndex(x => x == s), newTime);
                        //elements.Insert(elements.FindIndex(x => x == s), newElement);
                        //elements.Add(newElement);

                        return;
                        break;
                    }
                }
            }
            else
            {
                AddTime(newTime);
            }
        }

        public bool IsPlacingTimePossible(double t)
        {
            foreach (Highscore s in elements)
            {
                if (s.time < t)
                {
                    return true;
                }
            }
            return false;
        }
    }
}