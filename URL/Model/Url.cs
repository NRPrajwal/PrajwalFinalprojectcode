﻿namespace URL.Model
{
    public class Url
    {

        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string shortUrl { get; set; }
    }
    public class RequestPayload
    { public Uri OriginalUrl { get; set; } }
}
