using System;

namespace Bot
{
    public class RoleUserCompany : Role
    {
        public Company company { private set; get; }

        public RoleUserCompany(Company company, string name, int id) : base(name, id)
        {
            this.company = company;
        }
    }
}