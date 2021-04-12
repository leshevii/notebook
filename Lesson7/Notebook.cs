using System;
using System.IO;
namespace Lesson7
{
    public struct Notebook
    {
        public enum Field { title,author,status,created }        

        private Record[] records;
        private int index;

        public Notebook(byte count = 1)
        {            
            records = new Record[count];            
            index = 0;
        }
        public Notebook(Record[] records)
        {
            this.records = records;
            index = records.Length;
        }
        public Record[] Records
        {
            get { return records; }
        }

        public int[] findByTitle(string name)
        {
            int[] temp = new int[0];
            int index = 0;

            for (int i = 0;i<Records.Length;i++)
            {
                if(records[i].Title.ToLower() == name.ToLower())
                {
                    Array.Resize<int>(ref temp, temp.Length + 1);
                    temp[index++] = i;                    
                }
            }
            return temp;
        }
        public int[] findByAuthor(string name)
        {
            int[] temp = new int[0];
            int index = 0;

            for (int i = 0; i < Records.Length; i++)
            {
                if (records[i].Author.ToLower() == name.ToLower())
                {
                    Array.Resize<int>(ref temp, temp.Length + 1);
                    temp[index++] = i;                                       
                }
            }
            return temp;
        }
        public int[] findByStatus(bool status)
        {
            int[] temp = new int[0];
            int index = 0;

            for (int i = 0; i < Records.Length; i++)
            {
                if (records[i].Status == status)
                {
                    Array.Resize<int>(ref temp, temp.Length + 1);
                    temp[index++] = i;
                }
            }
            return temp;
        }        

        public void Delete(int index)
        {            
            Record[] temp = new Record[Records.Length - 1];
            int ind = 0;           

            for (int i = 0; i < Records.Length; i++)
            {
                if (i != index)
                {
                    temp[ind++] = Records[i];
                }
            }
            Array.Resize<Record>(ref records, temp.Length);
            records = temp;
        }
        public void Delete(int[] indexes)
        {
            Record[] temp = new Record[Records.Length - indexes.Length];
            
            int index = 0;
            int count = 0;

            for(int i = 0; i < Records.Length; i++)
            {
                count = 0;
                foreach(int curIndex in indexes)
                {
                    if (i != curIndex)
                    {
                        count++;
                    }
                }

                if (count == indexes.Length)
                {
                    temp[index++] = records[i];
                }
            }

            Array.Resize<Record>(ref records, temp.Length);
            records = temp;
        }
        public void Delete(Field field,string value)
        {
            if(field == Field.title)
            {
                Delete(findByTitle(value));
            }
            else if(field == Field.author)
            {
                Delete(findByAuthor(value));                
            }
            else if(field == Field.status)
            {
                Delete(findByStatus(Convert.ToBoolean(value)));
            }
        }
        public void Reorder(Field field,bool order)
        {
            if(Field.title == field)
            {
                if (order == true)
                {
                    Array.Sort(Records, Record.CompareByTitleAsc);
                }
                else
                {
                    Array.Sort(Records, Record.CompareByTitleDesc);
                }
            }
            else if(Field.author == field)
            {
                if (order == true)
                {
                    Array.Sort(Records, Record.CompareByAuthorAsc);
                }
                else
                {
                    Array.Sort(Records, Record.CompareByAuthorDesc);
                }
            }
            else if(Field.status == field)
            {
                if (order == true)
                {
                    Array.Sort(Records, Record.CompareByStatusAsc);
                }
                else
                {
                    Array.Sort(Records, Record.CompareByStatusDesc);
                }
            }
        }

        public void AddRecord(string title,string desc,string author,bool status = false)
        {
            if (index + 1 >= records.Length)
            {
                Array.Resize<Record>(ref records, records.Length + 1);
            }
            records[index] = new Record(title, desc, author, status);                        
        }
        public void AddRecord(string title, string desc, string author, bool status,DateTime created)
        {
            if (index >= records.Length)
            {
                Array.Resize<Record>(ref records, records.Length + 1);
            }
            records[index++] = new Record(title, desc, author, status,created);
        }
        public void AddRecord(Record rec)
        {
            if (index >= records.Length)
            {
                Array.Resize<Record>(ref records, records.Length + 1);
            }
            records[index++] = rec;
        }
        public void Edit(int index, string title,string desc=null, string author=null,bool status=false)
        {            
            if (title != null)
                records[index].Title = title;
            if (desc != null)
                records[index].Description = desc;
            if (author != null)
                records[index].Author = author;
            if (status != false)
                records[index].Status = true;            

        }
        public void Save(string path,bool status=true)
        {
            FileStream fs;
            StreamWriter writer;

            string str = string.Empty;

            if(status)
               fs = new FileStream(path, FileMode.Append);
            else
               fs = new FileStream(path, FileMode.Create);

            writer = new StreamWriter(fs);
            

            foreach (Record rec in records)
            {
                str = rec.Title + ";" + rec.Description + ";" + rec.Author + ";" + rec.Status + ";" + rec.Created.ToShortDateString()+";";
                writer.WriteLine(str);
            }
            writer.Close();
        }
        public void Load(string path)
        {
            string[] str,strDate;
            using(StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().Split(';');
                    strDate = str[4].Split('.');                    
                    DateTime date = new DateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]), Convert.ToInt32(strDate[0]));
                    AddRecord(str[0], str[1], str[2], Convert.ToBoolean(str[3]), date);
                }
            }
        }
        public void Load(string path,DateTime from, DateTime to)
        {
            string[] str, strDate;
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().Split(';');
                    strDate = str[4].Split('.');
                    DateTime date = new DateTime(Convert.ToInt32(strDate[2]), Convert.ToInt32(strDate[1]), Convert.ToInt32(strDate[0]));

                    if(date >= from && date <= to)
                       AddRecord(str[0], str[1], str[2], Convert.ToBoolean(str[3]), date);
                }
            }
        }
        
        public void Print()
        {
            string str = string.Empty;
            int index = 0;
            Console.WriteLine("------------------------------------------------------------------------------------------");
            foreach(Record rec in Records)
            {
                if(rec.Status)
                   str = rec.Title + " - " + rec.Description + " - " + rec.Author + " - " + "Решен" + " - " + rec.Created.ToShortDateString();
                else
                   str = rec.Title + " - " + rec.Description + " - " + rec.Author + " - " + "Не решен" + " - " + rec.Created.ToShortDateString();
                Console.WriteLine(index+") - "+str);
                index++;
            }
            Console.WriteLine("------------------------------------------------------------------------------------------");
        }
    }
}
