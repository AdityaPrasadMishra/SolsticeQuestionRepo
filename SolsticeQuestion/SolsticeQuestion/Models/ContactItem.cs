using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SolsticeQuestion.Models
{
    //[ModelBinder(typeof(JSONWithFilesMultiPartModelBinder), Name = "json")]
    public class ContactItem
    {
            public long Id { get; set; }
            [Required]
            public string Name { get; set; }
            public string Company { get; set; }
            public string ProfileImage { get; set; }
            public DateTime BirthDate { get; set; }
            [Phone]
            public string HomePhoneNumber { get; set; }
            [Phone]
            public string WorkPhoneNumber { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            [EmailAddress]
            public string Email { get; set; }

    }
}
