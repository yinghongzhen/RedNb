using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;

namespace RedNb.Auth.Application.Contracts.Apis.Dtos
{
    public class ApiOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ַ
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// �ӿڷ���
        /// </summary>
        public EHttpMethod Method { get; set; }

        public string MethodStr { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// ��ǩ
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// ģ����
        /// </summary>
        public long ModuleId { get; set; }
    }
}
