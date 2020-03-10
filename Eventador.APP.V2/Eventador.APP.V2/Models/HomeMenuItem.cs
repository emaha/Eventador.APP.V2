﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eventador.APP.V2.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Foo
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
