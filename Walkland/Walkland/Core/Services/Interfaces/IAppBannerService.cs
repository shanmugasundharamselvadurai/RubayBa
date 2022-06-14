﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Walkland.Core.Services.Interfaces
{
    public interface IAppBannerService
    {
        Task<List<AppBanner>> GetAppBanners();
    }
}