using AutoMapper;
using ePizza.Core.Contracts;
using ePizza.Models.Response;
using ePizza.Repository.Concrete;
using ePizza.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Core.Concrete
{
    public class ItemServices :IItemServices
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ItemServices(IItemRepository itemRepository, IMapper mapper, IConfiguration configuration ) 
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _config = configuration;
        }


        public IEnumerable<ItemResponseModel> GetItems()
        { 
          var items= _itemRepository.GetAll();
            return _mapper.Map<IEnumerable<ItemResponseModel>>( items );
        }


        /// Code with ADO.net

        public IEnumerable<ItemResponseModel> GetItemsUsingAdo()
        {
            List<ItemResponseModel> itemsList = new();
            using SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _config.GetConnectionString("DatabaseConnection");
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "select * from Items";
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ItemResponseModel itemResponseModel = new ItemResponseModel();

                itemResponseModel.ImageUrl = reader["ImageUrl"].ToString();
                itemResponseModel.ItemTypeId = Convert.ToInt32(reader["ItemTypeId"]);
                itemResponseModel.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);

                itemsList.Add(itemResponseModel);
            }
            return itemsList;
        }

        public IEnumerable<ItemResponseModel> GetItemsUsingProcedure()
        {
            var items = _itemRepository.CallProcedure();

            return new List<ItemResponseModel>();
        }

    }
}
