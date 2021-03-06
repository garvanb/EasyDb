﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows;

namespace EasyDb
{
    public class MouseDragPopupBehaviour : Behavior<Popup>
    {
        private bool mouseDown;
        private Point oldMousePosition;

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += (s, e) =>
            {
                mouseDown = true;
                oldMousePosition = AssociatedObject.PointToScreen(e.GetPosition(AssociatedObject));
                AssociatedObject.Child.CaptureMouse();
            };
            AssociatedObject.MouseMove += (s, e) =>
            {
                if (!mouseDown) return;
                var newMousePosition = AssociatedObject.PointToScreen(e.GetPosition(AssociatedObject));
                var offset = newMousePosition - oldMousePosition;
                oldMousePosition = newMousePosition;
                AssociatedObject.HorizontalOffset += offset.X;
                AssociatedObject.VerticalOffset += offset.Y;
            };
            AssociatedObject.MouseLeftButtonUp += (s, e) =>
            {
                mouseDown = false;
                AssociatedObject.Child.ReleaseMouseCapture();
            };
        }
    }
}
