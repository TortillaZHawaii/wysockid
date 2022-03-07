using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int GetResult(string a)
        {
            if(int.TryParse(a, out int result))
            {
                if(result < 0)
                {
                    throw new ArgumentException();
                }
                return result;
            }

            var splitting = new List<string>()
            {
                "\n", ","
            };

            var (extraSeparators, textWithoutExtras) = RetrieveExtraSeparators(a);
            
            splitting.AddRange(extraSeparators);
            
            var sum = SumPartsSplitBySymbols(textWithoutExtras, splitting.ToArray());

            if(sum is not null)
            {
                return sum.Value;
            }

            return 0;
        }

        private (string[] extraSeparators, string textWithoutExtras)
            RetrieveExtraSeparators(string text)
        {
            if(text.Length > 1 && text[0] == '#')
            {
                if(text.Length > 2 && text[1] == '[')
                {
                    int closingBracketIndex = text.IndexOf(']');

                    string separator = text.Substring(2, closingBracketIndex - 2);

                    return (new string[] { separator }, text.Substring(closingBracketIndex + 1));
                }
                
                return (new string[]{ text[1].ToString() }, text.Substring(2));
            }
            return (new string[] { }, text);
        }

        private int? SumPartsSplitBySymbols(string text, string[] splittingSymbols)
        {
            var parts = text.Split(splittingSymbols, StringSplitOptions.None);

            if(parts.Length > 3 || parts.Length < 2)
            {
                return null;
            }

            int sum = 0;
            for(int i = 0; i < parts.Length; ++i)
            {
                if(int.TryParse(parts[i], out int result))
                {
                    if(result < 0)
                    {
                        throw new ArgumentException();
                    }
                    if(result < 1000)
                    {
                        sum += result;
                    }
                }
                else
                {
                    return null;
                }
            }

            return sum;
        }

    }
}
