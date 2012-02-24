using System;
using System.ComponentModel.DataAnnotations;
using Forum.CustomAttributes;
using MvcPaging;

namespace Forum.Models
{
    public class ResumeModel
    {
        [Required]
        [Email]
        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        public string FileName { get; set; }

        //[Required]
        public string FileId { get; set; }
    }

    public class ResumeListItemModel
    {
        public string FileId { get; set; }

        public string FileName { get; set; }

        public string Email { get; set; }

        public DateTime UploadDate { get; set; }

        public string Description { get; set; }
    }

    public class ResumeListModel
    {
        public IPagedList<ResumeListItemModel> Resumes { get; set; }
    }
}