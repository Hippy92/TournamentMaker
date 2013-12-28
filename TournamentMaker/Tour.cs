using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TournamentMaker
{
    public class Tour
    {
        int number;
        System.Collections.Generic.List<Fight> fights;

        public int getNumber()
        {
            return number;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public Tour(int number)
        {
            this.number = number;
            fights = new System.Collections.Generic.List<Fight>();
        }

        public void addFight(Fight fight)
        {
            fights.Add(fight);
        }

        public System.Collections.Generic.List<Fight> getFights()
        {
            return fights;
        }
    }
}
