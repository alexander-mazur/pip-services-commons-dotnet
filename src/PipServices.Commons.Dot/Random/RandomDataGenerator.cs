//using System;
//using System.Collections.Generic;
//using PipServices.Commons.Convert;

//namespace PipServices.Commons.Data
//{
//    public abstract class RandomDataGenerator<T> : RandomData
//    {
//        public abstract T Create(DataReferences references = null);
//        public abstract T Update(T item, DataReferences references = null);
//        public abstract T Delete(T item, DataReferences references = null);

//        public virtual T Clone(T item)
//        {
//            var temp = JsonConverter.ToJson(item);
//            return JsonConverter.FromJson<T>(temp);
//        }

//        public List<T> CreateList(int minSize, int maxSize = 0, DataReferences references = null)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);
//            List<T> items = new List<T>();

//            for (int index = 0; index < size; index++)
//            {
//                items.Add(Create(references));
//            }

//            return items;
//        }

//        public List<T> AppendList(List<T> items, int minSize, int maxSize = 0, DataReferences references = null)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);
//            //items = new List<T>(items);

//            for (int index = 0; index < size; index++)
//            {
//                var item = Create();
//                items.Add(item);
//            }

//            return items;
//        }

//        public List<T> UpdateList(List<T> items, int minSize, int maxSize = 0, DataReferences references = null)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);
//            //items = new List<T>(items);

//            for (int index = 0; items.Count > 0 && index < size; index++)
//            {
//                var item = items[Integer(items.Count)];
//                Update(item, references);
//            }

//            return items;
//        }

//        public List<T> ReduceList(List<T> items, int minSize, int maxSize = 0, DataReferences references = null)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);
//            //items = new List<T>(items);

//            for (int index = 0; items.Count > 0 && index < size; index++)
//            {
//                var pos = Integer(items.Count);
//                var item = Delete(items[pos], references);
//                if (item == null) items.RemoveAt(pos);
//            }

//            return items;
//        }

//        public List<T> ChangeList(List<T> items, int minSize, int maxSize = 0, DataReferences references = null)
//        {
//            maxSize = Math.Max(minSize, maxSize);
//            int size = Integer(minSize, maxSize);
//            //items = new List<T>(items);

//            for (int index = 0; index < size; index++)
//            {
//                int choice = Integer(5);
//                if (choice == 1 && items.Count > 0)
//                {
//                    var pos = Integer(items.Count);
//                    var item = Delete(items[pos], references);
//                    if (item == null) items.RemoveAt(pos);
//                }
//                else if (choice == 3)
//                {
//                    var item = Create();
//                    items.Add(item);
//                }
//                else if (items.Count > 0)
//                {
//                    var item = items[Integer(items.Count)];
//                    Update(item, references);
//                }
//            }

//            return items;
//        }
//    }
//}
