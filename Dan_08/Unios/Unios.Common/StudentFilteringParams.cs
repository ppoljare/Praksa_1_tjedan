﻿using System;

namespace Unios.Common
{
    public class StudentFilteringParams : IStudentFilteringParams
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Fakultet { get; set; }
        public string Godina { get; set; }
    }
}
