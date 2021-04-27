﻿namespace SL2021.Application.Identity.Contracts
{
    public static class CreateToken
    {
        public class Request
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string Token { get; set; }
        }
    }
}
