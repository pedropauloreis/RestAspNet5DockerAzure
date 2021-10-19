using Microsoft.EntityFrameworkCore;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Model.Context;
using RestAspNet5DockerAzure.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAspNet5DockerAzure.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private DbSet<Person> dataset;

        public PersonRepository(MySQLContext context) : base(context)
        {
            _context = context;
            dataset = _context.Set<Person>();
        }

        //Custom Methods based on Generic Repository
        public override Person FindByID(long id)
        {
            return dataset
                .Include(p => p.Department)
                .SingleOrDefault(p => p.Id.Equals(id));
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            //return dataset.FromSqlRaw($"select * from person where first_name like ='{name}'").ToList();
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                return dataset.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                return dataset.Where(p => p.FirstName.Contains(firstName)).ToList();
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                return dataset.Where(p => p.LastName.Contains(lastName)).ToList();
            else
                return null;
        }

        public List<Person> FindWithPagedSearch(Dictionary<string, string> filters, List<string> sortfields, string sort, int size, int offset)
        {
            string query = @"SELECT * FROM persons P WHERE 1 = 1 ";


            foreach (KeyValuePair<string,string> filter in filters)
            {
                query += $" AND p.{filter.Key} like '%{filter.Value}%' ";
            }

            if(sortfields == null || sortfields.Count ==0)
            {
                query += $" ORDER BY id {sort} ";
            }
            else
                query += $" ORDER BY {String.Join(", ", sortfields.ToArray())} {sort} ";

            query += $" LIMIT {size} OFFSET {offset} ";

            return ExecuteFromRaw(query);
        }

        public int GetCount(Dictionary<string, string> filters)
        {
            string countQuery = @"SELECT COUNT(*) FROM persons P WHERE 1 = 1 ";

            foreach (KeyValuePair<string, string> filter in filters)
            {
                countQuery += $" AND p.{filter.Key} like '%{filter.Value}%' ";
            }

            return ExecuteScalar(countQuery);
        }
    }
}
