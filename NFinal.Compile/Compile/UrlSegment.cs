using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile
{
    public class UrlSegment
    {
        public int start=0;
        public int length=0;
        public string expression=null;
        public string name=null;
        public string format=null;
        public string regex=null;

        public UrlSegment(string url, int start, int length)
        {
            this.expression = url.Substring(start, length);
            this.start = start;
            this.length = length;
            int left = 0;
            int right = -1;
            int found = 0;
            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (expression[i] == ':')
                {
                    if (i < 2)
                    {
                        left = right + 1;
                        right = i;
                        found++;
                        if (found == 1)
                        {
                            this.name = expression.Substring(left, right - left);
                            this.regex = expression.Substring(right + 1);
                        }
                    }
                    else
                    {
                        if (expression[i - 2] == '(' && expression[i - 1] == '?')
                        {

                        }
                        else
                        {

                            found++;
                            if (found == 1)
                            {
                                left = right + 1;
                                right = i;
                                this.name = expression.Substring(left, right - left);
                                this.regex = expression.Substring(right + 1);
                            }
                            else if (found == 2)
                            {
                                left = right + 1;
                                right = i;
                                this.regex = expression.Substring(left, right - left);
                                this.format = expression.Substring(right + 1);
                            }
                        }
                    }
                }

            }
            if (found < 1)
            {
                this.name = expression;
                this.regex = "[\\\\S]+";
            }
            else
            {
                if (this.regex == "string")
                {
                    this.regex = "[\\\\S]+";
                }
                else if (this.regex == "int")
                {
                    this.regex = "[0-9]+";
                }
                else if (this.regex == "float")
                {
                    this.regex = "[0-9]*.[0-9]+";
                }
                else if (this.regex == "date")
                {
                    this.regex = "[0-9]{4}-[0-9]{2}-[0-9]{2}";
                }
            }
        }

    }
}
