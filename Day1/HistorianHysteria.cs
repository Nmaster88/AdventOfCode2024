﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    public class HistorianHysteria
    {
        public int CalculateDistanceFromLists(List<int> listOne, List<int> listTwo) 
        { 
            listOne.Sort();
            listTwo.Sort();

            List<int> listCountDistances = new List<int>();

            for (int i = 0; i < listOne.Count; i++) 
            {
                int distance = Math.Abs(listTwo[i] - listOne[i]);
                listCountDistances.Add(distance);
            }

            return listCountDistances.Sum();
        }
    }
}
