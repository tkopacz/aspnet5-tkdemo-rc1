using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKDemoMVC.Models
{
    public class BaseEntity {
        public int Id { get; set; }
    }

    public class Entity1:BaseEntity {
        public string Title { get; set; }
    }

    public interface IRepository<T> where T : BaseEntity {
        int Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        T[] Get();
        T FindById(int id);
    }

    public class RepositoryMem<T> : IRepository<T> where T : BaseEntity {

        List<T> m_lst = new List<T>();

        public int Add(T obj) {
            int maxid = 1;
            if (m_lst.Count>0) maxid = m_lst.Max(p => p.Id) + 1;
            obj.Id = maxid;
            m_lst.Add(obj);
            return maxid;
        }

        public void Delete(T obj) {
            throw new NotImplementedException();
        }

        public T FindById(int id) {
            throw new NotImplementedException();
        }

        public T[] Get() {
            return m_lst.ToArray();
        }

        public void Update(T obj) {
            throw new NotImplementedException();
        }
    }


    public class InMemoryModel: BaseEntity {
        public int MyProperty { get; set; }
        public string Title { get; set; }
        public DateTime MyDateTime { get; set; }
    }
}
