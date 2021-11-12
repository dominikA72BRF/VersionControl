﻿using SantaFActory.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaFActory.Entities
{
    public class Present : Toy
    {
        public SolidBrush BoxColor { get; set; }
        public SolidBrush RibbonColor { get; set; }

        public Present (Color boxColor, Color ribbonColor)
        {
            BoxColor = new SolidBrush(boxColor);
            RibbonColor = new SolidBrush(ribbonColor);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(
                    RibbonColor,
                    0,
                    Height * 0.4f,
                    Width,
                    Height * 0.2f
                );
            g.FillRectangle(
                RibbonColor,
                    Width * 0.4f,
                    0,
                    Width * 0.2f,
                    Height 
                );
        }
    }
}
