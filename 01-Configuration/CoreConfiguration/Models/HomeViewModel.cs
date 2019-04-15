using System;

namespace CoreConfiguration.Models
{
    public class HomeViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}