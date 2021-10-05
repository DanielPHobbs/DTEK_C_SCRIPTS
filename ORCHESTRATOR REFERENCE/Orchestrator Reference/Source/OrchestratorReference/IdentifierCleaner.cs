namespace C60.OrchestratorReference
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.CSharp;

    /// <summary>
    /// Provide a valid Class/Method/Parameter name.
    /// </summary>
    public class IdentifierCleaner
    {
        private static Regex legalCharactersRegex = new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]");
        private static CodeDomProvider codeDomProvider = CSharpCodeProvider.CreateProvider("C#");
        
        public string GetPascalName(string originalName)
        {
            if (string.IsNullOrWhiteSpace(originalName))
            {
                throw new ArgumentNullException("originalName");
            }

            return this.CleanIndentifier(originalName, true);
        }

        public string GetCamelName(string originalName)
        {
            if (string.IsNullOrWhiteSpace(originalName))
            {
                throw new ArgumentNullException("originalName");
            }

            return this.CleanIndentifier(originalName, false);
        }

        private string CleanIndentifier(string originalName, bool toPascal)
        {
            string ret = legalCharactersRegex.Replace(originalName, string.Empty);
            int i = ret.IndexOf(ret.FirstOrDefault(c => char.IsLetter(c)));
            if (i == -1)
            {
                if (ret.Length > 0)
                {
                    ret = string.Concat("_", ret);
                }
                else
                {
                    throw new ArgumentException("Can't convert to valid indentifier the string :" + originalName);
                }
            }
            else
            {
                ret = ret.Substring(i);
                if (toPascal)
                {
                    ret = ret[0].ToString().ToUpper() + ret.Substring(1);
                }
                else
                {
                    ret = ret[0].ToString().ToLower() + ret.Substring(1);
                }

                if (!codeDomProvider.IsValidIdentifier(ret))
                {
                    ret = string.Concat("_", ret);
                }
            }

            return ret;
        }
    }
}
