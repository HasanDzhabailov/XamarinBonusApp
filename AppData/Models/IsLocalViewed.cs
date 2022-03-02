using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.AppData.Models
{
    public class IsLocalViewed
    {
        public static int _viewedParentId;
        public static int ViewedParentId
        {
            get
            {
                return _viewedParentId;
            }
            set
            {
                _viewedParentId = value;
            }
        }

        public static int _viewedChildrenId;
        public static int ViewedChildrenId
        {
            get
            {
                return _viewedChildrenId;
            }
            set
            {
                _viewedChildrenId = value;
            }
        }

        public static int _timerForProgressBar;
        public static int TimerForProgressBar
        {
            get
            {
                return _timerForProgressBar;
            }
            set
            {
                _timerForProgressBar = value;
            }
        }

        public static bool _isTransitionElements = false;
        public static bool TransitionElements
        {
            get
            {
                return _isTransitionElements;
            }
            set
            {
                _isTransitionElements = value;
            }
        }

        public static bool _isTestElements = false;
        public static bool TestElements
        {
            get
            {
                return _isTestElements;
            }
            set
            {
                _isTestElements = value;
            }
        }
    }
}
