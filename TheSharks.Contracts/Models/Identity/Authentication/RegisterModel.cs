﻿namespace TheSharks.Contracts.Models.Identity.Authentication;

public class RegisterModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}