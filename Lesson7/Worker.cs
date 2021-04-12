using System;
namespace Lesson7
{
    public struct Worker
    {
        private string fName;
        private string lName;
        private string position;

        public Worker(string fName,string lName,string position)
        {
            this.fName = fName;
            this.lName = lName;
            this.position = position;
        }

        public string FName
        {
            get { return this.fName; }            
        }

        public string LName
        {
            get { return this.lName; }
            set { this.LName = value; }
        }

        public string Postition
        {
            get { return this.position; }
            set { this.position = value; }
        }
    }
}
