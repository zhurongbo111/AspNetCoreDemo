using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConfiguration.Options
{
    public class EmailOption
    {
        public string From { get; set; }

        public string To { get; set; }
    }

    public class MultiEmailOption : EmailOption
    {
        public string Body { get; set; }

        public bool UseSSL { get; set; }

        public string Subject { get; set; }

        public string BodyType { get; set; }
    }
}
