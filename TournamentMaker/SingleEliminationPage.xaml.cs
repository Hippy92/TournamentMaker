using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections.Generic;

namespace TournamentMaker
{
    public partial class SingleEliminationPage : PhoneApplicationPage
    {
        public SingleEliminationPage()
        {
            InitializeComponent();

            if (MainPage.tournamentName.Length > 0)
            {
                toursPanorama.Title = MainPage.tournamentName.ToLower();
                tournamentNameTextBlock.Text = MainPage.tournamentName + ":";
            }
            numberOfCompetitorsTextBlock.Text = "There are " + MainPage.tournament.competitors.Count + " competitors.";
            numberOfToursTextBlock.Text = "There are " + MainPage.tournament.tours.Count + " tours.";
            numberOfFightsTextBlock.Text = "There are " + MainPage.tournament.fights.Count + " fights.";

            foreach (Tour tour in MainPage.tournament.tours)
            {
                int number = tour.getNumber() + 1;
                PanoramaItem t = new PanoramaItem();
                t.Header = "tour #" + number.ToString();

                ScrollViewer scrollViewer = new ScrollViewer();
                scrollViewer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                scrollViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                foreach (Fight fight in tour.getFights())
                {
                    Dictionary<String, Object> tag;

                    StackPanel fightStackPanel = new StackPanel();
                    fightStackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    fightStackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    fightStackPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;

                    TextBlock firstTextBlock = new TextBlock();
                    firstTextBlock.Margin = new Thickness(0, 20, 30, 0);
                    firstTextBlock.FontSize = (double)Application.Current.Resources["PhoneFontSizeLarge"];
                    firstTextBlock.Text = fight.getFirstCompetitor().getName();
                    firstTextBlock.Tap += fightTextBlock_Tap;
                    tag = new Dictionary<String, Object>();
                    tag.Add("fightNumber", fight.getNumber());
                    tag.Add("tourNumber", tour.getNumber());
                    tag.Add("number", 0);
                    firstTextBlock.Tag = tag;

                    TextBlock secondTextBlock = new TextBlock();
                    secondTextBlock.Margin = new Thickness(30, 20, 0, 0);
                    secondTextBlock.FontSize = (double)Application.Current.Resources["PhoneFontSizeLarge"];
                    secondTextBlock.Text = fight.getSecondCompetitor().getName();
                    secondTextBlock.Tap += fightTextBlock_Tap;
                    tag = new Dictionary<String, Object>();
                    tag.Add("fightNumber", fight.getNumber());
                    tag.Add("tourNumber", tour.getNumber());
                    tag.Add("number", 1);
                    secondTextBlock.Tag = tag;


                    fightStackPanel.Children.Add(firstTextBlock);
                    fightStackPanel.Children.Add(secondTextBlock);


                    stackPanel.Children.Add(fightStackPanel);
                }

                scrollViewer.Content = stackPanel;
                t.Content = scrollViewer;

                toursPanorama.Items.Add(t);
            }

            int count = 0;
            foreach (Competitor c in MainPage.tournament.competitors)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = (++count).ToString() + ") " + c.getName();
                textBlock.FontSize = (double)Application.Current.Resources["PhoneFontSizeMedium"];
                competitorsListStackPanel.Children.Add(textBlock);
            }
        }

        void fightTextBlock_Tap(object sender, GestureEventArgs e)
        {
            if (sender is TextBlock)
            {
                if (((TextBlock)(sender)).Tag is Dictionary<String, Object>)
                {
                    Dictionary<String, Object> senderTag = (Dictionary<String, Object>)(((TextBlock)sender).Tag);
                    int fightNumber = (int)senderTag["fightNumber"];
                    int tourNumber = (int)senderTag["tourNumber"];
                    int number = (int)senderTag["number"];

                    MainPage.tournament.tours[tourNumber].getFights()[fightNumber].setWinner(number);



                    PanoramaItem t = new PanoramaItem();
                    t.Header = "tour #" + (tourNumber + 2).ToString();

                    ScrollViewer scrollViewer = new ScrollViewer();
                    scrollViewer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    scrollViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    stackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Top;


                    foreach (Fight fight in MainPage.tournament.tours[tourNumber + 1].getFights())
                    {
                        if (fight.getNumber() == fightNumber / 2)
                        {
                            if (fightNumber % 2 == 0)
                            {
                                fight.setFirstCompetitor(MainPage.tournament.tours[tourNumber].getFights()[fightNumber].getWinner());
                            }
                            else
                            {
                                fight.setSecondCompetitor(MainPage.tournament.tours[tourNumber].getFights()[fightNumber].getWinner());
                            }
                        }

                        Dictionary<String, Object> tag;

                        StackPanel fightStackPanel = new StackPanel();
                        fightStackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        fightStackPanel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                        fightStackPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;

                        TextBlock firstTextBlock = new TextBlock();
                        firstTextBlock.Margin = new Thickness(0, 20, 30, 0);
                        firstTextBlock.FontSize = (double)Application.Current.Resources["PhoneFontSizeLarge"];
                        firstTextBlock.Text = fight.getFirstCompetitor().getName();
                        firstTextBlock.Tap += fightTextBlock_Tap;
                        tag = new Dictionary<String, Object>();
                        tag.Add("fightNumber", fight.getNumber());
                        tag.Add("tourNumber", tourNumber + 1);
                        tag.Add("number", 0);
                        firstTextBlock.Tag = tag;

                        TextBlock secondTextBlock = new TextBlock();
                        secondTextBlock.Margin = new Thickness(30, 20, 0, 0);
                        secondTextBlock.FontSize = (double)Application.Current.Resources["PhoneFontSizeLarge"];
                        secondTextBlock.Text = fight.getSecondCompetitor().getName();
                        secondTextBlock.Tap += fightTextBlock_Tap;
                        tag = new Dictionary<String, Object>();
                        tag.Add("fightNumber", fight.getNumber());
                        tag.Add("tourNumber", tourNumber + 1);
                        tag.Add("number", 1);
                        secondTextBlock.Tag = tag;


                        fightStackPanel.Children.Add(firstTextBlock);
                        fightStackPanel.Children.Add(secondTextBlock);


                        stackPanel.Children.Add(fightStackPanel);
                    }



                    scrollViewer.Content = stackPanel;
                    t.Content = scrollViewer;
                    toursPanorama.Items[tourNumber + 2] = t;

                }
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}