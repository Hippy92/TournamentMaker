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
    public class Competitor
    {
        private System.Collections.Generic.KeyValuePair<int, String> idNamePair;

        public Competitor(int id, String Name)
        {
            idNamePair = new System.Collections.Generic.KeyValuePair<int, string>(id, Name);
        }

        public int getId()
        {
            return idNamePair.Key;
        }

        public void setId(int id)
        {
            String Name = idNamePair.Value;
            idNamePair = new System.Collections.Generic.KeyValuePair<int, string>(id, Name);
        }

        public String getName()
        {
            return idNamePair.Value;
        }

        public void setName(String Name)
        {
            int id = idNamePair.Key;
            idNamePair = new System.Collections.Generic.KeyValuePair<int, string>(id, Name);
        }
    }
}
