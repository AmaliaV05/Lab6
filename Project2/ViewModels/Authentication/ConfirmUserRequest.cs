﻿namespace Project2.ViewModels.Authentication
{
    public class ConfirmUserRequest
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public ConfirmUserRequest(string Email, string ConfirmationToken)
        {
            this.Email = Email;
            this.ConfirmationToken = ConfirmationToken;
        }
    }
}
