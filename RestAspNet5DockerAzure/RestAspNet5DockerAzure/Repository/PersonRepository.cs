using Microsoft.EntityFrameworkCore;
using RestAspNet5DockerAzure.Model;
using RestAspNet5DockerAzure.Model.Context;
using RestAspNet5DockerAzure.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAspNet5DockerAzure.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private MySQLContext _context;
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

        public List<Person> FindByName(string name)
        {
            //return dataset.FromSqlRaw($"select * from person where first_name like ='{name}'").ToList();
            return dataset.Where(p => (p.FirstName + ' ' + p.LastName).Contains(name)).ToList();
        }




    }
}
