using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Options
{
    public class JwtSettings
    {
        private IConfiguration _config;

        public JwtSettings(IConfiguration config)
        {
            _config = config;    
        }
        public string Secret { get { return _config["JwtSettings:Secret"]; } }
    }
}
