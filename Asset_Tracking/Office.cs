﻿

namespace Asset_Tracking
{
    public class Office
    {
        public Office(string country, string currency)
        {
            Country = country;
            Currency = currency;
        }

        public string Country { get; set; }

        public string Currency { get; set; }
    }
}
