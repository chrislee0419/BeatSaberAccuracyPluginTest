using BS_Utils.Utilities;

namespace TestBeatSaberPlugin
{
    internal class PluginConfig
    {
        private static Config config = new Config("BS-Test-Plugin");
        private const string AccuracyAverageSection = "AccuracyAverage";

        public static bool enabled 
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Enabled", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Enabled", value);
            }
        }
        public static bool accDisplayBoth
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Accuracy Display Combined", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Accuracy Display Combined", value);
            }
        }
        public static bool accDisplaySeparate
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Accuracy Display Separate", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Accuracy Display Separate", value);
            }
        }

        public static bool prehitAngleDisplayBoth
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Pre-hit Angle Display Both", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Pre-hit Angle Display Both", value);
            }
        }
        public static bool prehitAngleDisplaySeparate
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Pre-hit Angle Display Separate", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Pre-hit Angle Display Separate", value);
            }
        }

        public static bool posthitAngleDisplayBoth
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Post-hit Angle Display Both", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Post-hit Angle Display Both", value);
            }
        }
        public static bool posthitAngleDisplaySeparate
        {
            get
            {
                return config.GetBool(AccuracyAverageSection, "Post-hit Angle Display Separate", true, true);
            }
            set
            {
                config.SetBool(AccuracyAverageSection, "Post-hit Angle Display Separate", value);
            }
        }
    }
}
