using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ReportGenerator.Entity.Table
{
    public abstract class Table<T>: IEnumerable<T> where T: Entity, new()
    {
        public List<T> Items { get; set; } = new List<T>();

        public Table(List<T> items)
        {
            Items = items;
        }

        public Table()
        {

        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public T GetById(int id)
        {
            return Items.Find(x => x.Id == id);
        }

        public T GetByName(string name)
        {
            return Items.Find(x => x.Name == name);
        }

        /// <summary>
        /// Returns a dictionary of entities ids and names
        /// </summary>
        public Dictionary<int, string> GetDictionary()
        {
            var dictionary = new Dictionary<int, string>();
            foreach (var item in Items)
            {
                dictionary.Add(item.Id, item.Name);
            }

            return dictionary;
        }

        /// <summary>
        /// Gets entities from the provided JSON object
        /// </summary>
        /// <param name="data">JSON data to parse</param>
        /// <param name="arrayName">Name of the array with entities</param>
        /// <returns>Number of ignored entities</returns>
        public virtual int FillFromJson(JObject data, string arrayName)
        {
            var ignored = 0;
            foreach (var item in (JArray)data[arrayName])
            {
                var entity = new T
                {
                    Id = (int)item["id"],
                    Name = (string)item["name"]
                };
                
                if (GetById(entity.Id) == null)
                {
                    Add(entity);
                }
                else
                {
                    ignored++;
                }
            }

            return ignored;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
