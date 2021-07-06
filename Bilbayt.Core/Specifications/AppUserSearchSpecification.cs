using Ardalis.Specification;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;

namespace Bilbayt.Core.Specifications
{
    public class AppUserSearchSpecification : Specification<Entities.AppUser>
    {
        public AppUserSearchSpecification(string username = "",
                                             int pageStart = 0,
                                             int pageSize = 50,
                                             string sortColumn = "title",
                                             SortDirection sortDirection = SortDirection.Ascending,
                                             bool exactSearch = false
                                             )
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                if (exactSearch)
                {
                    Query.Where(item => item.UserName.ToLower() == username.ToLower());
                }
                else
                {
                    Query.Where(item => item.UserName.ToLower().Contains(username.ToLower()));
                }
            }

            // Pagination
            if (pageSize != -1) //Display all entries and disable pagination 
            {
                Query.Skip(pageStart).Take(pageSize);
            }

            // Sort
            switch (sortColumn.ToLower())
            {
                case ("firstname"):
                    {
                        if (sortDirection == SortDirection.Ascending)
                        {
                            Query.OrderBy(x => x.FirstName);
                        }
                        else
                        {
                            Query.OrderByDescending(x => x.FirstName);
                        }
                    }
                    break;
                case ("lastname"):
                    {
                        if (sortDirection == SortDirection.Ascending)
                        {
                            Query.OrderBy(x => x.LastName);
                        }
                        else
                        {
                            Query.OrderByDescending(x => x.LastName);
                        }
                    }
                    break;
                case ("username"):
                    {
                        if (sortDirection == SortDirection.Ascending)
                        {
                            Query.OrderBy(x => x.UserName);
                        }
                        else
                        {
                            Query.OrderByDescending(x => x.UserName);
                        }
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
