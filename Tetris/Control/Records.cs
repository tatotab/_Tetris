using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Tetris.Control
{
    public static class Records
    {
        public static string pathHighScores = "highScores.txt";
        public static string pathUserSaved = "userSaved.txt";

        public static IEnumerable<string> LoadForHighScores(string path)
        {
            string[] array = File.ReadAllLines(path);
            return array;
        }
        public static void SaveForHighScores(string name) //Saves Player's Name
        {
            //Creating List and using it's properties
            List<string> list = new List<string>(LoadForHighScores(pathHighScores));
            bool nameExists = false;
            
            for (int i = 0; i < list.Count; i++)
            {
                if (name == list[i].Split('|')[0])
                {
                    nameExists = true;
                    list.RemoveAt(i);
                    list.Add(name + "|" + Drawings.totScore);
                }
            }

            if (!nameExists)
                list.Add(name + "|" + Drawings.totScore);
            File.WriteAllLines(pathHighScores, list);
        }
        public static string Show() //Shows Records of Players
        {
            string forRecords;
            //Creating List and using it's properties
            List<string> list = new List<string>(LoadForHighScores(pathHighScores));

            for (int i = 0; i < list.Count - 1; ++i)
            {
                string array = list[i];
                string nextArray = list[i + 1];
                string[] args = array.Split('|');
                string[] nextArgs = nextArray.Split('|');

                if (int.Parse(args[1]) < int.Parse(nextArgs[1]))
                {
                    var temporary = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = temporary;
                    i = -1;
                }
            }

            forRecords = "List of Records!";

            foreach (var str in list)
            {
                string[] args = str.Split('|');

                forRecords += "\n";
                forRecords += "Player " + args[0] + ": " + args[1];
            }
            return forRecords;
        }

        public static void SaveForUser(string _name)
        {
            List<string> list = new List<string>();
            list.Add(_name);
            list.Add(Drawings.totScore.ToString());
            list.Add(Drawings.level.ToString());
            list.Add(Drawings.RemovedLine.ToString()) ;
            list.Add(Drawings.interval.ToString()) ;
            File.WriteAllLines(pathUserSaved, list);

        }
        public static string LoadForUser()
        {
            List<string> list = new List<string>(LoadForHighScores(pathUserSaved)); //read from file
            string name = list[0];
            Drawings.totScore = uint.Parse(list[1]);
            Drawings.level = uint.Parse(list[2]);
            Drawings.RemovedLine = uint.Parse(list[3]);
            Drawings.interval = int.Parse(list[4]);

            return name;
        }
    }
        
}
