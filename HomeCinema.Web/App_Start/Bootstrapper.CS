using HomeCinema.Web.Mappings;
using System.Web.Http;

namespace HomeCinema.Web.App_Start {
    public class Bootstrapper {

        public static void Run() {

            //Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //configure AutoMapper
            AutoMapperConfiguration.Configure();
            //AutoMapperConfiguration.Configure();
        }
    }
}