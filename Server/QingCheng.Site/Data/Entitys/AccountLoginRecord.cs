﻿using QingCheng.Tools.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Data.Entitys
{
    public class AccountLoginRecord : BaseTimeTable
    {
        public int Id { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public Accounts Account { get; set; }

        public string UA { get; set; }

        public string Ip { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool IsLogin { get; set; }
    }
}