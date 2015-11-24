﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace System.Globalization.Tests
{
    internal static class NumberFormatInfoData
    {
        private static bool s_isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        private static int s_WindowsVersion = DateTimeFormatInfoData.GetWindowsVersion();
        private static bool s_isOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static int[] GetNumberGroupSizes(CultureInfo cultureInfo)
        {
            if (string.Equals(cultureInfo.Name, "en-US", StringComparison.OrdinalIgnoreCase))
            {
                return new int[] { 3 };
            }
            if (string.Equals(cultureInfo.Name, "ur-IN", StringComparison.OrdinalIgnoreCase))
            {
                if (s_isOSX || (s_isWindows && s_WindowsVersion >= 10))
                {
                    return new int[] { 3 };
                }
                else
                {
                    return new int[] { 3, 2 };
                }
            }

            throw DateTimeFormatInfoData.GetCultureNotSupportedException(cultureInfo);
        }

        internal static string GetNegativeInfinitySymbol(CultureInfo cultureInfo)
        {
            if (s_isWindows && s_WindowsVersion < 10)
            {
                if (string.Equals(cultureInfo.Name, "en-US", StringComparison.OrdinalIgnoreCase))
                {
                    return "-Infinity";
                }
                if (string.Equals(cultureInfo.Name, "fr-FR", StringComparison.OrdinalIgnoreCase))
                {
                    return "-Infini";
                }

                throw DateTimeFormatInfoData.GetCultureNotSupportedException(cultureInfo);
            }
            else
            {
                return "-\u221E";
            }
        }

        internal static int[] GetCurrencyNegativePatterns(CultureInfo cultureInfo)
        {
            // CentOS uses an older ICU than Ubuntu, which means the "Linux" values need to allow for
            // multiple values, since we can't tell which version of ICU we are using, or whether we are
            // on CentOS or Ubuntu.
            // When multiple values are returned, the "older" ICU value is returned last.

            switch (cultureInfo.Name)
            {
                case "en-US":
                    return s_isWindows ? new int[] { 0 } : new int[] { 1, 0 };

                case "en-CA":
                    return s_isWindows ? new int[] { 1 } : new int[] { 1, 0 };

                case "fa-IR":
                    return s_isWindows ? new int[] { 3 } : new int[] { 1, 0 };

                case "fr-CD":
                    if (s_isWindows)
                    {
                        return (s_WindowsVersion < 10) ? new int[] { 4 } : new int[] { 8 };
                    }
                    else
                    {
                        return new int[] { 8, 15 };
                    }

                case "as":
                    return s_isWindows ? new int[] { 12 } : new int[] { 9 };

                case "es-BO":
                    return (s_isWindows && s_WindowsVersion < 10) ? new int[] { 14 } : new int[] { 1 };

                case "fr-CA":
                    return s_isWindows ? new int[] { 15 } : new int[] { 8, 15 };
            }

            throw DateTimeFormatInfoData.GetCultureNotSupportedException(cultureInfo);
        }
    }
}
