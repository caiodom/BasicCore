﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Communication
{
   public class ResponseResult
    {

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
        

        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }

        
    }
}
