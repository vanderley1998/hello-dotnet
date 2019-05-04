﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.Plans
{
    public class PlanHistory
    {
        public int Id { get; set; }
        public Plan Plan { get; set; }
        public PlanStatus PlanStatus { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id}, [Plan: {Plan.Id}, User: {Plan.User.Id}], [PlanStatus {PlanStatus.Id}], Date: {Date}]";
        }
    }
}
