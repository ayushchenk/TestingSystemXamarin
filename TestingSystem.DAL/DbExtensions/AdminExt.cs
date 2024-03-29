﻿using GenericRepository.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.DAL.DbModel
{
    public partial class Admin : IEntity<int>
    {
        [NotMapped]
        int IEntity<int>.Id
        {
            set { this.Id = value; }
            get { return this.Id; }
        }
    }
}
