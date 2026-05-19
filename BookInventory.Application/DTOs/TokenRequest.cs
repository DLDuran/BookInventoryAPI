using System;
using System.Collections.Generic;
using System.Text;

namespace BookInventory.Application.DTOs
{
    public class TokenRequest
    {
        public string AccessToken {  get; set; }
        public string RefreshToken { get; set; }
    }
}
