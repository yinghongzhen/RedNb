using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Core.Contracts
{
    public class LoginUserDto
    {
        /// <summary>
        /// ƽ̨��ʶ
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// �û����
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// �ǳ�
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// �û����ñ��
        /// </summary>
        public long? ReferenceId { get; set; }

        /// <summary>
        /// �û���������
        /// </summary>
        public string ReferenceName { get; set; }

        /// <summary>
        /// �⻧���
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// �⻧����
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public long Exp { get; set; }
    }
}
