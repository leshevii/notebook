using System;
namespace Lesson7
{
    public struct Record
    {
        private string title;
        private string description;
        private string author;
        private bool status;
        private DateTime created;
    
        public Record(string title) :
            this(title, string.Empty, string.Empty, false, DateTime.Now)
        { }
        public Record(string title, string description, string author) :
            this(title, description, author, false, DateTime.Now)
        { }
        public Record(string title, string description, string author, bool status) :
            this(title, description, author, status, DateTime.Now)
        { }
        public Record(string title,string description,string author,bool status,DateTime created)
        {
            this.title  = title;
            this.description = description;
            this.author = author;
            this.status = status;
            this.created = created;
        }                

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public string Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public DateTime Created
        {
            get { return this.created; }            
        }
        /// <summary>
        /// Используеться для регулируемой сортировки в Array.Sort
        /// </summary>
        /// <param name="rec1"></param>
        /// <param name="rec2"></param>
        /// <returns></returns>
        public static int CompareByTitleAsc(Record rec1, Record rec2)
        {
            return String.Compare(rec1.title, rec2.title);
        }
        public static int CompareByTitleDesc(Record rec1, Record rec2)
        {
            if (String.Compare(rec1.title, rec2.title) < 0)
            {
                return 1;
            }
            else if (String.Compare(rec1.title, rec2.title) > 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public static int CompareByAuthorAsc(Record rec1, Record rec2)
        {
            return String.Compare(rec1.author, rec2.author);
        }
        public static int CompareByAuthorDesc(Record rec1, Record rec2)
        {
            if (String.Compare(rec1.author, rec2.author) < 0)
            {
                return 1;
            }
            else if (String.Compare(rec1.author, rec2.author) > 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }            
        }

        public static int CompareByStatusAsc(Record rec1, Record rec2)
        {
            if (Convert.ToInt32(rec1.status) > Convert.ToInt32(rec2.status))
            {
                return 1;
            }
            else if (Convert.ToInt32(rec1.status) == Convert.ToInt32(rec2.status))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public static int CompareByStatusDesc(Record rec1, Record rec2)
        {
            if (Convert.ToInt32(rec1.status) > Convert.ToInt32(rec2.status))
            {
                return -1;
            }
            else if (Convert.ToInt32(rec1.status) == Convert.ToInt32(rec2.status))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
