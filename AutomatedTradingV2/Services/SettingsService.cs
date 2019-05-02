using AutomatedTradingV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Services
{
    public static class SettingsService
    {
        public static void CreateSettings(DateTime startTime, Dictionary<Symbol, StrategyAbstract> strategies, int expiriationAfterXTicks)
        {
            Settings settings = new Settings(startTime, strategies, expiriationAfterXTicks);
        }

        public static void SaveSettings(Settings settings, string folderPath)
        {

        }


        public static Settings LoadSettings()
        {
            return null;
        }
    }
}
