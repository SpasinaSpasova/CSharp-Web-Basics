﻿using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Models
{
    public class BoardViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
