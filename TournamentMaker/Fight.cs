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

using System.Collections.Generic;

namespace TournamentMaker
{
    public class Fight
    {
        int number;
        Competitor[] competitors;
        Competitor winner;

        public Fight()
        {
            competitors = new Competitor[2];
            competitors[0] = new Competitor(0, "Undefined");
            competitors[1] = new Competitor(1, "Undefined");
            winner = new Competitor(0, "Undefined");
        }

        public Fight(Competitor competitor1, Competitor competitor2)
        {
            competitors = new Competitor[2];
            competitors[0] = competitor1;
            competitors[1] = competitor2;
            winner = new Competitor(0, "Undefined");
        }

        public int getNumber()
        {
            return number;
        }

        public void setNumber(int number)
        {
            this.number = number;
        }

        public void setFirstCompetitor(Competitor competitor)
        {
            competitors[0] = competitor;
        }

        public void setSecondCompetitor(Competitor competitor)
        {
            competitors[1] = competitor;
        }

        public Competitor getFirstCompetitor()
        {
            return competitors[0];
        }

        public Competitor getSecondCompetitor()
        {
            return competitors[1];
        }

        public Competitor getWinner()
        {
            return winner;
        }

        public bool setWinner(int winner)
        {
            if (winner == 0 || winner == 1)
            {
                this.winner = competitors[winner];
                return true;
            }
            else
                return false;
        }
    }
}
