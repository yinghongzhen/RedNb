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
using Newtonsoft.Json;

namespace RedNb.Auth.Application.Employees
{
    public class EmployeeCreatedEvent : ILocalEventHandler<EntityCreatedEventData<Employee>>, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;

        public EmployeeCreatedEvent(IObjectMapper objectMapper)
        {
            _objectMapper = objectMapper;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<Employee> eventData)
        {
            var model = eventData.Entity;

            var employeeDto = _objectMapper.Map<Employee, EmployeeOutputDto>(model);

            var json = JsonConvert.SerializeObject(employeeDto);

            RedisHelper.StringSet(RedisKeyManger.GetEmployeeKey(model.UserId.ToString()), json, null);
        }
    }
}
