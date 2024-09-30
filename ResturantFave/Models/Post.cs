using System;
using System.Collections.Generic;

namespace ResturantFave.Models;

public partial class Post
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Resturant { get; set; }

    public int? Rating { get; set; }

    public bool? Orderagain { get; set; }
}
