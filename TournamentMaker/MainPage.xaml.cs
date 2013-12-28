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
    public partial class MainPage : PhoneApplicationPage
    {
        public static Tournament tournament;
        public static String tournamentName;
        public List<String> tournamentTypes;
        public String inputString = "Input new team/participant name";
        public StackPanel textBoxStackPanel;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            TextBlock tournamentTypeTextBlock = getTextBlock("Choose tournament type");
            tournamentTypeTextBlock.Name = "TournamentTypeTextBlock";
            ContentPanel.Children.Add(tournamentTypeTextBlock);

            tournamentTypes = new List<String>();
            tournamentTypes.Add("Single Elimination");
            tournamentTypes.Add("Double Elimination");
            tournamentTypes.Add("Triple Elimination");

            foreach (String s in tournamentTypes)
                ContentPanel.Children.Add(getRadioButton(s));


            TextBlock tournamentNameTextBlock = getTextBlock("Input tournament name");
            tournamentNameTextBlock.Name = "TournamentNameTextBlock";
            ContentPanel.Children.Add(tournamentNameTextBlock);

            TextBox tournamentNameTextBox = new TextBox();
            tournamentNameTextBox.Name = "TournamentNameTextBox";
            tournamentNameTextBox.Text = "Input tournament name";
            tournamentNameTextBox.Margin = new Thickness(0, 0, 0, 0);
            tournamentNameTextBox.Height = 72;
            tournamentNameTextBox.Width = 440;
            tournamentNameTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            tournamentNameTextBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            tournamentNameTextBox.Foreground = (Brush)Application.Current.Resources["PhoneTextBoxForegroundBrush"];
            tournamentNameTextBox.SelectionForeground = (Brush)Application.Current.Resources["PhoneTextBoxSelectionForegroundBrush"];
            tournamentNameTextBox.GotFocus += new RoutedEventHandler(tournamentNameTextBox_GotFocus);
            tournamentNameTextBox.TextChanged += tournamentNameTextBox_TextChanged;
            ContentPanel.Children.Add(tournamentNameTextBox);

            TextBlock competitorsTextBlock = getTextBlock("Input participants/teams");
            competitorsTextBlock.Name = "CompetitorsTextBlock";
            ContentPanel.Children.Add(competitorsTextBlock);


            textBoxStackPanel = new StackPanel();
            textBoxStackPanel = getTextBoxStackPanel();
            ContentPanel.Children.Add(textBoxStackPanel);

            ContentPanel.Children.Add(getStartButton("start"));
        }

        void tournamentNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void tournamentNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                tournamentName = ((TextBox)sender).Text;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender = (TextBox)sender;

            if (sender is TextBox) {
                ((TextBox)sender).Text = "";
                textBoxStackPanel.Children.Add(getTextBox());
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox senderTextBox = (TextBox)sender;
                if (senderTextBox.Text.Length == 0)
                {
                    textBoxStackPanel.Children.Remove(textBoxStackPanel.Children.Last());
                    senderTextBox.GotFocus += textBox_GotFocus;
                    senderTextBox.Text = inputString;
                }
            }
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox)
            {
                if (e.Key == Key.Enter)
                {
                    TextBox senderTextBox = (TextBox)sender;
                    if (senderTextBox.Text == "")
                    {
                        textBox_LostFocus(sender, new RoutedEventArgs());
                    }
                }
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            System.Collections.Generic.List<String> names = new System.Collections.Generic.List<String>();

            foreach (UIElement child in textBoxStackPanel.Children)
            {
                if (child is TextBox)
                {
                    if (((TextBox)child).Text != inputString)
                    {
                        names.Add(((TextBox)child).Text);
                    }
                }
            }

            MainPage.tournament = new Tournament(names);

            NavigationService.Navigate(new Uri("/SingleEliminationPage.xaml", UriKind.Relative));
        }

        private TextBlock getTextBlock(String text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Height = 30;
            textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            return textBlock;
        }

        private RadioButton getRadioButton(String content)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.Name = content + "radioButton";
            radioButton.Content = content;
            radioButton.Height = 72;
            radioButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            radioButton.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            return radioButton;
        }

        private StackPanel getTextBoxStackPanel()
        {
            StackPanel sp = new StackPanel();
            sp.Name = "TextBoxStackPanel";
            sp.Children.Add(getTextBox());

            return sp;
        }

        private TextBox getTextBox()
        {
            int number = textBoxStackPanel.Children.Count + 1;

            TextBox textBox = new TextBox();
            textBox.Name = "competitorName" + number.ToString();
            textBox.Margin = new Thickness(0, -10, 0, 0);
            textBox.Height = 72;
            textBox.Width = 440;
            textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            textBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            textBox.Text = inputString;
            textBox.Foreground = (Brush)Application.Current.Resources["PhoneTextBoxForegroundBrush"];
            textBox.SelectionForeground = (Brush)Application.Current.Resources["PhoneTextBoxSelectionForegroundBrush"];

            textBox.GotFocus += textBox_GotFocus;
            textBox.LostFocus += textBox_LostFocus;
            textBox.KeyUp += textBox_KeyUp;

            if (textBoxStackPanel.Children.Count > 0)
                textBoxStackPanel.Children[number - 2].GotFocus -= textBox_GotFocus;
            
            return textBox;
        }

        private Button getStartButton(String content)
        {
            Button button = new Button();
            button.Name = "startButton1";
            button.Content = content;
            button.Height = 72;
            button.Width = 160;
            button.Margin = new Thickness(150, 0, 0, 0);
            button.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            button.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            button.Click += startButton_Click;

            return button;
        }
    }
}