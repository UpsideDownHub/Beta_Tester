﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Assets.Scripts.DAL
{
    public class Phase
    {
        [DisplayName("Key")]
        public int PhaseId { get; set; }
        public string FileId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int Played { get; set; }
        public int Dies { get; set; }
        public int Completed { get; set; }
        public bool Tested { get; set; }
    }
}