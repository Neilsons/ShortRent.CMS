﻿using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
   public interface IHistoryOperatorService
    {
        void CreateHistoryOperator(HistoryOperator model);
    }
}
