﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Samples.SPASite;

public class SignMemberInCommand : ICommand
{
    [Required]
    [EmailAddress(ErrorMessage = "Please use a valid email address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}