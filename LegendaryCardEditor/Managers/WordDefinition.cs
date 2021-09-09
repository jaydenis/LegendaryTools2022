using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Managers
{
    public class WordDefinition
    {

        public String word;

        public bool space = true;

        public WordDefinition(String _word, bool _space)
        {
            word = _word;
            space = _space;
        }

        public static List<WordDefinition> GetWordDefinitionList(String text)
        {
            List<WordDefinition> retVal = new List<WordDefinition>();
            String[] words = text.Split(' ');
            foreach (String s in words)
            {
                int breakCount = 0;
                foreach (char c in s.ToCharArray())
                {
                    if ((c == '<'))
                    {
                        breakCount++;
                    }

                }

                if ((breakCount >= 1))
                {
                    String[] Split = s.Split('<');
                    if (Split[0] != null)
                    {
                        if (Split[0].Contains('<'))
                        {
                            if ((Split[0].Contains('>')
                                        && ((Split[0].Split('>').Length > 1)
                                        && (Split[0].Split('>')[1].Length > 0))))
                            {
                                String[] Split2 = Split[0].Split('>');
                                // System.out.println(Split2[0] + ":" + Split2[1]);
                                retVal.Add(new WordDefinition(('<'
                                                    + (Split2[0] + '>')), false));
                                retVal.Add(new WordDefinition(Split2[1], false));
                            }
                            else
                            {
                                retVal.Add(new WordDefinition(('<' + Split[0]), false));
                            }

                        }
                        else
                        {
                            retVal.Add(new WordDefinition(Split[0], false));
                        }

                    }

                    for (int i = 1; (i
                                < (Split.Length - 1)); i++)
                    {
                        if ((Split[i].Contains('>')
                                    && ((Split[i].Split('>').Length > 1)
                                    && (Split[i].Split('>')[1].Length > 0))))
                        {
                            String[] Split2 = Split[i].Split('>');
                            // System.out.println(Split2[0] + ":" + Split2[1]);
                            retVal.Add(new WordDefinition(('<'
                                                + (Split2[0] + '>')), false));
                            retVal.Add(new WordDefinition(Split2[1], false));
                        }
                        else
                        {
                            retVal.Add(new WordDefinition(('<' + Split[i]), false));
                        }

                    }

                    if ((Split[(Split.Length - 1)].Contains('>')
                                && ((Split[(Split.Length - 1)].Split('>').Length > 1)
                                && (Split[(Split.Length - 1)].Split('>')[1].Length > 0))))
                    {
                        String[] Split2 = Split[(Split.Length - 1)].Split('>');
                        retVal.Add(new WordDefinition(('<'
                                            + (Split2[0] + '>')), false));
                        retVal.Add(new WordDefinition(Split2[1], true));
                    }
                    else
                    {
                        retVal.Add(new WordDefinition(('<' + Split[(Split.Length - 1)]), true));
                    }

                }
                else
                {
                    retVal.Add(new WordDefinition(s, true));
                }

            }

            return retVal;
        }
    }
}
