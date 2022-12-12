using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Offices;
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
using Volo.Abp.ObjectMapping;
using RedNb.Auth.Application.Contracts.Employees.Dtos;

namespace RedNb.Auth.Application.Employees
{
    public class EmployeeUpdatedEvent : ILocalEventHandler<EntityUpdatedEventData<Employee>>, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;

        public EmployeeUpdatedEvent(IObjectMapper objectMapper)
        {
            _objectMapper = objectMapper;
        }

        public async Task HandleEventAsync(EntityUpdatedEventData<Employee> eventData)
        {
            var model = eventData.Entity;

            var employeeDto = _objectMapper.Map<Employee, EmployeeOutputDto>(model);

            var json = JsonSerializer.Serialize(eventData.Entity);

            RedisHelper.StringSet(RedisKeyManger.GetEmployeeKey(model.Id.ToString()), json, null);
        }
    }
}
