﻿using GenericRepository.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.DAL.DbModel
{
    public partial class QuestionAnswer : IEntity<int>
    {
        [NotMapped]
        int IEntity<int>.Id
        {
            get { return this.Id; }
            set { this.Id = value; }
        }
    }
}
