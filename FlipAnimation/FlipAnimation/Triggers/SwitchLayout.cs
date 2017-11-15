﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlipAnimation.Triggers
{

    public class SwitchLayout : TriggerAction<Button>
    {
        public enum AnimationDirection
        {
            Left,
            Right
        }

        protected override async void Invoke(Button sender)
        {
            View Viewhide = null;
            View Viewshow = null;

            if (TargetElement != null)
            {
                // This is For Finding TargetView
                Viewshow = ((View)sender.Parent.Parent).FindByName<View>(TargetElement);

                if (Viewshow != null)
                {
                    // This is For Finding SourceView
                    if (SourceElement != null)
                    {
                        Viewhide = ((View)sender.Parent.Parent).FindByName<View>(SourceElement);

                        if (Viewhide != null)
                        {
                            await PerformAnimation(Viewhide, Viewshow);
                        }
                    }
                }
            }
        }

        public string SourceElement { get; set; }
        public string TargetElement { get; set; }
        public AnimationDirection Direction { get; set; }

        private async Task PerformAnimation(View ViewElementHide, View ViewElementShow)
        {
            int hideStart = 0;
            int hideStop = (Direction == AnimationDirection.Left ? -90 : 90);
            int showStart = (Direction == AnimationDirection.Left ? 90 : 270);
            int showStop =(Direction == AnimationDirection.Left ? 0 : 360);

            await ViewElementHide.RotateYTo(hideStart, 0);
            await ViewElementShow.RotateYTo(showStart, 0);
            await ViewElementShow.ScaleTo(0.2, 0);
            ViewElementShow.IsVisible = true;    // This is rotated at 90 or 270 degrees

            // Animate
            ViewElementHide.FadeTo(0.5, 100, Easing.SinOut);
            ViewElementHide.ScaleTo(0.2, 100, Easing.Linear);
            await ViewElementHide.RotateYTo(hideStop, 150, Easing.Linear);

            ViewElementShow.FadeTo(1, 100, Easing.SinIn);
            ViewElementShow.ScaleTo(1, 150, Easing.Linear);
            await ViewElementShow.RotateYTo(showStop, 150, Easing.Linear);

            ViewElementHide.IsVisible = false;
        }
    }


}
