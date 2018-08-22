﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace CxFlatUI
{
    public class CxFlatTabControl : TabControl
    {
        #region 变量
        int enterIndex;
        bool enterFlag = false;

        #endregion

        #region 属性

        private Color _themeColor = ThemeColors.PrimaryColor;
        public Color ThemeColor
        {
            get { return _themeColor; }
            set
            {
                _themeColor = value;
                Invalidate();
            }
        }
        #endregion


        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 4, rect.Top - 4, rect.Width + 8, rect.Height + 8);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            enterFlag = true;
            for (int i = 0; i < TabCount; i++)
            {
                var tempRect = GetTabRect(i);
                if (tempRect.Contains(e.Location))
                {
                    enterIndex = i;
                }
            }
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            enterFlag = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Color.White);

            for (int i = 0; i < TabCount; i++)
            {
                if (i == SelectedIndex)
                {
                    graphics.FillRectangle(new SolidBrush(_themeColor), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                    graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(_themeColor), GetTabRect(i), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                }
                else
                {
                    if (i == enterIndex && enterFlag)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, _themeColor)), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                    }

                    graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(Color.Black), GetTabRect(i), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                }
            }
        }

        public CxFlatTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12F);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(120, 40);

        }
    }
}