using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Project_WPF.MVVM.Core
{
    public static class CustomDisplay
    {
        private static string AddLetter(string verb, int poz, char character)
        {
            string temp = verb.Substring(0, verb.Length + poz);
            string endTemp = verb.Substring(verb.Length + poz);
            temp += character + endTemp;
            return temp;
        }

        private static string RemoveLetter(string verb, int poz)
        {
            if (poz * (-1) != verb.Length)
            {
                string temp = verb.Substring(0, verb.Length + poz - 1);
                string endTemp = verb.Substring(verb.Length + poz);
                temp += endTemp;
                return temp;
            }
            return verb;
        }

        public static void TextCentered(string sentence)
        {
            Console.Write(new string(' ', (150 - sentence.Length) / 2) + sentence);
        }

        public static string WritingModule(ConsoleKeyInfo choice, string verbs, int userVPoz, int maxLenght)
        {
            if (choice.Key == ConsoleKey.Backspace)
            {
                return RemoveLetter(verbs, userVPoz);
            }

            char litera;

            if (verbs.Length >= maxLenght) return verbs;

            if (choice.Key >= ConsoleKey.A && choice.Key <= ConsoleKey.Z)
            {
                litera = choice.Modifiers.HasFlag(ConsoleModifiers.Shift)
                    ? (char)('A' + (choice.Key - ConsoleKey.A))
                    : (char)('a' + (choice.Key - ConsoleKey.A));

                return AddLetter(verbs, userVPoz, litera);
            }

            else if (choice.Key >= ConsoleKey.D0 && choice.Key <= ConsoleKey.D9)
            {
                if (choice.Modifiers.HasFlag(ConsoleModifiers.Shift))
                {
                    switch (choice.Key)
                    {
                        case ConsoleKey.D1: litera = '!'; break;
                        case ConsoleKey.D2: litera = '@'; break;
                        case ConsoleKey.D3: litera = '#'; break;
                        case ConsoleKey.D4: litera = '$'; break;
                        case ConsoleKey.D5: litera = '%'; break;
                        case ConsoleKey.D6: litera = '^'; break;
                        case ConsoleKey.D7: litera = '&'; break;
                        case ConsoleKey.D8: litera = '*'; break;
                        case ConsoleKey.D9: litera = '('; break;
                        case ConsoleKey.D0: litera = ')'; break;
                        default: litera = '\0'; break;
                    }
                }
                else
                {
                    litera = (char)('0' + (choice.Key - ConsoleKey.D0));
                }

                return AddLetter(verbs, userVPoz, litera);
            }
            else if (choice.Key == ConsoleKey.OemPeriod)
            {
                litera = '.';
                return AddLetter(verbs, userVPoz, litera);
            }

            return verbs;
        }

        public static UserType SelectUserType(ConsoleKeyInfo choice, UserType current)
        {
            List<UserType> userTypes = new List<UserType>() { UserType.Anonymous, UserType.Standard, UserType.Moderator, UserType.Administrator};

            int currentPoz = 0;

            if (current == UserType.Anonymous)
                currentPoz = 0;
            if (current == UserType.Standard)
                currentPoz = 1;
            if (current == UserType.Moderator)
                currentPoz = 2;
            if (current == UserType.Administrator)
                currentPoz = 3;


            if (choice.Key == ConsoleKey.RightArrow)
            {
                if (currentPoz == 3) currentPoz = 0;
                else currentPoz++;
            }
            if (choice.Key == ConsoleKey.LeftArrow)
            {
                if (currentPoz == 0) currentPoz = 3;
                else
                {
                    currentPoz--;
                }
            }

            return userTypes[currentPoz];
        }
    }
}
