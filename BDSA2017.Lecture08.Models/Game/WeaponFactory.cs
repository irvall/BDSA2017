﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BDSA2017.Lecture08.Models.Game
{
    public class WeaponFactory
    {
        public IWeapon Make(string name)
        {
            var w = typeof(IWeapon).GetTypeInfo();

            var type = w.Assembly
                        .GetTypes()
                        .Where(t => t.IsClass)
                        .Where(t => w.IsAssignableFrom(t))
                        .Where(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();

            return type == null ? null : (IWeapon) Activator.CreateInstance(type);
        }

        public IEnumerable<string> Available()
        {
            var w = typeof(IWeapon).GetTypeInfo();

            return w.Assembly
                    .GetTypes()
                    .Where(t => t != typeof(IWeapon))
                    .Where(t => w.IsAssignableFrom(t))
                    .OrderBy(t => t.Name)
                    .Select(t => t.Name);
        }
    }
}
