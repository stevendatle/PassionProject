using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passionproject.Models
{
    //This class describes a class in World of Warcraft
    public class Class
    {
        [Key]

        public int ClassID { get; set; }

        public string ClassName { get; set; }

        public string ClassSpec { get; set; }

        public bool ClassPic { get; set; }

        public string PicExtension { get; set; }

        //A class can play for multiple comps

        [ForeignKey("Comp")]
        public int CompID { get; set; }

        public virtual Comp Comp { get; set; }


    }

    public class ClassDto
    {
        public int ClassID { get; set; }

        [DisplayName("Class Name")]
        public string ClassName { get; set; }

        [DisplayName("Class Specialization")]
        public string ClassSpec { get; set; }

        public bool ClassPic { get; set; }

        public string PicExtension { get; set; }

        public int CompID { get; set; }
    }
}