using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCinema.Data
{
    public class RoomsManager
    {
        private const string getRoomsQuery = "Select * from Rooms";
        //private const string addCategoryQuery = "Insert into Categories output INSERTED.Id values(@Name, @Desc, @Url)";
        private const string getRoomByIdQuery = "Select * from Rooms where Id=@Id";
        //private const string removeCategoryByIdQuery = "Delete from Categories where Id=@Id";

        public static List<Room> GetRooms()
        {
            try
            {
                DbReader reader = DbReader.GetInstance();

                //reader.Connection.Open();
                //List<Category> cats = reader.Connection.Query<Category>(getCategoriesQuery).ToList();

                List<Room> rooms = reader.RunReader<Room>(getRoomsQuery);

                return rooms;
            }
            catch (Exception ex)
            {
                throw new CouldNotPerformOperationException() { InnerException = ex };
            }
        }

        public static Room GetRoom(int id)
        {

            DbReader reader = DbReader.GetInstance();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Id", id));

            List<Room> rooms = new List<Room>();

            try
            {
                rooms = reader.RunReader<Room>(getRoomByIdQuery, parameters);
            }
            catch (Exception ex)
            {
                throw new CouldNotPerformOperationException() { InnerException = ex };
            }

            if (rooms.Any())
            {
                return rooms.FirstOrDefault();
            }
            else
            {
                throw new NotFoundException();
            }

        }

        //public static Category AddCategory(Category category)
        //{
        //    try
        //    {
        //        DbReader reader = DbReader.GetInstance();

        //        List<SqlParameter> parameters = new List<SqlParameter>();
        //        parameters.Add(new SqlParameter("Name", category.Name));
        //        parameters.Add(new SqlParameter("Desc", category.Description));
        //        parameters.Add(new SqlParameter("Url", category.ThumbnailUrl));

        //        int id = reader.RunSingleReader<int>(addCategoryQuery, parameters);

        //        if (id > 0)
        //        {
        //            category.Id = id;
        //            return category;
        //        }
        //        else
        //        {
        //            throw new CouldNotPerformOperationException();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new CouldNotPerformOperationException() { InnerException = ex };
        //    }
        //}

        //public static void DeleteCategory(int id)
        //{
        //    DbReader reader = DbReader.GetInstance();

        //    List<SqlParameter> parameters = new List<SqlParameter>();
        //    parameters.Add(new SqlParameter("Id", id));

        //    List<Category> categories = reader.RunReader<Category>(getCategoryByIdQuery, parameters);

        //    if (categories.Any())
        //    {
        //        int affectedRows;
        //        try
        //        {
        //            parameters = new List<SqlParameter>();
        //            parameters.Add(new SqlParameter("Id", id));

        //            affectedRows = reader.RunSimpleQuery(removeCategoryByIdQuery, parameters);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new CouldNotPerformOperationException() { InnerException = ex };
        //        }

        //        if (affectedRows == 1)
        //        {

        //        }
        //        else
        //        {
        //            throw new CouldNotPerformOperationException();
        //        }
        //    }
        //    else
        //    {
        //        throw new NotFoundException();
        //    }
        //}
    }
}
