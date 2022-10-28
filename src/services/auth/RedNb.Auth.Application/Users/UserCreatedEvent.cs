using RedNb.Auth.Domain.Admins;
using RedNb.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace RedNb.Auth.Application.Users
{
    public class UserCreatedEvent : ILocalEventHandler<EntityCreatedEventData<User>>, ITransientDependency
    {
        public async Task HandleEventAsync(EntityCreatedEventData<User> eventData)
        {
            var json = JsonSerializer.Serialize(eventData.Entity);
        }
    }
}
