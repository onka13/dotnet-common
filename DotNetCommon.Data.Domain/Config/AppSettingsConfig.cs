﻿using System.Collections.Generic;

namespace DotNetCommon.Data.Domain.Config;

/// <summary>
/// App configuration model which defined in appsettings.
/// </summary>
public class AppSettingsConfig
{
    public List<string> TestEmailReceivers { get; set; }

    public string Url { get; set; }
}
