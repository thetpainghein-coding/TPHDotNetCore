﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHDotNetCore.MvcApp2.Models;

[Table("Tbl_Blog")]
public class BlogModel
{
    [Key]
    public int? BlogId { get; set; }

    public string? BlogTitle { get; set; }

    public string? BlogAuthor { get; set; }

    public string? BlogContent { get; set; }
}

public class MessageModel
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}

//public record BlogEntity(int BlogId, string BlogName,string BlogAuthor, string BlogContent);
