﻿using Lagou.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API {
    public enum ErrorTypes {
        Unknow,

        /// <summary>
        /// 404
        /// </summary>
        [ErrorTag("404")]
        HttpNotFound,

        /// <summary>
        /// 500
        /// </summary>
        [ErrorTag("500")]
        ServerException,

        [ErrorTag("-2146233079")]
        DNSError
    }
}