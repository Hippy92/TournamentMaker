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
    public class Tournament
    {
        int degree;
        public List<Tour> tours;
        public List<Fight> fights;
        public List<Competitor> competitors;

        public Tournament(List<String> competitorsNames) 
        {
            degree = (int)(System.Math.Log(competitorsNames.Count, 2.0) + 0.1);

            competitors = new List<Competitor>();
            for (int i = 0; i < competitorsNames.Count; i++)
                competitors.Add(new Competitor(i, competitorsNames[i]));

            tours = new List<Tour>();
            fights = new List<Fight>();

            for (int i = 0; i < degree; i++)
            {
                Tour tour = new Tour(i);

                for (int j = 0; j < Math.Pow(2.0, degree - i - 1); j++)
                {
                    Fight fight = new Fight();
                    
                    if (i > 0)
                    {
                        fight = new Fight(tours[tours.Count - 1].getFights()[j * 2].getWinner(),
                            tours[tours.Count - 1].getFights()[j * 2 + 1].getWinner());
                    }
                    
                    fight.setNumber(j);
                    tour.addFight(fight);
                    fights.Add(fight);
                }


                tours.Add(tour);
            }
            
            int count = 0;
            foreach (Fight fight in tours[0].getFights())
            {
                fight.setFirstCompetitor(competitors[count]);
                fight.setSecondCompetitor(competitors[count + 1]);


                count+=2;
            }
        }
    }
}
