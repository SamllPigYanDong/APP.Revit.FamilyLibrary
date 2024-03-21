using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Revit.Entity.Entity.Dtos;

namespace Revit.Service.Services
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }


        public long Id { get; set; }

        public long CreatorId { get; set; }


        public DateTime CreationTime { get; set; } = DateTime.Now;

        public DateTime LastModificationTime { get; set; } = DateTime.Now;

        public LoginedUserDto UserDto { get; set; }

        public int ProjectUserCount { get; set; }

        public double DocumentsSize { get; set; }
    }
}