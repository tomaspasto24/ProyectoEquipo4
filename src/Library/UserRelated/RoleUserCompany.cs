using System;

namespace Bot
{
    public class RoleUserCompany : Role
    {
        public Company company { private set; get; }

        public RoleUserCompany(Company company)
        {
            this.company = company;
        }
    }
}