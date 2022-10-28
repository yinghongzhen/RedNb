using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Apis.Dtos
{
    public class ApiAddInputDto
    {
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// ��ַ
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// �ӿڷ���
        /// </summary>
        [Required]
        public EHttpMethod Method { get; set; }

        /// <summary>
        /// ʹ��״̬
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// ��ǩ
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Tags { get; set; }

        /// <summary>
        /// ģ����
        /// </summary>
        public long ModuleId { get; set; }
    }
}
