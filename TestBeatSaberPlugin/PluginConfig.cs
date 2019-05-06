using BS_Utils.Utilities;

namespace TestBeatSaberPlugin
{
    internal class PluginConfig
    {
        private static Config config = new Config("AccuracyAverageTestPlugin");
        public static bool enabled 
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Enabled", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Enabled", value);
            }
        }
        public static bool accDisplayBoth
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Accuracy Display Combined", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Accuracy Display Combined", value);
            }
        }
        public static bool accDisplaySeparate
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Accuracy Display Separate", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Accuracy Display Separate", value);
            }
        }

        public static bool prehitAngleDisplayBoth
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Pre-hit Angle Display Both", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Pre-hit Angle Display Both", value);
            }
        }
        public static bool prehitAngleDisplaySeparate
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Pre-hit Angle Display Separate", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Pre-hit Angle Display Separate", value);
            }
        }

        public static bool posthitAngleDisplayBoth
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Post-hit Angle Display Both", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Post-hit Angle Display Both", value);
            }
        }
        public static bool posthitAngleDisplaySeparate
        {
            get
            {
                return config.GetBool("AccuracyAverageTestPlugin", "Post-hit Angle Display Separate", true, true);
            }
            set
            {
                config.SetBool("AccuracyAverageTestPlugin", "Post-hit Angle Display Separate", value);
            }
        }
    }
}
