﻿using System.ComponentModel.DataAnnotations;

namespace TestingSystem.BusinessModel.Model
{
    public class QuestionAnswerDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1024)]
        public string AnswerString { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public bool IsPicked { set; get; }

        public PickedCheckbox PickedCheckbox { set; get; }
    }

    public struct PickedCheckbox
    {
        public int AnswerId { set; get; }
        public bool Checked { set; get; }
    }
}
