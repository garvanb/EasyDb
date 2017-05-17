using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Logic
{
    public static class CodeGeneratorExtensions
    {
        public static void AppendWithNewLine(this StringBuilder code, string text)
        {
            code.Append(text);
            code.Append(Environment.NewLine);
        }

        public static void AppendWithNewTabbedLine(this StringBuilder code, string text)
        {
            code.AddTab();
            code.Append(text);
            code.Append(Environment.NewLine);
        }

        public static void AppendWithTabsAndNewLine(this StringBuilder code, int tabCount, string text)
        {
            code.AddTabs(tabCount);
            code.Append(text);
            code.Append(Environment.NewLine);
        }

        public static void AppendWithNewTabbedLines(this StringBuilder code, string text, int newLines)
        {
            code.AddTab();
            code.Append(text);
            if (newLines > 0)
            {
                for (int counter = 1; counter <= newLines; counter++)
                {
                    code.Append(Environment.NewLine);
                }
            }
        }

        public static void AppendWithNewLines(this StringBuilder code, string text, int newLines)
        {
            code.Append(text);
            if (newLines > 0)
            {
                for (int counter = 1; counter <= newLines; counter++)
                {
                    code.Append(Environment.NewLine);
                }
            }
        }

        public static void AppendIfThere(this StringBuilder code, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                code.Append(value);
            }
        }

        public static void AddGo(this StringBuilder code)
        {
            code.Append("GO");
            code.Append(Environment.NewLine);
            code.Append(Environment.NewLine);
        }

        public static void AddTab(this StringBuilder code)
        {
            code.Append("\t");
        }

        public static void AddTabs(this StringBuilder code, int tabs)
        {
            if (tabs > 0)
            {
                for (int counter = 1; counter <= tabs; counter++)
                {
                    code.Append("\t");
                }
            }
        }

        public static void AddLine(this StringBuilder code)
        {
            code.Append(Environment.NewLine);
        }

        public static void AppendWithAngleBrackets(this StringBuilder code, string text)
        {
            code.Append("[");
            code.Append(text);
            code.Append("]");
        }
    }
}
