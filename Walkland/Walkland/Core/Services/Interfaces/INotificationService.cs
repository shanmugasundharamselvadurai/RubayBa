using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walkland.Core.Models;

namespace Core.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotification();
    }
}
