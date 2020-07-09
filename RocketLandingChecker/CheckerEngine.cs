using System;
using System.Collections.Generic;
using System.Drawing;

namespace RocketLandingChecker
{
    public class CheckerEngine
    {
        private readonly Size _platform;

        private Dictionary<int, Point> lastCheckedPositions;
        public static Size DefaultPlatform => new Size() { Width = 10, Height = 10 };
        public static Size LandingArea => new Size() { Width = 100, Height = 100 };
        public static Point PlatformPosition => new Point(5, 5);

        public CheckerEngine() : this(DefaultPlatform)
        {
        }

        public CheckerEngine(Size platform)
        {
            _platform = platform;
             lastCheckedPositions = new Dictionary<int, Point>();
        }

        public string CheckPosition(int rocketId, Point position)
        {
            if (IsPositionOutsidePlatform(position))
                return ReturnCodes.OutOfPlatform;

            if (HasPositionAlreadyBeenChecked(rocketId, position))
                return ReturnCodes.Clash;

            if (IsPositionNextToAny(rocketId, position))
                return ReturnCodes.Clash;

            UpdateLastCheckedPosition(rocketId, position);

            return ReturnCodes.OkForLanding;
        }

        private bool IsPositionNextToAny(int rocketId, Point position)
        {
            foreach (KeyValuePair<int, Point> kvp in lastCheckedPositions)
            {
                if (kvp.Key != rocketId)
                {
                    if (IsPositionNextTo(position, kvp.Value))
                        return true;
                }
            }
            return false;
        }

        private bool IsPositionNextTo(Point position, Point value)
        {
            return
                (position.X == value.X - 1 && position.Y == value.Y - 1) ||
                (position.X == value.X && position.Y == value.Y - 1) ||
                (position.X == value.X + 1 && position.Y == value.Y - 1) ||
                (position.X == value.X - 1 && position.Y == value.Y) ||
                (position.X == value.X + 1 && position.Y == value.Y) ||
                (position.X == value.X - 1 && position.Y == value.Y + 1) ||
                (position.X == value.X && position.Y == value.Y + 1) ||
                (position.X == value.X + 1 && position.Y == value.Y + 1);
        }

        private bool HasPositionAlreadyBeenChecked(int rocketId, Point position)
        {
            foreach (KeyValuePair<int, Point> kvp in lastCheckedPositions)
            {
                if (kvp.Key != rocketId && kvp.Value == position)
                    return true;
            }
            return false;
        }

        private void UpdateLastCheckedPosition(int rocketId, Point position)
        {
            if (lastCheckedPositions.ContainsKey(rocketId))
                lastCheckedPositions[rocketId] = position;
            else
                lastCheckedPositions.Add(rocketId, position);
        }

        private bool IsPositionOutsidePlatform(Point position) =>
            !(position.X >= PlatformPosition.X && position.X <= PlatformPosition.X + _platform.Width &&
            position.Y >= PlatformPosition.Y && position.Y <= PlatformPosition.Y + _platform.Height);
    }
}
