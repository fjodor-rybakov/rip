﻿namespace backend.core.configs
{
    public class DataAuthConfig
    {
        public string Key { get; set; }
        
        public int Lifetime { get; set; }
        
        public string Issuer { get; set; }
        
        public string Audience { get; set; }
    }
}