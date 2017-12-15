using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambda1Template
{
    public class Data
    {
     //   public string       Language            { get; set; }
        public List<String> Antworten           { get; set; }
        public string       GetDatenNachricht   { get; set; }
        public string       HelpMessage         { get; set; }
        public string       HelpReprompt        { get; set; }
        public string       StopMessage         { get; set; }
        public string       Antwort1Message     { get; set; }
        public string       AufrufMessage       { get; set; }


        public Data()
        {
         //    this.Language = language;
            this.Antworten = new List<string>();
        }
    }
}
